using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup
{
    public class LookupClientTests
    {
        [Test]
        public void Should_Stop_The_Rate_Limiter_When_Disposed_Before_Any_Request()
        {
            // Arrange
            var lookupRateLimiter = new LookupRateLimiter(50);
            var lookupClient = CreateLookupClient(lookupRateLimiter);

            // Act
            lookupClient.Dispose();

            // Assert
            Assert.That(lookupRateLimiter.IsRunning, Is.False);
        }

        [Test]
        public async Task Should_Stop_The_Rate_Limiter_When_Disposed_After_A_Request()
        {
            // Arrange: a request starts the rate limiter's release loop
            var lookupRateLimiter = new LookupRateLimiter(50);
            var lookupClient = CreateLookupClient(lookupRateLimiter);
            await lookupRateLimiter.WaitAsync();

            // Act
            lookupClient.Dispose();

            // Assert
            Assert.That(lookupRateLimiter.IsRunning, Is.False);
        }

        [Test]
        public async Task Should_Not_Wait_Out_The_Rate_Limiter_Warm_Up_When_Disposed()
        {
            // Arrange: after the first request the release loop waits one second before it starts releasing
            var lookupRateLimiter = new LookupRateLimiter(50);
            var lookupClient = CreateLookupClient(lookupRateLimiter);
            await lookupRateLimiter.WaitAsync();
            await Task.Delay(100);
            var stopwatch = Stopwatch.StartNew();

            // Act
            lookupClient.Dispose();

            // Assert
            Assert.That(stopwatch.Elapsed, Is.LessThan(TimeSpan.FromMilliseconds(500)));
            Assert.That(lookupRateLimiter.IsRunning, Is.False);
        }

        [Test]
        public void Should_Keep_The_Rate_Limiter_Running_When_Disconnected()
        {
            // Arrange
            var lookupRateLimiter = new LookupRateLimiter(50);
            var lookupClient = CreateLookupClient(lookupRateLimiter);

            // Act
            lookupClient.Disconnect();

            // Assert: the rate limiter belongs to the client, not to its connection
            Assert.That(lookupRateLimiter.IsRunning, Is.True);
            lookupClient.Dispose();
        }

        [Test]
        public void Should_Disconnect_When_Disposed()
        {
            // Arrange
            var lookupDispatcher = CreateLookupDispatcher();
            var lookupClient = new LookupClient(lookupDispatcher, null, null, null, null, null, new LookupRateLimiter(50));

            // Act
            lookupClient.Dispose();

            // Assert: a disconnected dispatcher fails a request for a socket at once
            var take = lookupDispatcher.TakeAsync();
            Assert.That(() => take.Wait(TimeSpan.FromSeconds(10)), Throws.InstanceOf<AggregateException>().With.InnerException.InstanceOf<ObjectDisposedException>());
        }

        [Test]
        public void Should_Allow_Disposing_Twice()
        {
            // Arrange
            var lookupRateLimiter = new LookupRateLimiter(50);
            var lookupClient = CreateLookupClient(lookupRateLimiter);
            lookupClient.Dispose();

            // Act & Assert
            Assert.That(() => lookupClient.Dispose(), Throws.Nothing);
            Assert.That(lookupRateLimiter.IsRunning, Is.False);
        }

        [Test]
        public void Should_Allow_Disposing_A_Client_Created_Without_A_Rate_Limiter()
        {
            // Arrange
            var lookupClient = new LookupClient(CreateLookupDispatcher(), null, null, null, null, null);

            // Act & Assert
            Assert.That(() => lookupClient.Dispose(), Throws.Nothing);
        }

        [Test]
        public void Should_Stop_The_Rate_Limiter_Of_A_Client_Created_By_The_Factory_When_Disposed()
        {
            // Arrange: the factory never connects, so nothing is dialed
            var lookupClient = LookupClientFactory.CreateNew();
            var lookupRateLimiter = GetLookupRateLimiter(lookupClient);
            Assert.That(lookupRateLimiter.IsRunning, Is.True);

            // Act
            lookupClient.Dispose();

            // Assert
            Assert.That(lookupRateLimiter.IsRunning, Is.False);
        }

        private static LookupClient CreateLookupClient(LookupRateLimiter lookupRateLimiter)
        {
            return new LookupClient(CreateLookupDispatcher(), null, null, null, null, null, lookupRateLimiter);
        }

        private static LookupDispatcher CreateLookupDispatcher()
        {
            return new LookupDispatcher("localhost", 1, LookupDefault.BufferSize, IQFeedDefault.ProtocolVersion, 1, new RequestFormatter());
        }

        private static LookupRateLimiter GetLookupRateLimiter(LookupClient lookupClient)
        {
            var field = typeof(LookupClient).GetField("_lookupRateLimiter", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.That(field, Is.Not.Null, "LookupClient no longer has a '_lookupRateLimiter' field - update this test.");
            return (LookupRateLimiter)field.GetValue(lookupClient);
        }
    }
}
