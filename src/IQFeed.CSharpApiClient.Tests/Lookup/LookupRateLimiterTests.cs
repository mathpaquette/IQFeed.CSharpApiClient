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
        public async Task Should_Rate_Limit_Measure_By_Iterations()
        {
            // Arrange
            const int requestsPerSecond = 50;
            const int experimentDurationSeconds = 10;
            const int iterations = experimentDurationSeconds * requestsPerSecond;

            // Act
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                await lookupRateLimiter.WaitAsync();
                lookupRateLimiter.Release();
            }

            sw.Stop();

            Console.WriteLine($"requests: {iterations}");
            Console.WriteLine($"elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"average call request time: {sw.Elapsed.TotalMilliseconds / iterations}ms, Interval: {lookupRateLimiter.Interval.TotalMilliseconds}ms");

            // Assert

            Assert.That(sw.Elapsed.TotalMilliseconds / iterations,
                Is.EqualTo(lookupRateLimiter.Interval.TotalMilliseconds).Within(3).Percent
                    .And.GreaterThanOrEqualTo(lookupRateLimiter.Interval.TotalMilliseconds)); //value has to be higher than 20ms to match serverside.
        }

        [Test]
        public async Task Should_Rate_Limit_Measure_By_Time()
        {
            // Arrange
            const int seconds = 15;
            var totalSecondsTicks = TimeSpan.FromSeconds(seconds);

            const int requestsPerSecond = 50;
            var requestsCount = 0;

            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);

            // Act
            var sw = Stopwatch.StartNew();

            while (true)
            {
                await lookupRateLimiter.WaitAsync();
                lookupRateLimiter.Release();

                requestsCount++;
                if (sw.Elapsed >= totalSecondsTicks)
                {
                    sw.Stop();
                    break;
                }
            }

            Console.WriteLine($"requests: {requestsCount}");
            Console.WriteLine($"elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"average call request time: {sw.Elapsed.TotalMilliseconds / requestsCount}ms, Interval: {lookupRateLimiter.Interval.TotalMilliseconds}ms");


            // Assert
            Assert.That(sw.Elapsed.TotalMilliseconds / requestsCount,
                Is.EqualTo(lookupRateLimiter.Interval.TotalMilliseconds).Within(3).Percent
                    .And.GreaterThanOrEqualTo(lookupRateLimiter.Interval.TotalMilliseconds)); //value has to be higher than 20ms to match serverside.
        }


        [Test]
        public async Task Should_Rate_Limit_Measure_By_Iterations_HighPrecision()
        {
            // Arrange
            const int requestsPerSecond = 50;
            const int experimentDurationSeconds = 10;
            const int iterations = experimentDurationSeconds * requestsPerSecond;

            // Act
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond, higherPrecision: true);
            Stopwatch sw = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                await lookupRateLimiter.WaitAsync();
                lookupRateLimiter.Release();
            }

            sw.Stop();

            Console.WriteLine($"requests: {iterations}");
            Console.WriteLine($"elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"average call request time: {sw.Elapsed.TotalMilliseconds / iterations}ms, Interval: {lookupRateLimiter.Interval.TotalMilliseconds}ms");

            // Assert

            Assert.That(sw.Elapsed.TotalMilliseconds / iterations,
                Is.EqualTo(lookupRateLimiter.Interval.TotalMilliseconds).Within(3).Percent
                    .And.GreaterThanOrEqualTo(lookupRateLimiter.Interval.TotalMilliseconds)); //value has to be higher than 20ms to match serverside.
        }

        [Test]
        public async Task Should_Rate_Limit_Measure_By_Time_HighPrecision()
        {
            // Arrange
            const int seconds = 15;
            var totalSecondsTicks = TimeSpan.FromSeconds(seconds);

            const int requestsPerSecond = 50;
            var requestsCount = 0;

            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond, higherPrecision: true);

            // Act
            var sw = Stopwatch.StartNew();

            while (true)
            {
                await lookupRateLimiter.WaitAsync();
                lookupRateLimiter.Release();

                requestsCount++;
                if (sw.Elapsed >= totalSecondsTicks)
                {
                    sw.Stop();
                    break;
                }
            }

            Console.WriteLine($"requests: {requestsCount}");
            Console.WriteLine($"elapsed: {sw.Elapsed.TotalMilliseconds}ms");
            Console.WriteLine($"average call request time: {sw.Elapsed.TotalMilliseconds / requestsCount}ms, Interval: {lookupRateLimiter.Interval.TotalMilliseconds}ms");

            // Assert
            Assert.That(sw.Elapsed.TotalMilliseconds / requestsCount,
                Is.EqualTo(lookupRateLimiter.Interval.TotalMilliseconds).Within(3).Percent
                    .And.GreaterThanOrEqualTo(lookupRateLimiter.Interval.TotalMilliseconds)); //value has to be higher than 20ms to match serverside.
        }


        [Test]
        public async Task Should_Rate_Limit_By_Throughput_Original()
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
                    lookupRateLimiter.Release();
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