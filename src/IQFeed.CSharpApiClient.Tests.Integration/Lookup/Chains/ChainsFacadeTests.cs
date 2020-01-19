using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Chains
{
    [TestFixture]
    public class ChainsFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const string EquitySymbol = "EBAY";
        private const string FutureSymbol = "@ES";

        private LookupClient<decimal> _lookupClient;
        private readonly string _years;

        public ChainsFacadeTests()
        {
            IQFeedLauncher.Start();
            _years = $"{DateTime.Now.Date:yy}{DateTime.Now.Date.AddYears(1):yy}";
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
        public async Task Should_Return_Futures_When_ReqChainFutureAsync()
        {
            var futureMessages = await _lookupClient.Chains.ReqChainFutureAsync(FutureSymbol, string.Empty, _years, 24);
            Assert.IsInstanceOf<Future>(futureMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_FutureSpreads_When_ReqChainFutureSpreadsAsync()
        {
            var futureSpreadMessages = await _lookupClient.Chains.ReqChainFutureSpreadsAsync(FutureSymbol, string.Empty, _years, 24);
            Assert.IsInstanceOf<FutureSpread>(futureSpreadMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_FutureOptions_When_ReqChainFutureOptionAsync()
        {
            var futureOptionMessages = await _lookupClient.Chains.ReqChainFutureOptionAsync(FutureSymbol, OptionSideFilterType.CP, string.Empty, _years, 12);
            Assert.IsInstanceOf<FutureOption>(futureOptionMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_EquityIndexOptions_When_ReqChainIndexEquityOptionAsync()
        {
            var equityIndexOptions = await _lookupClient.Chains.ReqChainIndexEquityOptionAsync(EquitySymbol, OptionSideFilterType.CP, string.Empty, 4);
            Assert.IsInstanceOf<EquityOption>(equityIndexOptions.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_IQFeedException_When_Chains_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(
                async () => await _lookupClient.Chains.ReqChainIndexEquityOptionAsync("INVALID_SYMBOL_NAME", OptionSideFilterType.CP, string.Empty, 4));
        }
    }
}