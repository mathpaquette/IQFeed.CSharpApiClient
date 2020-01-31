using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level2;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Level2
{
    /**
     * These tests require Level 2 data access from IQFeed.
     * By default they are not executed. You should trigger them manually.
     * In theory, these tests should be run during normal market hours.
     */
    [Explicit]
    [TestFixture]
    public class Level2ClientTests
    {
        private const int TimeoutMs = 15000;
        private const string Symbol = "AAPL";
        private const string MarketMarkerId = "MD02";
        private ILevel2Client<double> _level2Client;

        public Level2ClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _level2Client = Level2ClientFactory.CreateNew();
            // ** IMPORTANT ** you should always subscribe to System event
            _level2Client.System += message => { };
            _level2Client.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _level2Client.Disconnect();
        }

        [Test]
        public async Task Should_Receive_SummaryMessages_Snapshot()
        {
            // Act
            var summaryMessages = await _level2Client.GetSummarySnapshotAsync(Symbol);

            // Assert
            Assert.AreEqual(summaryMessages.First().Symbol, Symbol);
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Receive_Timestamp_When_Connected()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.Timestamp += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqServerConnect();

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Receive_Summary_When_ReqWatch_Symbol()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.Summary += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqWatch(Symbol);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        [Description("Ignore the test if market closed")]
        public void Should_Receive_Update_When_ReqWatch_Symbol()
        {
            var now = DateTime.Now;
            var marketOpen = new TimeSpan(9, 30, 00);
            var marketClose = new TimeSpan(16, 00, 00);
            if (now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday || now.TimeOfDay <= marketOpen || now.TimeOfDay > marketClose)
            {
                Assert.Ignore();
            }

            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.Update += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqWatch(Symbol);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Receive_Query_When_ReqMarketMakerNameById()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.Query += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqMarketMakerNameById(MarketMarkerId);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Receive_SymbolNotFound_When_Requesting_Invalid_Symbol()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.SymbolNotFound += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqWatch("INVALID_SYMBOL_NAME");

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Explicit("Only works if you don't have access to Level2!")]
        [Test, MaxTime(TimeoutMs)]
        public void Should_Receive_Error_When_Not_Having_Level2_Permission()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _level2Client.Error += message =>
            {
                eventRaised.Set();
            };

            // Act
            _level2Client.ReqWatch(Symbol);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }
    }
}