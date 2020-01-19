using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.Symbol
{
    public class SymbolFacadeTests
    {
        private LookupClient<decimal> _lookupClient;

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
        public async Task Should_Return_ListedMarkets()
        {
            // Act
            var markets = await _lookupClient.Symbol.ReqListedMarketsAsync();

            // Assert
            Assert.Greater(markets.Count(), 0);
        }

        [Test]
        public async Task Should_Return_SecurityTypes()
        {
            // Act
            var securityTypes = await _lookupClient.Symbol.ReqSecurityTypesAsync();

            // Assert
            Assert.AreEqual(1, securityTypes.Where(st => st.ShortName == "EQUITY").Count());
        }

        [Test]
        public async Task Should_Return_TradeConditions()
        {
            // Act
            var tradeConditions = await _lookupClient.Symbol.ReqTradeConditionsAsync();

            // Assert
            Assert.AreEqual(1, tradeConditions.Where(st => st.ShortName == "REGULAR").Count());
        }

        [Test]
        public async Task Should_Return_SicCodes()
        {
            // Act
            var sicCodes = await _lookupClient.Symbol.ReqSicCodesAsync();

            // Assert
            Assert.AreEqual("ELECTRONIC COMPUTERS", sicCodes.Where(c => c.SicCode == 3571).Single().Description);
        }

        [Test]
        public async Task Should_Return_NaicsCodes()
        {
            // Act
            var naicsCodes = await _lookupClient.Symbol.ReqNaicsCodesAsync();

            // Assert
            Assert.AreEqual("Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing", 
                naicsCodes.Where(c => c.NaicsCode == 334220).Single().Description);            
        }

        [Test]
        public async Task Should_Return_EquitySymbols()
        {
            // Act
            var equitySymbols = await _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "AA", FilterType.SecurityTypes, new int[] { 1 });

            // Assert
            Assert.AreEqual(1, equitySymbols.Where(st => st.Symbol == "AAPL").Count());
        }

        [Test]
        public async Task Should_Return_EquityOption_Symbols()
        {
            // Act
            var equitySymbols = await _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "AAPL", FilterType.SecurityTypes, new int[] { 2 });

            // Assert
            Assert.AreEqual(equitySymbols.Count(), equitySymbols.Where(st => st.Symbol.StartsWith("AAPL")).Count());
        }

        [Test]
        public async Task Should_Return_Symbols_By_SicCode()
        {
            // Act
            var symbolsBySicCode = await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync("3571");

            // Assert
            Assert.AreEqual(symbolsBySicCode.Count(), symbolsBySicCode.Where(st => st.SicCode.ToString().StartsWith("3571")).Count());
        }

        [Test]
        public void Should_Throw_Exceptions_When_Null_SicCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync(null));            
        }

        [Test]
        public void Should_Throw_Exceptions_When_Short_SicCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync("1"));
        }

        [Test]
        public async Task Should_Return_Symbols_By_NaicsCode_And_RequestId()
        {
            // Act
            var symbolsByNaicsCode = await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync("33", "reqId2");

            // Assert
            Assert.AreEqual(symbolsByNaicsCode.Count(), 
                symbolsByNaicsCode.Where(st => st.RequestId == "reqId2" && st.NaicsCode.ToString().StartsWith("33")).Count());
        }

        [Test]
        public void Should_Throw_Exceptions_When_Null_NaicsCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync(null));
        }

        [Test]
        public void Should_Throw_Exceptions_When_Short_NaicsCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync("2"));
        }

        [Test]
        public void Should_Throw_NoDataIQFeedException_For_Missing_Equity_Symbol()
        {
            var ex = Assert.ThrowsAsync<NoDataIQFeedException>(
                async () => await _lookupClient.Symbol.ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "GZZZZZZ", FilterType.SecurityTypes, new int[] { 1 }));
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