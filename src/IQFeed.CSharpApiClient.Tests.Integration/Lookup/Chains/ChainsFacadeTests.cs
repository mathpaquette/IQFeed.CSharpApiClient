using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Chains
{
    public class ChainsFacadeTests
    {
        private const int TimeoutMs = 30000;
        private const string EquitySymbol = "SPY";
        private const string FutureSymbol = "@ES";

        private ChainsFacade _chainsFacade;

        public ChainsFacadeTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            var lookupClient = LookupClientFactory.CreateNew();
            lookupClient.Connect();

            _chainsFacade = lookupClient.Chains;
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_Strings_When_ReqChainFutureAsync()
        {
            var stringMessages = await _chainsFacade.ReqChainFutureAsync(FutureSymbol, string.Empty, "1819", 24);
            Assert.IsInstanceOf<string>(stringMessages.First());
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_Strings_When_ReqChainFutureSpreadsAsync()
        {
            var stringMessages = await _chainsFacade.ReqChainFutureSpreadsAsync(FutureSymbol, string.Empty, "1819", 24);
            Assert.IsInstanceOf<string>(stringMessages.First());
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_Strings_When_ReqChainFutureOptionAsync()
        {
            var stringMessages = await _chainsFacade.ReqChainFutureOptionAsync(FutureSymbol, OptionSideFilterType.CP, string.Empty, "18", 12);
            Assert.IsInstanceOf<string>(stringMessages.First());
        }

        [Test, Timeout(TimeoutMs)]
        public async Task Should_Return_EquityIndexOptions_When_ReqChainIndexEquityOptionAsync()
        {
            var equityIndexOptions = await _chainsFacade.ReqChainIndexEquityOptionAsync(EquitySymbol, OptionSideFilterType.CP, string.Empty, 4);
            Assert.IsInstanceOf<EquityOptionMessage>(equityIndexOptions.First());
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Throw_Exception_When_Chains_Getting_Error()
        {
            var ex = Assert.ThrowsAsync<Exception>(async () => await _chainsFacade.ReqChainIndexEquityOptionAsync("INVALID_SYMBOL_NAME", OptionSideFilterType.CP, string.Empty, 4));
        }
    }
}