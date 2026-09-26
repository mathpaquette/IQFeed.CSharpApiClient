using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Socket;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    public class BaseLookupFacadeTests
    {
        // how long a test waits for a request that must end quickly, far beyond what a passing run needs
        private static readonly TimeSpan MustEndWithin = TimeSpan.FromSeconds(10);

        private LookupRateLimiter _lookupRateLimiter;

        [SetUp]
        public void SetUp()
        {
            _lookupRateLimiter = new LookupRateLimiter(LookupDefault.RequestsPerSecond);
        }

        [TearDown]
        public void TearDown()
        {
            _lookupRateLimiter.Dispose();
        }

        [Test]
        public async Task Should_Return_Messages_When_Server_Answers()
        {
            // Arrange
            using (var server = new FakeLookupServer(request => $"LINE1\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();

                // Act
                var lines = await WithinAsync(facade.GetLinesAsync("TEST\r\n"));

                // Assert
                StringAssert.Contains(IQFeedDefault.ProtocolEndOfMessageCharacters, string.Concat(lines));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public void Should_Time_Out_When_No_Socket_Becomes_Available()
        {
            // Arrange: never connected, so no socket ever joins the pool
            var lookupDispatcher = CreateLookupDispatcher(1, 1);
            var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMilliseconds(200));

            // Act
            var request = facade.GetLinesAsync("TEST\r\n");

            // Assert
            Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(request));
        }

        [Test]
        public void Should_Fail_Waiting_Request_When_Disconnected()
        {
            // Arrange
            var lookupDispatcher = CreateLookupDispatcher(1, 1);
            var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
            var request = facade.GetLinesAsync("TEST\r\n");

            // Act
            lookupDispatcher.DisconnectAll();

            // Assert
            Assert.ThrowsAsync<ObjectDisposedException>(() => WithinAsync(request));
        }

        [Test]
        public void Should_Fail_InFlight_Request_When_Disconnected()
        {
            // Arrange: the server confirms the protocol but never answers the request
            using (var server = new FakeLookupServer(request => (string)null))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                var request = facade.GetLinesAsync("TEST\r\n");
                Assert.That(() => server.RequestsReceived, Is.EqualTo(1).After(10000, 20), "the request must be in flight");

                // Act
                lookupDispatcher.DisconnectAll();

                // Assert
                Assert.ThrowsAsync<ObjectDisposedException>(() => WithinAsync(request));
            }
        }

        [Test]
        public async Task Should_Replace_Socket_When_Request_Times_Out_After_Send()
        {
            // Arrange: the slow answer arrives after its request timed out, on the socket the next request would reuse
            using (var server = new FakeLookupServer(async request =>
            {
                if (request.StartsWith("SLOW", StringComparison.Ordinal))
                {
                    await Task.Delay(TimeSpan.FromSeconds(2)).ConfigureAwait(false);
                    return $"STALE\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n";
                }

                return $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n";
            }))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var impatient = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMilliseconds(500));
                var patient = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                await WithinAsync(patient.GetLinesAsync("FAST\r\n")); // the socket is confirmed, so the short timeout only covers the reply
                Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(impatient.GetLinesAsync("SLOW\r\n")));

                // Act
                var lines = string.Concat(await WithinAsync(patient.GetLinesAsync("FAST\r\n")));

                // Assert: the next request runs on a new socket and gets its own answer, not the rest of the abandoned one
                StringAssert.Contains("FRESH", lines);
                StringAssert.DoesNotContain("STALE", lines);
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(2));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public async Task Should_Replace_Socket_When_Send_Throws()
        {
            // Arrange: the only socket was never connected, so the first send on it throws. It is handed to the empty pool directly,
            // which only works because that pool was never connected.
            using (var server = new FakeLookupServer(request => $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                lookupDispatcher.Add(new SocketClient("localhost", server.Port));
                var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                Assert.ThrowsAsync<SocketException>(() => WithinAsync(facade.GetLinesAsync("TEST\r\n")));

                // Act
                var lines = string.Concat(await WithinAsync(facade.GetLinesAsync("TEST\r\n")));

                // Assert: the next request runs on the connected replacement
                StringAssert.Contains("FRESH", lines);
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public async Task Should_Keep_Socket_When_IQFeed_Answers_With_An_Error()
        {
            // Arrange: an error answer is complete, so the socket carries nothing more
            using (var server = new FakeLookupServer(request => request.StartsWith("EMPTY", StringComparison.Ordinal)
                ? $"E,{IQFeedDefault.ProtocolNoDataCharacters},\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"
                : $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                Assert.ThrowsAsync<NoDataIQFeedException>(() => WithinAsync(facade.GetLinesAsync("EMPTY\r\n")));

                // Act
                var lines = string.Concat(await WithinAsync(facade.GetLinesAsync("FAST\r\n")));

                // Assert
                StringAssert.Contains("FRESH", lines);
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public async Task Should_Not_Send_Request_That_Timed_Out_Before_It_Was_Sent()
        {
            // Arrange: one request per second, and the only slot of this second is used up
            using (var lookupRateLimiter = new LookupRateLimiter(1))
            using (var server = new FakeLookupServer(request => $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var patient = new BaseLookupFacadeTestClass(lookupDispatcher, lookupRateLimiter, TimeSpan.FromMinutes(1));
                var impatient = new BaseLookupFacadeTestClass(lookupDispatcher, lookupRateLimiter, TimeSpan.FromMilliseconds(200));
                lookupDispatcher.ConnectAll();
                await WithinAsync(patient.GetLinesAsync("FAST\r\n"));
                var stopwatch = Stopwatch.StartNew();

                // Act
                Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(impatient.GetLinesAsync("FAST\r\n")));

                // Assert: it gave up at its own timeout rather than the rate limit's, never reached IQFeed, and left the socket in the pool
                Assert.That(stopwatch.Elapsed, Is.LessThan(TimeSpan.FromMilliseconds(900)));
                Assert.That(server.RequestsReceived, Is.EqualTo(1));
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public void Should_Not_Run_Caller_Continuations_On_The_Disconnecting_Thread()
        {
            // Arrange: the caller does slow work once its request fails
            using (var server = new FakeLookupServer(request => (string)null))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port, 1);
                var facade = new BaseLookupFacadeTestClass(lookupDispatcher, _lookupRateLimiter, TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                var request = facade.GetLinesAsync("TEST\r\n");
                var callerWork = request.ContinueWith(_ => Thread.Sleep(TimeSpan.FromSeconds(2)), TaskContinuationOptions.ExecuteSynchronously);
                Assert.That(() => server.RequestsReceived, Is.EqualTo(1).After(10000, 20), "the request must be in flight");
                var stopwatch = Stopwatch.StartNew();

                // Act
                lookupDispatcher.DisconnectAll();

                // Assert
                Assert.That(stopwatch.Elapsed, Is.LessThan(TimeSpan.FromSeconds(1)), "DisconnectAll must not wait for the caller's work");
                Assert.That(callerWork.Wait(MustEndWithin), Is.True);
            }
        }

        private static LookupDispatcher CreateLookupDispatcher(int port, int numberOfClients)
        {
            return new LookupDispatcher("localhost", port, LookupDefault.BufferSize, IQFeedDefault.ProtocolVersion, numberOfClients, new RequestFormatter());
        }

        private static async Task<T> WithinAsync<T>(Task<T> task)
        {
            var winner = await Task.WhenAny(task, Task.Delay(MustEndWithin)).ConfigureAwait(false);
            if (winner != task)
                Assert.Fail($"The request did not end within {MustEndWithin}.");

            return await task.ConfigureAwait(false);
        }
    }
}
