using System.Linq;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Symbol
{
    public class SymbolFacadeTests
    {
        private LookupClient _lookupClient;

        public SymbolFacadeTests()
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

        [Test]
        public void Should_Return_MarketSymbols_From_Sample_Archive_File()
        {
            // Act
            var marketSymbols = _lookupClient.Symbol.GetAllMarketSymbols(url: Settings.MarketSymbolsSampleUrl);

            // Assert
            Assert.True(marketSymbols.Count() == Settings.MarketSymbolsSampleCount);
        }

        [Test]
        public void Should_Return_ExpiredOptions_From_Sample_Archive_File()
        {
            // Act
            var expiredOptions = _lookupClient.Symbol.GetAllExpiredOptions(url: Settings.ExpiredOptionsSampleUrl);

            // Assert
            Assert.True(expiredOptions.Count() == Settings.ExpiredOptionsSampleCount);
        }

        [Test]
        public void Should_Return_Listed_Markets()
        {
            // Act
            var markets = _lookupClient.Symbol.ReqListedMarketsAsync().GetAwaiter().GetResult();

            // Assert
            Assert.True(markets.Count() > 0);
        }

        [Test]
        public void Should_Return_Security_Types()
        {
            // Act
            var securityTypes = _lookupClient.Symbol.ReqSecurityTypesAsync().GetAwaiter().GetResult();

            // Assert
            Assert.True(securityTypes.Count() > 0);
            Assert.True(securityTypes.Where(st => st.ShortName == "EQUITY").Count() == 1);
        }

        [Test]
        public void Should_Return_Equity_Symbols()
        {
            // Act
            var equitySymbols = _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "*", FilterType.SecurityTypes, new int[] { 1 })
                .GetAwaiter().GetResult();

            // Assert
            Assert.True(equitySymbols.Count() > 0);
            Assert.True(equitySymbols.Where(st => st.Symbol == "AAPL").Count() == 1);
        }

        [Test, Explicit]
        public void Should_Return_Distinctive_Properties_From_Archive_File()
        {
            var securityTypes = _lookupClient.Symbol.GetAllMarketSymbols().GroupBy(x => x.SecurityType).Select(x => x.First()).Select(x => x.SecurityType).ToList();
            var listedMarkets = _lookupClient.Symbol.GetAllMarketSymbols().GroupBy(x => x.ListedMarket).Select(x => x.First()).Select(x => x.ListedMarket).ToList();
            var exchanges = _lookupClient.Symbol.GetAllMarketSymbols().GroupBy(x => x.Exchange).Select(x => x.First()).Select(x => x.Exchange).ToList();
        }
    }
}