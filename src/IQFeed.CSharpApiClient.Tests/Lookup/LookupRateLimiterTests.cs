using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup
{
    /// <summary>
    /// Some tests have a tendency to be flaky under CI heavy load.
    /// </summary>
    public class LookupRateLimiterTests
    {
        [Test]
        public async Task Should_Rate_Limit_By_Throughput()
        {
            // Arrange
            var totalSeconds = TimeSpan.FromSeconds(15).TotalSeconds;
            var seconds = 0;

            var requestsPerSecond = 50;
            var requestsCount = 0;
            var requests = new List<int>();

            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);
            var cts = new CancellationTokenSource();

            // Act
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    await lookupRateLimiter.WaitAsync();
                    Interlocked.Increment(ref requestsCount);
                }
            }, cts.Token);

            while (seconds < totalSeconds)
            {
                var sw = Stopwatch.StartNew();
                await Task.Delay(TimeSpan.FromSeconds(1));
                sw.Stop();

                Console.WriteLine($"requests: {requestsCount}/{sw.Elapsed.TotalMilliseconds}ms");
                requests.Add(requestsCount);
                Interlocked.Exchange(ref requestsCount, 0);
                seconds++;
            }

            cts.Cancel();
            cts.Dispose();

            // Assert
            Assert.That(requests.Average(), Is.EqualTo(requestsPerSecond).Within(2).Percent);
        }

        [Test]
        public void Should_Validate_RequestsPerSecond()
        {
            // Arrange
            var requestsPerSecond = 50;

            // Act
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);

            // Assert
            Assert.AreEqual(lookupRateLimiter.RequestsPerSecond, requestsPerSecond);
        }

        [Test]
        public void Should_Validate_Interval()
        {
            // Arrange
            var requestsPerSecond = 50;

            // Act
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);

            // Assert
            var expectedInterval = TimeSpan.FromMilliseconds(1000 / requestsPerSecond);
            Assert.AreEqual(lookupRateLimiter.Interval, expectedInterval);
        }

        [Test]
        public void Should_Dispose()
        {
            // Arrange
            var requestsPerSecond = 50;
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);

            // Act
            lookupRateLimiter.Dispose();

            // Assert
            Assert.False(lookupRateLimiter.IsRunning);
        }
    }
}