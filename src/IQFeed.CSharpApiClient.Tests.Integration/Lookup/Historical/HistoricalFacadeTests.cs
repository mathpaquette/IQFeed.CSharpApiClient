using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";
        private const string RequestId = "TEST";

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

        // Tick
        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDatapointsAsync()
        {
            var tickMessages = await _lookupClient.Historical.GetHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_With_RequestId_When_ReqHistoryTickDatapointsAsync_Using_RequestId()
        {
            var tickMessages = await _lookupClient.Historical.GetHistoryTickDatapointsAsync(Symbol, Datapoints, requestId: RequestId);
            Assert.AreEqual(tickMessages.First().RequestId, RequestId);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickDaysAsync()
        {
            var tickMessages = await _lookupClient.Historical.GetHistoryTickDaysAsync(Symbol, Int16.MaxValue, Datapoints);
            Assert.AreEqual(tickMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_TickMessages_When_ReqHistoryTickTimeframeAsync()
        {
            var tickMessages = await _lookupClient.Historical.GetHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.Greater(tickMessages.Count(), 0);
        }

        // Interval
        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDatapointsAsync()
        {
            var intervalMessages = await _lookupClient.Historical.GetHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_With_RequestId_When_ReqHistoryIntervalDatapointsAsync_Using_RequestId()
        {
            var intervalMessages = await _lookupClient.Historical.GetHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints, requestId: RequestId);
            Assert.AreEqual(intervalMessages.First().RequestId, RequestId);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalDaysAsync()
        {
            // Days > Int16.MaxValue is illegal in protocol 6.1
            var intervalMessages = await _lookupClient.Historical.GetHistoryIntervalDaysAsync(Symbol, 5, Int16.MaxValue, Datapoints);
            Assert.AreEqual(intervalMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_IntervalMessages_When_ReqHistoryIntervalTimeframeAsync()
        {
            var intervalMessages = await _lookupClient.Historical.GetHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.Greater(intervalMessages.Count(), 0);
        }

        // Daily
        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.GetHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_With_RequestId_When_ReqHistoryDailyDatapointsAsync_Using_RequestId()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.GetHistoryDailyDatapointsAsync(Symbol, Datapoints, requestId: RequestId);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.First().RequestId, RequestId);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryDailyTimeframeAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.GetHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.Greater(dailyWeeklyMonthlyMessages.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.GetHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_DailyWeeklyMonthlyMessages_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var dailyWeeklyMonthlyMessages = await _lookupClient.Historical.GetHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.AreEqual(dailyWeeklyMonthlyMessages.Count(), Datapoints);
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_NoDataIQFeedException_When_Historical_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(
                async () => await _lookupClient.Historical.GetHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints));
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_NoDataIQFeedException_When_Historical_With_RequestId_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(
                async () => await _lookupClient.Historical.GetHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints, requestId: "zzz"));
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_IQFeedException_When_Historical_With_Illegal_Argument_Getting_Error()
        {
            // let's try to break the protocol
            // we need to add argument validation for all requests - this should ideally throw ArgumentException
            // and we should do the checks before sending wrong protocol messages to IQ Feed
            // no string parameter should contain commas...
            var ex = Assert.ThrowsAsync<IQFeedException>(
                async () => await _lookupClient.Historical.GetHistoryTickDatapointsAsync("INVALID,SYMBOL,NAME", Datapoints));
        }
    }
}