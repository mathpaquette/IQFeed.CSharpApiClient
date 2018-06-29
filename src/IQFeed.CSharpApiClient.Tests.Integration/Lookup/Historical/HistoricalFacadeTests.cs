using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";

        private LookupClient _lookupClient;

        public HistoricalFacadeTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _lookupClient = LookupClientFactory.CreateNew();
            _lookupClient.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _lookupClient.Disconnect();
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDatapointsAsync()
        {
            var tickMessages = await _lookupClient.Historical.ReqHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDaysAsync()
        {
            var tickMessages = await _lookupClient.Historical.ReqHistoryTickDaysAsync(Symbol, int.MaxValue, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickTimeframeAsync()
        {
            var tickMessages = await _lookupClient.Historical.ReqHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.Greater(tickMessages.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDatapointsAsync()
        {
            var intervalMessages = await _lookupClient.Historical.ReqHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDaysAsync()
        {
            var intervalMessages = await _lookupClient.Historical.ReqHistoryIntervalDaysAsync(Symbol, 5, int.MaxValue, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalTimeframeAsync()
        {
            var intervalMessages = await _lookupClient.Historical.ReqHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.Greater(intervalMessages.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.ReqHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyTimeframeAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.ReqHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.Greater(dailyWeeklyMonthlyMessages.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.ReqHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.ReqHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_Exception_When_Historical_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<Exception>(async () => await _lookupClient.Historical.ReqHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints));
        }
    }
}