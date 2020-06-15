using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    public class Level1DynamicFieldsTests
    {
        private const double DoubleValue = 1234.5678;
        private const int IntegerValue = 12345678;

        [Test]
        public void Should_Convert_SevenDayYield()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.SevenDayYield };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.SevenDayYield, DoubleValue);
        }

        [Test]
        public void Should_Convert_Ask()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AskChange()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AskMarketCenter()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AskSize()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AskTime()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AvailableRegions()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_AverageMaturity()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Bid()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_BidChange()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_BidMarketCenter()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_BidSize()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_BidTime()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Change()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ChangeFromOpen()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Close()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_CloseRange1()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_CloseRange2()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_DaysToExpiration()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_DecimalPrecision()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Delay()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExchangeID()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTrade()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradeDate()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradeMarketCenter()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradeSize()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradeTime()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradingChange()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_ExtendedTradingDifference()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_FinancialStatusIndicator()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_FractionDisplayCode()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_High()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Last()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_LastDate()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_LastMarketCenter()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_LastSize()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_LastTime()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_LastTradeDate()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Low()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MarketCapitalization()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MarketOpen()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MessageContents()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTrade()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTradeConditions()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTradeDate()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTradeMarketCenter()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTradeSize()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_MostRecentTradeTime()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_NetAssetValue()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_NumberOfTradesToday()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Open()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_OpenInterest()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_OpenRange1()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_OpenRange2()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_PercentChange()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_PercentOffAverageVolume()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_PreviousDayVolume()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_PriceEarningsRatio()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Range()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_RestrictedCode()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Settle()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_SettlementDate()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Spread()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Symbol()
        {
            // Arrange
            var message = "Q,AAPL";
            var fields = new[] { DynamicFieldset.Symbol };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Symbol, "AAPL");
        }

        [Test]
        public void Should_Convert_Tick()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_TickID()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_TotalVolume()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Type()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_Volatility()
        {
            // TODO
        }

        [Test]
        public void Should_Convert_VWAP()
        {
            // TODO
        }
    }
}