using System.Linq;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Symbol
{
    public class SymbolFacadeTests
    {
        private SymbolFacade _symbolFacade;

        [SetUp]
        public void SetUp()
        {
            var lookupClient = LookupClientFactory.CreateNew();
            _symbolFacade = lookupClient.Symbol;
        }

        [Test]
        public void Should_Return_MarketSymbols_From_Sample_Archive_File()
        {
            // Act
            var marketSymbols = _symbolFacade.GetAllMarketSymbols(marketSymbolsArchiveUrl: Settings.MarketSymbolsSampleUrl);

            // Assert
            Assert.True(marketSymbols.Count() == Settings.MarketSymbolsSampleCount);
        }

        [Test, Explicit]
        public void Should_Return_Distinctive_Properties_From_Archive_File()
        {
            var securityTypes = _symbolFacade.GetAllMarketSymbols().GroupBy(x => x.SecurityType).Select(x => x.First()).Select(x => x.SecurityType).ToList();
            var listedMarkets = _symbolFacade.GetAllMarketSymbols().GroupBy(x => x.ListedMarket).Select(x => x.First()).Select(x => x.ListedMarket).ToList();
            var exchanges = _symbolFacade.GetAllMarketSymbols().GroupBy(x => x.Exchange).Select(x => x.First()).Select(x => x.Exchange).ToList();
        }
    }
}