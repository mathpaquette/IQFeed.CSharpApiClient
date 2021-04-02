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
    public class LookupRateLimiterTests
    {
        [Test]
        public async Task Should_Rate_Limit_By_Throughput()
        {
            // Arrange
            var debug = false;
            var totalSeconds = TimeSpan.FromSeconds(10).TotalSeconds;
            var requestsPerSecond = 50;
            var numberOfClients = 15;
            var testResults = new List<TestResult>();

            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);
            var cts = new CancellationTokenSource();
            await Task.Delay(1000);

            var tasks = new Task[numberOfClients];
            var mutex = new object();

            var requestsCount = 0;
            var seconds = 0;

            for (var i = 0; i < numberOfClients; i++)
            {
                var taskId = i;
                tasks[i] = new Task(async () =>
                {
                    while (true)
                    {
                        await lookupRateLimiter.WaitAsync().ConfigureAwait(false);
                        if (debug)
                            Console.WriteLine($"Executed task #{taskId}");
                        lock (mutex)
                            requestsCount++;
                    }
                }, cts.Token);
            }

            // Act
            for (var i = 0; i < numberOfClients; i++)
                tasks[i].Start();

            while (seconds < totalSeconds)
            {
                var sw = Stopwatch.StartNew();
                await Task.Delay(TimeSpan.FromSeconds(1));
                sw.Stop();

                Console.WriteLine($"requests: {requestsCount}/{sw.Elapsed.TotalMilliseconds}ms");
                lock (mutex)
                {
                    testResults.Add(new TestResult(requestsCount, sw.Elapsed.TotalMilliseconds));
                    requestsCount = 0;
                }
                seconds++;
            }

            cts.Cancel();
            cts.Dispose();

            // Assert
            
            // initial burst
            var initialBurst = testResults.First();
            var calcInitialBustRequestsPerSecond = initialBurst.NumberOfRequest * 1000 / initialBurst.TotalMilliseconds;
            var expectedInitialBurst = lookupRateLimiter.RequestsPerSecond + lookupRateLimiter.MaxCount;
            Assert.That(calcInitialBustRequestsPerSecond, Is.LessThan(expectedInitialBurst).Within(5).Percent);
            Console.WriteLine($"Initial burst {calcInitialBustRequestsPerSecond} requests/second");

            // average
            var testResultsWithoutInitialBurst = testResults.Skip(1).ToList();
            var totalMs = testResultsWithoutInitialBurst.Sum(x => x.TotalMilliseconds);
            var totalRequests = testResultsWithoutInitialBurst.Sum(x => x.NumberOfRequest);
            var calcRequestsPerSecond = totalRequests * 1000 / totalMs;
            var variationPercentage = Math.Abs(calcRequestsPerSecond - requestsPerSecond) / requestsPerSecond * 100;

            Console.WriteLine($"Average {calcRequestsPerSecond} requests/second");
            Console.WriteLine($"Variation {variationPercentage} %");
            Assert.That(calcRequestsPerSecond, Is.EqualTo(requestsPerSecond).Within(1).Percent);
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
        public void Should_Validate_MaxCount()
        {
            // Arrange
            var requestsPerSecond = 50;

            // Act
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);

            // Assert
            var expectedMaxCount = 25;
            Assert.AreEqual(lookupRateLimiter.MaxCount, expectedMaxCount);
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

        class TestResult
        {
            public TestResult(int numberOfRequest, double totalMilliseconds)
            {
                NumberOfRequest = numberOfRequest;
                TotalMilliseconds = totalMilliseconds;
            }

            public int NumberOfRequest { get; }
            public double TotalMilliseconds { get; }
        }
    }
}