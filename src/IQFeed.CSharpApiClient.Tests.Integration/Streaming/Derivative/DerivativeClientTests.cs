using System;
using System.Linq;
using System.Threading;
using IQFeed.CSharpApiClient.Streaming.Derivative;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Derivative
{
    public class DerivativeClientTests
    {
        private const int TimeoutMs = 30000;
        private const string Symbol = "AAPL";
        private const int Interval = 5;
        private IDerivativeClient _derivativeClient;

        public DerivativeClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _derivativeClient = DerivativeClientFactory.CreateNew();
            _derivativeClient.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _derivativeClient.Disconnect();
        }

        [Test, MaxTime(TimeoutMs)]
        [TestCase(null)]
        [TestCase("TEST12345")]
        public void Should_Raise_IntervalBars_When_ReqBarWatch(string requestId)
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            var eventCount = 0;
            var eventCountExpected = 100;
            var symbol = Symbol;

            _derivativeClient.IntervalBar += message =>
            {
                if (message.Symbol == symbol && message.RequestId == requestId)
                    eventCount++;

                if (eventCount == eventCountExpected)
                    eventRaised.Set();
            };

            // Act
            _derivativeClient.ReqBarWatch(symbol, Interval, DateTime.Now.AddDays(-120), eventCountExpected, requestId: requestId);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Raise_SymbolNotFound_When_ReqBarWatch_With_Invalid_Symbol()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            var notFoundSymbol = "THISSYMBOLDOESNTEXIST";

            _derivativeClient.SymbolNotFound += message =>
            {
                if (message.Symbol == notFoundSymbol)
                    eventRaised.Set();
            };

            // Act
            _derivativeClient.ReqBarWatch(notFoundSymbol, Interval);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Raise_System_When_ReqWatches()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);

            _derivativeClient.System += message =>
            {
                if (message.Type == "WATCHES")
                    eventRaised.Set();
            };

            // Act
            _derivativeClient.ReqWatches();

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Raise_System_Without_Watches_When_UnwatchAll()
        {
            // Arrange
            var watchedEventRaised = new ManualResetEvent(false);
            var unwatchedeventRaised = new ManualResetEvent(false);

            _derivativeClient.System += message =>
            {
                if (message.Type != "WATCHES")
                    return;

                var definitions = DerivativeWatchDefinition.Parse(message.Message).ToList();

                if (!definitions.Any())
                    unwatchedeventRaised.Set();

                if (definitions.Count == 2)
                    watchedEventRaised.Set();
            };

            // Act
            _derivativeClient.ReqBarWatch(Symbol, Interval, requestId: "RequestId1");
            _derivativeClient.ReqBarWatch(Symbol, 2 * Interval, requestId: "RequestId2");
            _derivativeClient.ReqWatches();
            watchedEventRaised.WaitOne();

            _derivativeClient.UnwatchAll();
            _derivativeClient.ReqWatches();

            // Assert
            Assert.IsTrue(unwatchedeventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Raise_System_With_Less_Watches_When_ReqBarUnwatch()
        {
            // Arrange
            var watchedEventRaised = new ManualResetEvent(false);
            var unwatchedeventRaised = new ManualResetEvent(false);

            _derivativeClient.System += message =>
            {
                if (message.Type != "WATCHES")
                    return;

                var definitions = DerivativeWatchDefinition.Parse(message.Message).ToList();

                if (definitions.Count == 1)
                    unwatchedeventRaised.Set();

                if (definitions.Count == 2)
                    watchedEventRaised.Set();
            };

            // Act
            _derivativeClient.ReqBarWatch(Symbol, Interval, requestId: "RequestId1");
            _derivativeClient.ReqBarWatch(Symbol, 2 * Interval, requestId: "RequestId2");
            _derivativeClient.ReqWatches();
            watchedEventRaised.WaitOne();

            _derivativeClient.ReqBarUnwatch(Symbol, "RequestId2");
            _derivativeClient.ReqWatches();

            // Assert
            Assert.IsTrue(unwatchedeventRaised.WaitOne());
        }
    }
}