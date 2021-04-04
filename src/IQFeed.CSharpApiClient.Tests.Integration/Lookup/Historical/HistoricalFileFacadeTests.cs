using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Historical
{
    public class HistoricalFileFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const int Datapoints = 100;
        private const string Symbol = "AAPL";

        private LookupClient _lookupClient;

        public HistoricalFileFacadeTests()
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
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryTickDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickDaysAsync()
        {
            // 6.1 protocol only allows Int16.MaxValue days to be requested
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryTickDaysAsync(Symbol, Int16.MaxValue, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryTickTimeframeAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryTickTimeframeAsync(Symbol, null, DateTime.Now.Date, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDatapointsAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryIntervalDatapointsAsync(Symbol, 5, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalDaysAsync()
        {
            // 6.1 protocol only allows Int16.MaxValue days to be requested
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryIntervalDaysAsync(Symbol, 5, Int16.MaxValue, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryIntervalTimeframeAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryIntervalTimeframeAsync(Symbol, 5, null, DateTime.Now.Date);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyDatapointsAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryDailyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryDailyTimeframeAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryDailyTimeframeAsync(Symbol, null, DateTime.Today.Date);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryWeeklyDatapointsAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryWeeklyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_ReqHistoryMonthlyDatapointsAsync()
        {
            var tmpFilename = await _lookupClient.Historical.File.GetHistoryMonthlyDatapointsAsync(Symbol, Datapoints);
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_NoDataIQFeedException_When_Historical_Has_No_Data()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(async () => await _lookupClient.Historical.File.GetHistoryTickDatapointsAsync("INVALID_SYMBOL_NAME", Datapoints));
        }
    }
}