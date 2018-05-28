using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalRawFacadeTests
    {
        private const int TimeoutMs = 10000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";

        private HistoricalRawFacade _historicalRawFacade;

        public HistoricalRawFacadeTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            var lookupClient = LookupClientFactory.CreateNew();
            lookupClient.Connect();

            _historicalRawFacade = lookupClient.Historical.Raw;
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickDatapointsAsync()
        {
            var rawTickMessage = await _historicalRawFacade.ReqHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickDaysAsync()
        {
            var rawTickMessage = await _historicalRawFacade.ReqHistoryTickDaysAsync(Symbol, int.MaxValue, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickTimeframeAsync()
        {
            var rawTickMessage = await _historicalRawFacade.ReqHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.IsNotEmpty(rawTickMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDatapointsAsync()
        {
            var rawIntervalMessage = await _historicalRawFacade.ReqHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDaysAsync()
        {
            var rawIntervalMessage = await _historicalRawFacade.ReqHistoryIntervalDaysAsync(Symbol, 5, int.MaxValue, Datapoints);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalTimeframeAsync()
        {
            var rawIntervalMessage = await _historicalRawFacade.ReqHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.IsNotEmpty(rawIntervalMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _historicalRawFacade.ReqHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyTimeframeAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _historicalRawFacade.ReqHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _historicalRawFacade.ReqHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var rawDailyWeeklyMonthlyMessage = await _historicalRawFacade.ReqHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(rawDailyWeeklyMonthlyMessage);
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Throw_Exception_When_Historical_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<Exception>(async () => await _historicalRawFacade.ReqHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints));
        }
    }
}