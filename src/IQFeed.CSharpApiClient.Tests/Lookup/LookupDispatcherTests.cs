using System;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Tests.Lookup.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup
{
    public class LookupDispatcherTests
    {
        [Test]
        public void Should_Throw_When_Canceled_Before_A_Socket_Is_Free()
        {
            // Arrange: never connected, so no socket ever joins the pool
            var lookupDispatcher = CreateLookupDispatcher();

            using (var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100)))
            {
                // Act
                var take = lookupDispatcher.TakeAsync(cts.Token);

                // Assert
                Assert.That(() => take.Wait(TimeSpan.FromSeconds(10)), Throws.InstanceOf<AggregateException>().With.InnerException.InstanceOf<TaskCanceledException>());
            }
        }

        [Test]
        public void Should_Throw_When_Disconnected_While_Waiting_For_A_Socket()
        {
            // Arrange
            var lookupDispatcher = CreateLookupDispatcher();
            var take = lookupDispatcher.TakeAsync(CancellationToken.None);

            // Act
            lookupDispatcher.DisconnectAll();

            // Assert
            Assert.That(() => take.Wait(TimeSpan.FromSeconds(10)), Throws.InstanceOf<AggregateException>().With.InnerException.InstanceOf<ObjectDisposedException>());
        }

        [Test]
        public void Should_Throw_When_Taking_A_Socket_After_Disconnect()
        {
            // Arrange
            var lookupDispatcher = CreateLookupDispatcher();
            lookupDispatcher.DisconnectAll();

            // Act
            var take = lookupDispatcher.TakeAsync();

            // Assert
            Assert.That(() => take.Wait(TimeSpan.FromSeconds(10)), Throws.InstanceOf<AggregateException>().With.InnerException.InstanceOf<ObjectDisposedException>());
        }

        [Test]
        public async Task Should_Connect_A_New_Socket_In_Place_Of_A_Replaced_One()
        {
            // Arrange
            using (var server = new FakeLookupServer(request => (string)null))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                lookupDispatcher.ConnectAll();
                var socketClient = await lookupDispatcher.TakeAsync(TenSeconds());

                // Act
                lookupDispatcher.Replace(socketClient);

                // Assert
                var replacement = await lookupDispatcher.TakeAsync(TenSeconds());
                Assert.That(replacement, Is.Not.SameAs(socketClient));
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(2));
                lookupDispatcher.DisconnectAll();
            }
        }

        [Test]
        public async Task Should_Not_Connect_A_Replacement_After_Disconnect()
        {
            // Arrange
            using (var server = new FakeLookupServer(request => (string)null))
            {
                var lookupDispatcher = CreateLookupDispatcher(server.Port);
                lookupDispatcher.ConnectAll();
                var socketClient = await lookupDispatcher.TakeAsync(TenSeconds());
                lookupDispatcher.DisconnectAll();

                // Act
                lookupDispatcher.Replace(socketClient);

                // Assert: a replacement connects within milliseconds (see the test above), so a second is ample
                await Task.Delay(TimeSpan.FromSeconds(1));
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task Should_Keep_Retrying_A_Replacement_Until_IQFeed_Accepts_Connections()
        {
            // Arrange: a free port that nothing listens on yet, and a taken socket to replace
            var probe = new TcpListener(IPAddress.Loopback, 0);
            probe.Start();
            var port = ((IPEndPoint)probe.LocalEndpoint).Port;
            probe.Stop();
            var lookupDispatcher = CreateLookupDispatcher(port);
            lookupDispatcher.Add(new SocketClient("localhost", port));
            var socketClient = await lookupDispatcher.TakeAsync(TenSeconds());

            // Act: the replacement is refused until IQFeed comes back. A refused connect can take about 2s on Windows, so the
            // outage lasts long enough for at least one attempt to fail outright.
            lookupDispatcher.Replace(socketClient);
            await Task.Delay(TimeSpan.FromSeconds(5));

            using (var server = new FakeLookupServer(request => (string)null, port))
            {
                // Assert
                var replacement = await lookupDispatcher.TakeAsync(TwentySeconds());
                Assert.That(replacement, Is.Not.SameAs(socketClient));
                Assert.That(server.ConnectionsAccepted, Is.EqualTo(1));
                lookupDispatcher.DisconnectAll();
            }
        }

        private static CancellationToken TwentySeconds()
        {
            return new CancellationTokenSource(TimeSpan.FromSeconds(20)).Token;
        }

        private static CancellationToken TenSeconds()
        {
            return new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token;
        }

        private static LookupDispatcher CreateLookupDispatcher(int port)
        {
            return new LookupDispatcher("localhost", port, LookupDefault.BufferSize, IQFeedDefault.ProtocolVersion, 1, new RequestFormatter());
        }

        private static LookupDispatcher CreateLookupDispatcher()
        {
            return new LookupDispatcher("localhost", 1, LookupDefault.BufferSize, IQFeedDefault.ProtocolVersion, 1, new RequestFormatter());
        }
    }
}
