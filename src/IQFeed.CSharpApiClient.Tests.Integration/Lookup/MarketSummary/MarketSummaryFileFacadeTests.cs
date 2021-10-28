using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.MarketSummary
{
    public class MarketSummaryFacadeFileTests
    {
        private const int TimeoutMs = 30000;
        private const SecurityType Security_Type = SecurityType.FOPTION;
        //private const int GroupId = 31; //DOW JONES
        private const int GroupId = 34; //CME
        private const string RequestId = "TEST";

        private LookupClient _lookupClient;
        private int _groupId;

        public MarketSummaryFacadeFileTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public async Task SetUp()
        {
            _lookupClient = LookupClientFactory.CreateNew();
            _lookupClient.Connect();
            var groupIds = await _lookupClient.Symbol.GetListedMarketsAsync("TEST"); // using ReqId as workaround for IQFeed 6.2 beta bug
            _groupId = groupIds.First(g => g.ShortName == "CME").ListedMarketId;
        }

        [TearDown]
        public void TearDown()
        {
            _lookupClient.Disconnect();
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_GetEndOfDaySummaryAsync()
        {
            var tmpFilename = await _lookupClient.MarketSummary.File.GetEndOfDaySummaryAsync(Security_Type, GroupId, new DateTime(2021, 04, 01));
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_GetFundamentalSummaryAsync()
        {
            var tmpFilename = await _lookupClient.MarketSummary.File.GetEndOfDayFundamentalSummaryAsync(Security_Type, GroupId, new DateTime(2020, 04, 06));
            Assert.IsNotEmpty(tmpFilename);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_String_When_Get5MinuteSummaryAsync()
        {
            var tmpFilename = await _lookupClient.MarketSummary.File.Get5MinuteSnapshotSummaryAsync(Security_Type, _groupId);
            Assert.IsNotEmpty(tmpFilename);
        }
    }
}