using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;
using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    public class Level1DynamicFieldsTests
    {
        private const double DoubleValue = 1234.5678;
        private const int IntegerValue = 12345678;
        private const string TimeSpanStringValue = "09:30:00.123456";
        private const string StringValue = "STRINGVALUE";
        private const string DateTimeStringValue = "06/15/2020";
        private readonly TimeSpan _timeSpanValue = DateTime.ParseExact(TimeSpanStringValue, "HH:mm:ss.ffffff", CultureInfo.InvariantCulture, DateTimeStyles.None).TimeOfDay;
        private readonly DateTime _dateTimeValue = DateTime.ParseExact(DateTimeStringValue, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

        [Test]
        public void Should_Throw_Exception_When_Symbol_Is_Not_First_Field_Requested()
        {
            // Arrange
            var level1DynamicHandler = new Level1MessageDynamicHandler();
            var fields = new[]
            {
                DynamicFieldset.TotalVolume,
                DynamicFieldset.Symbol
            };

            // Assert
            Assert.Throws<ArgumentException>(() => level1DynamicHandler.SetDynamicFields(fields));
        }

        [Test]
        public void Should_Throw_Exception_When_Dynamic_Field_Type_Requested()
        {
            // Arrange
            var level1DynamicHandler = new Level1MessageDynamicHandler();
            var fields = new[]
            {
                DynamicFieldset.Symbol,
                DynamicFieldset.Type
            };

            // Assert
            Assert.Throws<ArgumentException>( () => level1DynamicHandler.SetDynamicFields(fields));
        }

        [Test]
        public void Should_Convert_Type_Summary()
        {
            // Arrange
            var message = $"P,DONT_CARE_SYMBOL";
            var fields = new[] { DynamicFieldset.Symbol };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Type, "P");
        }

        [Test]
        public void Should_Convert_Type_Update()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL";
            var fields = new[] { DynamicFieldset.Symbol };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Type, "Q");
        }

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
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Ask };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Ask, DoubleValue);
        }

        [Test]
        public void Should_Convert_AskChange()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AskChange };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AskChange, DoubleValue);
        }

        [Test]
        public void Should_Convert_AskMarketCenter()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AskMarketCenter };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AskMarketCenter, IntegerValue);
        }

        [Test]
        public void Should_Convert_AskSize()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AskSize };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AskSize, IntegerValue);
        }

        [Test]
        public void Should_Convert_AskTime()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{TimeSpanStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AskTime };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AskTime, _timeSpanValue);
        }

        [Test]
        public void Should_Convert_AvailableRegions()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AvailableRegions };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AvailableRegions, StringValue);
        }

        [Test]
        public void Should_Convert_AverageMaturity()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.AverageMaturity };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.AverageMaturity, DoubleValue);
        }

        [Test]
        public void Should_Convert_Bid()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Bid };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Bid, DoubleValue);
        }

        [Test]
        public void Should_Convert_BidChange()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.BidChange };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.BidChange, DoubleValue);
        }

        [Test]
        public void Should_Convert_BidMarketCenter()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.BidMarketCenter };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.BidMarketCenter, IntegerValue);
        }

        [Test]
        public void Should_Convert_BidSize()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.BidSize };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.BidSize, IntegerValue);
        }

        [Test]
        public void Should_Convert_BidTime()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{TimeSpanStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.BidTime };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.BidTime, _timeSpanValue);
        }

        [Test]
        public void Should_Convert_Change()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Change };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Change, DoubleValue);
        }

        [Test]
        public void Should_Convert_ChangeFromOpen()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ChangeFromOpen };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ChangeFromOpen, DoubleValue);
        }

        [Test]
        public void Should_Convert_Close()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Close };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Close, DoubleValue);
        }

        [Test]
        public void Should_Convert_CloseRange1()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.CloseRange1 };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.CloseRange1, DoubleValue);
        }

        [Test]
        public void Should_Convert_CloseRange2()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.CloseRange2 };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.CloseRange2, DoubleValue);
        }

        [Test]
        public void Should_Convert_DaysToExpiration()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.DaysToExpiration };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.DaysToExpiration, StringValue);
        }

        [Test]
        public void Should_Convert_DecimalPrecision()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.DecimalPrecision };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.DecimalPrecision, StringValue);
        }

        [Test]
        public void Should_Convert_Delay()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Delay };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Delay, IntegerValue);
        }

        [Test]
        public void Should_Convert_ExchangeID()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExchangeID };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExchangeID, StringValue);
        }

        [Test]
        public void Should_Convert_ExtendedTrade()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTrade };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTrade, DoubleValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradeDate()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DateTimeStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradeDate };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradeDate, _dateTimeValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradeMarketCenter()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradeMarketCenter };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradeMarketCenter, IntegerValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradeSize()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradeSize };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradeSize, IntegerValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradeTime()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{TimeSpanStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradeTime };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradeTime, _timeSpanValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradingChange()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradingChange };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradingChange, DoubleValue);
        }

        [Test]
        public void Should_Convert_ExtendedTradingDifference()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.ExtendedTradingDifference };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.ExtendedTradingDifference, DoubleValue);
        }

        [Test]
        public void Should_Convert_FinancialStatusIndicator()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.FinancialStatusIndicator };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.FinancialStatusIndicator, StringValue);
        }

        [Test]
        public void Should_Convert_FractionDisplayCode()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.FractionDisplayCode };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.FractionDisplayCode, StringValue);
        }

        [Test]
        public void Should_Convert_High()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.High };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.High, DoubleValue);
        }

        [Test]
        public void Should_Convert_Last()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Last };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Last, DoubleValue);
        }

        [Test]
        public void Should_Convert_LastDate()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DateTimeStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.LastDate };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.LastDate, _dateTimeValue);
        }

        [Test]
        public void Should_Convert_LastMarketCenter()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.LastMarketCenter };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.LastMarketCenter, IntegerValue);
        }

        [Test]
        public void Should_Convert_LastSize()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.LastSize };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.LastSize, IntegerValue);
        }

        [Test]
        public void Should_Convert_LastTime()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{TimeSpanStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.LastTime };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.LastTime, _timeSpanValue);
        }

        [Test]
        public void Should_Convert_Low()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Low };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Low, DoubleValue);
        }

        [Test]
        public void Should_Convert_MarketCapitalization()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MarketCapitalization };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MarketCapitalization, DoubleValue);
        }

        [Test]
        public void Should_Convert_MarketOpen()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MarketOpen };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MarketOpen, IntegerValue);
        }

        [Test]
        public void Should_Convert_MessageContents()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MessageContents };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MessageContents, StringValue);
        }

        [Test]
        public void Should_Convert_MostRecentTrade()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTrade };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTrade, DoubleValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeAggressor()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeAggressor };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeAggressor, IntegerValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeConditions()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeConditions };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeConditions, StringValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeDate()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DateTimeStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeDate };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeDate, _dateTimeValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeDayCode()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeDayCode };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeDayCode, IntegerValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeMarketCenter()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeMarketCenter };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeMarketCenter, IntegerValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeSize()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeSize };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeSize, IntegerValue);
        }

        [Test]
        public void Should_Convert_MostRecentTradeTime()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{TimeSpanStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.MostRecentTradeTime };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.MostRecentTradeTime, _timeSpanValue);
        }

        [Test]
        public void Should_Convert_NetAssetValue()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.NetAssetValue };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.NetAssetValue, DoubleValue);
        }

        [Test]
        public void Should_Convert_NumberOfTradesToday()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.NumberOfTradesToday };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.NumberOfTradesToday, IntegerValue);
        }

        [Test]
        public void Should_Convert_Open()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Open };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Open, DoubleValue);
        }

        [Test]
        public void Should_Convert_OpenInterest()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.OpenInterest };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.OpenInterest, IntegerValue);
        }

        [Test]
        public void Should_Convert_OpenRange1()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.OpenRange1 };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.OpenRange1, DoubleValue);
        }

        [Test]
        public void Should_Convert_OpenRange2()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.OpenRange2 };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.OpenRange2, DoubleValue);
        }

        [Test]
        public void Should_Convert_PercentChange()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.PercentChange };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.PercentChange, DoubleValue);
        }

        [Test]
        public void Should_Convert_PercentOffAverageVolume()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.PercentOffAverageVolume };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.PercentOffAverageVolume, DoubleValue);
        }

        [Test]
        public void Should_Convert_PreviousDayVolume()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.PreviousDayVolume };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.PreviousDayVolume, IntegerValue);
        }

        [Test]
        public void Should_Convert_PriceEarningsRatio()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.PriceEarningsRatio };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.PriceEarningsRatio, DoubleValue);
        }

        [Test]
        public void Should_Convert_Range()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Range };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Range, DoubleValue);
        }

        [Test]
        public void Should_Convert_RestrictedCode()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{StringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.RestrictedCode };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.RestrictedCode, StringValue);
        }

        [Test]
        public void Should_Convert_Settle()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Settle };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Settle, DoubleValue);
        }

        [Test]
        public void Should_Convert_SettlementDate()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DateTimeStringValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.SettlementDate };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.SettlementDate, _dateTimeValue);
        }

        [Test]
        public void Should_Convert_Spread()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Spread };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Spread, DoubleValue);
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
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Tick };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Tick, IntegerValue);
        }

        [Test]
        public void Should_Convert_TickID()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.TickID };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.TickID, IntegerValue);
        }

        [Test]
        public void Should_Convert_TotalVolume()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{IntegerValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.TotalVolume };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.TotalVolume, IntegerValue);
        }

        [Test]
        public void Should_Convert_Volatility()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.Volatility };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.Volatility, DoubleValue);
        }

        [Test]
        public void Should_Convert_VWAP()
        {
            // Arrange
            var message = $"Q,DONT_CARE_SYMBOL,{DoubleValue}";
            var fields = new[] { DynamicFieldset.Symbol, DynamicFieldset.VWAP };

            // Act
            var dynamicFields = Level1DynamicFields.Parse(message, fields);

            // Assert
            Assert.AreEqual(dynamicFields.VWAP, DoubleValue);
        }
    }
}