using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalFacadeTests
    {
        private const int TimeoutMs = 10000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";

        private HistoricalFacade _historicalFacade;

        public HistoricalFacadeTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            var lookupClient = LookupClientFactory.CreateNew();
            lookupClient.Connect();

            _historicalFacade = lookupClient.Historical;
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDatapointsAsync()
        {
            var tickMessages = await _historicalFacade.ReqHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDaysAsync()
        {
            var tickMessages = await _historicalFacade.ReqHistoryTickDaysAsync(Symbol, int.MaxValue, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickTimeframeAsync()
        {
            var tickMessages = await _historicalFacade.ReqHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.Greater(tickMessages.Count(), 0);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDatapointsAsync()
        {
            var intervalMessages = await _historicalFacade.ReqHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDaysAsync()
        {
            var intervalMessages = await _historicalFacade.ReqHistoryIntervalDaysAsync(Symbol, 5, int.MaxValue, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalTimeframeAsync()
        {
            var intervalMessages = await _historicalFacade.ReqHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.Greater(intervalMessages.Count(), 0);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _historicalFacade.ReqHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyTimeframeAsync()
        {
            var dailyWeeklyMonthlyMessages = await _historicalFacade.ReqHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.Greater(dailyWeeklyMonthlyMessages.Count(), 0);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _historicalFacade.ReqHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _historicalFacade.ReqHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }
    }
}