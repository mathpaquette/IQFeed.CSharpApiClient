using System;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task Should_Return_ListedMarkets()
        {
            // Act
            var markets = await _lookupClient.Symbol.ReqListedMarketsAsync();

            // Assert
            Assert.True(markets.Count() > 0);
        }

        [Test]
        public async Task Should_Return_SecurityTypes()
        {
            // Act
            var securityTypes = await _lookupClient.Symbol.ReqSecurityTypesAsync();

            // Assert
            Assert.True(securityTypes.Count() > 0);
            Assert.True(securityTypes.Where(st => st.ShortName == "EQUITY").Count() == 1);
        }

        [Test]
        public async Task Should_Return_TradeConditions()
        {
            // Act
            var tradeConditions = await _lookupClient.Symbol.ReqTradeConditionsAsync();

            // Assert
            Assert.True(tradeConditions.Count() > 0);
            Assert.True(tradeConditions.Where(st => st.ShortName == "REGULAR").Count() == 1);
        }

        [Test]
        public async Task Should_Return_SicCodes()
        {
            // Act
            var sicCodes = await _lookupClient.Symbol.ReqSicCodesAsync();

            // Assert
            Assert.True(sicCodes.Count() > 0);
            Assert.AreEqual("ELECTRONIC COMPUTERS", sicCodes.Where(c => c.SicCode == 3571).Single().Description);
        }

        [Test]
        public async Task Should_Return_NaicsCodes()
        {
            // Act
            var naicsCodes = await _lookupClient.Symbol.ReqNaicsCodesAsync();

            // Assert
            Assert.True(naicsCodes.Count() > 0);
            Assert.AreEqual("Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing", 
                naicsCodes.Where(c => c.NaicsCode == 334220).Single().Description);            
        }

        [Test]
        public async Task Should_Return_EquitySymbols()
        {
            // Act
            var securityTypes = await _lookupClient.Symbol.ReqSecurityTypesAsync();
            var equitySecurityType = securityTypes
                .Where(t => t.ShortName == "EQUITY")
                .Select(t => t.SecurityTypeId)
                .Single();

            var equitySymbols = await _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "*", FilterType.SecurityTypes, new int[] { equitySecurityType });

            // Assert
            Assert.True(equitySymbols.Count() > 0);
            Assert.True(equitySymbols.Where(st => st.Symbol == "AAPL").Count() == 1);
        }

        [Test]
        public async Task Should_Return_EquityOption_Symbols()
        {
            // Act
            var securityTypes = await _lookupClient.Symbol.ReqSecurityTypesAsync();
            var equityOptionSecurityType = securityTypes.Where(t => t.ShortName == "IEOPTION")
                .Select(t => t.SecurityTypeId)
                .Single();

            var equitySymbols = await _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "AAPL", FilterType.SecurityTypes, new int[] { equityOptionSecurityType });

            // Assert
            Assert.True(equitySymbols.Count() > 0);
            Assert.AreEqual(equitySymbols.Count(), equitySymbols.Where(st => st.Symbol.StartsWith("AAPL")).Count());
        }

        [Test]
        public async Task Should_Return_Symbols_By_SicCode()
        {
            // Act
            var symbolsBySicCode = await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync("3571");

            // Assert
            Assert.True(symbolsBySicCode.Count() > 0);
            Assert.AreEqual(symbolsBySicCode.Count(), symbolsBySicCode.Where(st => st.SicCode.ToString().StartsWith("3571")).Count());
        }

        [Test]
        public void Should_Throw_Exceptions_When_Invalid_SicCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync(null));
            Assert.ThrowsAsync<ArgumentException>(async () => await _lookupClient.Symbol.ReqSymbolsBySicCodeAsync("1"));
        }

        [Test]
        public async Task Should_Return_Symbols_By_NaicsCode_And_RequestId()
        {
            // Act
            var symbolsByNaicsCode = await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync("33", "reqId2");

            // Assert
            Assert.True(symbolsByNaicsCode.Count() > 0);
            Assert.AreEqual(symbolsByNaicsCode.Count(), 
                symbolsByNaicsCode.Where(st => st.RequestId == "reqId2" && st.NaicsCode.ToString().StartsWith("33")).Count());
        }

        [Test]
        public void Should_Throw_Exceptions_When_Invalid_NaicsCodePrefix()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync(null));
            Assert.ThrowsAsync<ArgumentException>(async () => await _lookupClient.Symbol.ReqSymbolsByNaicsCodeAsync("2"));
        }

        [Test]
        public async Task Should_Not_Find_Missing_Equity_Symbol()
        {
            // Act
            var equitySymbols = await _lookupClient.Symbol
                .ReqSymbolsByFilterAsync(FieldToSearch.Symbols, "GZZZZZZ", FilterType.SecurityTypes, new int[] { 1 });

            // Assert
            Assert.True(equitySymbols.Count() == 0);
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