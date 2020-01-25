using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalRawFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";

        private LookupClient<decimal> _lookupClient;

        public HistoricalRawFacadeTests()
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
        public async Task Should_Return_String_When_ReqHistoryTickDatapointsAsync()
        {
            var rawTickMessage = await _lookupClient.Historical.Raw.GetHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickDaysAsync()
        {
            var rawTickMessage = await _lookupClient.Historical.Raw.GetHistoryTickDaysAsync(Symbol, int.MaxValue, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickTimeframeAsync()
        {
            var rawTickMessage = await _lookupClient.Historical.Raw.GetHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDatapointsAsync()
        {
            var rawIntervalMessage = await _lookupClient.Historical.Raw.GetHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDaysAsync()
        {
            var rawIntervalMessage = await _lookupClient.Historical.Raw.GetHistoryIntervalDaysAsync(Symbol, 5, int.MaxValue, Datapoints);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalTimeframeAsync()
        {
            var rawIntervalMessage = await _lookupClient.Historical.Raw.GetHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _lookupClient.Historical.Raw.GetHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyTimeframeAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _lookupClient.Historical.Raw.GetHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _lookupClient.Historical.Raw.GetHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _lookupClient.Historical.Raw.GetHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_NoDataIQFeedException_When_Historical_Has_No_Data()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(async () => await _lookupClient.Historical.Raw.GetHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints));
        }
    }
}