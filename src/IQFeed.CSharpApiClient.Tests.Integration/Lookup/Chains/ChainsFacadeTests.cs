using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Chains
{
    [TestFixture]
    public class ChainsFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const string EquitySymbol = "EBAY";
        private const string FutureSymbol = "@ES";

        private LookupClient _lookupClient;

        public ChainsFacadeTests()
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
        public async Task Should_Return_Futures_When_ReqChainFutureAsync()
        {
            var futureMessages = await _lookupClient.Chains.ReqChainFutureAsync(FutureSymbol, string.Empty, "1819", 24);
            Assert.IsInstanceOf<FutureMessage>(futureMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_FutureSpreads_When_ReqChainFutureSpreadsAsync()
        {
            var futureSpreadMessages = await _lookupClient.Chains.ReqChainFutureSpreadsAsync(FutureSymbol, string.Empty, "1819", 24);
            Assert.IsInstanceOf<FutureSpreadMessage>(futureSpreadMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_FutureOptions_When_ReqChainFutureOptionAsync()
        {
            var futureOptionMessages = await _lookupClient.Chains.ReqChainFutureOptionAsync(FutureSymbol, OptionSideFilterType.CP, string.Empty, "18", 12);
            Assert.IsInstanceOf<FutureOptionMessage>(futureOptionMessages.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_EquityIndexOptions_When_ReqChainIndexEquityOptionAsync()
        {
            var equityIndexOptions = await _lookupClient.Chains.ReqChainIndexEquityOptionAsync(EquitySymbol, OptionSideFilterType.CP, string.Empty, 4);
            Assert.IsInstanceOf<EquityOptionMessage>(equityIndexOptions.First());
        }

        [Test, MaxTime(TimeoutMs)]
        public void Should_Throw_Exception_When_Chains_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<Exception>(async () => await _lookupClient.Chains.ReqChainIndexEquityOptionAsync("INVALID_SYMBOL_NAME", OptionSideFilterType.CP, string.Empty, 4));
        }
    }
}