using System;
using System.IO;
using System.Threading;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Socket;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    public class LookupMessageFileHandlerTests
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
        public async Task Should_Write_Response_To_File_When_Server_Answers()
        {
            // Arrange
            using (var server = new FakeLookupServer(request => $"LINE1\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();

                // Act
                var filename = await WithinAsync(handler.GetFilenameAsync("TEST\r\n"));

                // Assert
                try
                {
                    StringAssert.Contains(IQFeedDefault.ProtocolEndOfMessageCharacters, File.ReadAllText(filename));
                }
                finally
                {
                    File.Delete(filename);
                    lookupDispatcher.DisconnectAll();
                }
            }
        }

        [Test]
        public void Should_Time_Out_When_No_Socket_Becomes_Available()
        {
            // Arrange: never connected, so no socket ever joins the pool
            var lookupDispatcher = CreateLookupDispatcher(1);
            var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMilliseconds(200));

            // Act
            var request = handler.GetFilenameAsync("TEST\r\n");

            // Assert
            Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(request));
        }

        [Test]
        public void Should_Fail_Waiting_Request_When_Disconnected()
        {
            // Arrange
            var lookupDispatcher = CreateLookupDispatcher(1);
            var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
            var request = handler.GetFilenameAsync("TEST\r\n");

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
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                var request = handler.GetFilenameAsync("TEST\r\n");
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
            // Arrange: the slow answer is held back until its request has timed out, then sent on the socket the next request would reuse
            var sendSlowAnswer = new TaskCompletionSource<bool>();
            using (var server = new FakeLookupServer(async request =>
            {
                if (request.StartsWith("SLOW", StringComparison.Ordinal))
                {
                    await sendSlowAnswer.Task.ConfigureAwait(false);
                    return $"STALE\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n";
                }

                return $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n";
            }))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                var impatient = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMilliseconds(500));
                var patient = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                File.Delete(await WithinAsync(patient.GetFilenameAsync("FAST\r\n"))); // the socket is confirmed, so the short timeout only covers the reply
                var filesBefore = Directory.GetFiles(Environment.CurrentDirectory).Length;
                Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(impatient.GetFilenameAsync("SLOW\r\n")));
                Assert.That(Directory.GetFiles(Environment.CurrentDirectory).Length, Is.EqualTo(filesBefore), "the partial file must be deleted");
                sendSlowAnswer.SetResult(true);

                // Act
                var filename = await WithinAsync(patient.GetFilenameAsync("FAST\r\n"));

                // Assert: the next request runs on a new socket and gets its own answer, not the rest of the abandoned one
                try
                {
                    var content = File.ReadAllText(filename);
                    StringAssert.Contains("FRESH", content);
                    StringAssert.DoesNotContain("STALE", content);
                    Assert.That(server.ConnectionsAccepted, Is.EqualTo(2));
                }
                finally
                {
                    File.Delete(filename);
                    lookupDispatcher.DisconnectAll();
                }
            }
        }

        [Test]
        public async Task Should_Replace_Socket_When_Send_Throws()
        {
            // Arrange: the only socket was never connected, so the first send on it throws. It is handed to the empty pool directly,
            // which only works because that pool was never connected.
            using (var server = new FakeLookupServer(request => $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                lookupDispatcher.Add(new SocketClient("localhost", server.Port));
                var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                Assert.ThrowsAsync<SocketException>(() => WithinAsync(handler.GetFilenameAsync("TEST\r\n")));

                // Act
                var filename = await WithinAsync(handler.GetFilenameAsync("TEST\r\n"));

                // Assert: the next request runs on the connected replacement
                try
                {
                    StringAssert.Contains("FRESH", File.ReadAllText(filename));
                    Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                }
                finally
                {
                    File.Delete(filename);
                    lookupDispatcher.DisconnectAll();
                }
            }
        }

        [Test]
        public async Task Should_Not_Send_Request_That_Timed_Out_Before_It_Was_Sent()
        {
            // Arrange: the impatient request waits for a rate-limit slot that never comes
            using (var exhaustedRateLimiter = new FakeExhaustedLookupRateLimiter())
            using (var server = new FakeLookupServer(request => $"FRESH\r\n{IQFeedDefault.ProtocolEndOfMessageCharacters},\r\n"))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                var patient = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                var impatient = new LookupMessageFileHandler(lookupDispatcher, exhaustedRateLimiter, new ExceptionFactory(), TimeSpan.FromMilliseconds(200));
                lookupDispatcher.ConnectAll();
                File.Delete(await WithinAsync(patient.GetFilenameAsync("FAST\r\n")));

                // Act
                Assert.ThrowsAsync<TaskCanceledException>(() => WithinAsync(impatient.GetFilenameAsync("FAST\r\n")));

                // Assert: it never reached IQFeed and left the socket in the pool
                Assert.That(server.RequestsReceived, Is.EqualTo(1));
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public void Should_Fail_And_Delete_File_When_Disconnected_While_Data_Streams()
        {
            // Arrange: a large answer that never ends, so the request is still writing to its file when the client disconnects
            var line = "DATA,1,2,3,4,5,6,7,8,9\r\n";
            var answer = string.Concat(Enumerable.Repeat(line, 200000));
            using (var server = new FakeLookupServer(request => answer))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                var handler = new LookupMessageFileHandler(lookupDispatcher, _lookupRateLimiter, new ExceptionFactory(), TimeSpan.FromMinutes(1));
                lookupDispatcher.ConnectAll();
                var filesBefore = Directory.GetFiles(Environment.CurrentDirectory);
                var request = handler.GetFilenameAsync("TEST\r\n");
                Assert.That(() => Directory.GetFiles(Environment.CurrentDirectory).Except(filesBefore).Any(file => new FileInfo(file).Length > 0),
                    Is.True.After(10000, 10), "data must be arriving into the file");

                // Act
                lookupDispatcher.DisconnectAll();

                // Assert: the request fails cleanly and a late write into the closed file does not bring the process down
                Assert.ThrowsAsync<ObjectDisposedException>(() => WithinAsync(request));
                Thread.Sleep(500);
                Assert.That(Directory.GetFiles(Environment.CurrentDirectory).Except(filesBefore), Is.Empty, "the partial file must be deleted");
            }
        }

        private static LookupDispatcher CreateLookupDispatcher(int port)
        {
            return new LookupDispatcher("localhost", port, LookupDefault.BufferSize, IQFeedDefault.ProtocolVersion, 1, new RequestFormatter());
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
