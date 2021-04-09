using System;
using System.Reflection;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Dynamic.Messages
{
    public class UpdateSummaryDynamicMessageTypesFactoryTests
    {
        #region Constants

        private static readonly DynamicFieldset[] PartialFields = new DynamicFieldset[]
        {
            DynamicFieldset.Symbol,
            DynamicFieldset.MostRecentTrade,
            DynamicFieldset.MostRecentTradeSize,
            DynamicFieldset.MostRecentTradeDate,
            DynamicFieldset.MostRecentTradeTime,
            DynamicFieldset.MostRecentTradeAggressor
        };

        private static readonly DynamicFieldset[] AllFields = new DynamicFieldset[]
        {
            DynamicFieldset.Symbol,
            DynamicFieldset.SevenDayYield,
            DynamicFieldset.Ask,
            DynamicFieldset.AskChange,
            DynamicFieldset.AskMarketCenter,
            DynamicFieldset.AskSize,
            DynamicFieldset.AskTime,
            DynamicFieldset.AvailableRegions,
            DynamicFieldset.AverageMaturity,
            DynamicFieldset.Bid,
            DynamicFieldset.BidChange,
            DynamicFieldset.BidMarketCenter,
            DynamicFieldset.BidSize,
            DynamicFieldset.BidTime,
            DynamicFieldset.Change,
            DynamicFieldset.ChangeFromOpen,
            DynamicFieldset.Close,
            DynamicFieldset.CloseRange1,
            DynamicFieldset.CloseRange2,
            DynamicFieldset.DaysToExpiration,
            DynamicFieldset.DecimalPrecision,
            DynamicFieldset.Delay,
            DynamicFieldset.ExchangeID,
            DynamicFieldset.ExtendedTrade,
            DynamicFieldset.ExtendedTradeDate,
            DynamicFieldset.ExtendedTradeMarketCenter,
            DynamicFieldset.ExtendedTradeSize,
            DynamicFieldset.ExtendedTradeTime,
            DynamicFieldset.ExtendedTradingChange,
            DynamicFieldset.ExtendedTradingDifference,
            DynamicFieldset.FinancialStatusIndicator,
            DynamicFieldset.FractionDisplayCode,
            DynamicFieldset.High,
            DynamicFieldset.Last,
            DynamicFieldset.LastDate,
            DynamicFieldset.LastMarketCenter,
            DynamicFieldset.LastSize,
            DynamicFieldset.LastTime,
            DynamicFieldset.Low,
            DynamicFieldset.MarketCapitalization,
            DynamicFieldset.MarketOpen,
            DynamicFieldset.MessageContents,
            DynamicFieldset.MostRecentTrade,
            DynamicFieldset.MostRecentTradeAggressor,
            DynamicFieldset.MostRecentTradeConditions,
            DynamicFieldset.MostRecentTradeDate,
            DynamicFieldset.MostRecentTradeDayCode,
            DynamicFieldset.MostRecentTradeMarketCenter,
            DynamicFieldset.MostRecentTradeSize,
            DynamicFieldset.MostRecentTradeTime,
            DynamicFieldset.NetAssetValue,
            DynamicFieldset.NumberOfTradesToday,
            DynamicFieldset.Open,
            DynamicFieldset.OpenInterest,
            DynamicFieldset.OpenRange1,
            DynamicFieldset.OpenRange2,
            DynamicFieldset.PercentChange,
            DynamicFieldset.PercentOffAverageVolume,
            DynamicFieldset.PreviousDayVolume,
            DynamicFieldset.PriceEarningsRatio,
            DynamicFieldset.Range,
            DynamicFieldset.RestrictedCode,
            DynamicFieldset.Settle,
            DynamicFieldset.SettlementDate,
            DynamicFieldset.Spread,
            DynamicFieldset.Tick,
            DynamicFieldset.TickID,
            DynamicFieldset.TotalVolume,
            DynamicFieldset.Volatility,
            DynamicFieldset.VWAP,
        };

        #endregion Constants

        #region Partial Fields Tests

        [Test]
        public void Should_Generate_IUpdateSummaryMessage_Implementiation_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance = Activator.CreateInstance(updateSummaryMessageType);

            // Assert
            Assert.IsNotNull(emptyInstance);
            Assert.IsTrue(emptyInstance is IUpdateSummaryDynamicMessage);
        }

        [Test]
        public void Should_Generate_Only_Specified_Properties_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance = Activator.CreateInstance(updateSummaryMessageType);
            var typedEmptyInstance = emptyInstance as IUpdateSummaryDynamicMessage;

            // Assert
            Assert.IsNotNull(emptyInstance);
            Assert.IsNotNull(typedEmptyInstance);
            Assert.IsNull(typedEmptyInstance.Symbol);
            Assert.AreEqual(default(double), typedEmptyInstance.MostRecentTrade);
            Assert.AreEqual(default(int), typedEmptyInstance.MostRecentTradeSize);
            Assert.AreEqual(default(DateTime), typedEmptyInstance.MostRecentTradeDate);
            Assert.AreEqual(default(TimeSpan), typedEmptyInstance.MostRecentTradeTime);
            Assert.AreEqual(default(int), typedEmptyInstance.MostRecentTradeAggressor);
            Assert.Throws(typeof(NotImplementedException), () => typedEmptyInstance.Bid.ToString());
            Assert.Throws(typeof(NotImplementedException), () => typedEmptyInstance.MostRecentTradeDayCode.ToString());
        }

        [Test]
        public void Should_Generate_Default_Constructor_For_Partial_Fields()
        {   
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance = Activator.CreateInstance(updateSummaryMessageType);
            var typedEmptyInstance = emptyInstance as IUpdateSummaryDynamicMessage;

            // Assert
            Assert.IsNotNull(emptyInstance);
            Assert.IsNotNull(typedEmptyInstance);
            Assert.IsNull(typedEmptyInstance.Symbol);
            Assert.AreEqual(default(double), typedEmptyInstance.MostRecentTrade);
            Assert.AreEqual(default(int), typedEmptyInstance.MostRecentTradeSize);
            Assert.AreEqual(default(DateTime), typedEmptyInstance.MostRecentTradeDate);
            Assert.AreEqual(default(TimeSpan), typedEmptyInstance.MostRecentTradeTime);
            Assert.AreEqual(default(int), typedEmptyInstance.MostRecentTradeAggressor);
        }

        [Test]
        public void Should_Generate_Parse_Method_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            var parsedInstance = parseMethod.Invoke(null, new object[] { "P,AAPL,188.3500,52500,03/30/2021,19:59:14.503633,3" });
            var typedParsedInstance = parsedInstance as IUpdateSummaryDynamicMessage;

            // Assert
            Assert.IsNotNull(parsedInstance);
            Assert.IsNotNull(typedParsedInstance);
            Assert.AreEqual("AAPL", typedParsedInstance.Symbol);
            Assert.AreEqual(188.35, typedParsedInstance.MostRecentTrade);
            Assert.AreEqual(52500, typedParsedInstance.MostRecentTradeSize);
            Assert.AreEqual(FieldParser.ParseDate("03/30/2021", FundamentalMessage.FundamentalDateTimeFormat), typedParsedInstance.MostRecentTradeDate);
            Assert.AreEqual(FieldParser.ParseTime("19:59:14.503633", UpdateSummaryMessage.UpdateMessageTimeFormat), typedParsedInstance.MostRecentTradeTime);
            Assert.AreEqual(3, typedParsedInstance.MostRecentTradeAggressor);
        }

        [Test]
        public void Should_Generate_Equals_Method_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance1 = Activator.CreateInstance(updateSummaryMessageType) as IUpdateSummaryMessage;
            var emptyInstance2 = Activator.CreateInstance(updateSummaryMessageType) as IUpdateSummaryMessage;
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            var parsedInstance1 = parseMethod.Invoke(null, new object[] { "P,AAPL,188.3500,52500,03/30/2021,19:59:14.503633,1" });
            var parsedInstance2 = parseMethod.Invoke(null, new object[] { "P,AAPL,188.3500,52500,03/30/2021,19:59:14.503633,1" });
            var parsedInstance3 = parseMethod.Invoke(null, new object[] { "P,AAPL,200,52500,03/30/2021,19:59:14.503633,1" });

            // Assert
            Assert.AreEqual(emptyInstance1, emptyInstance2);
            Assert.AreNotEqual(emptyInstance1, parsedInstance1);
            Assert.AreEqual(parsedInstance1, parsedInstance2);
            Assert.AreNotEqual(parsedInstance1, parsedInstance3);
        }

        [Test]
        public void Should_Generate_GetHashCode_Method_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance = Activator.CreateInstance(updateSummaryMessageType);
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            var parsedInstance = parseMethod.Invoke(null, new object[] { "P,AAPL,188.3500,52500,03/30/2021,19:59:14.503633,2" });

            // calculate the expected hashes
            var expectedHashCodeEmptyInstance = 17;
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + ((string)null).GetHashCodeOrDefault();
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + default(double).GetHashCode();
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + default(int).GetHashCode();
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + default(DateTime).GetHashCode();
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + default(TimeSpan).GetHashCode();
            expectedHashCodeEmptyInstance = expectedHashCodeEmptyInstance * 29 + default(int).GetHashCode();

            var expectedHashCodeParsedInstance = 17;
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + "AAPL".GetHashCode();
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + 188.35.GetHashCode();
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + 52500.GetHashCode();
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + FieldParser.ParseDate("03/30/2021", FundamentalMessage.FundamentalDateTimeFormat).GetHashCode();
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + FieldParser.ParseTime("19:59:14.503633", UpdateSummaryMessage.UpdateMessageTimeFormat).GetHashCode();
            expectedHashCodeParsedInstance = expectedHashCodeParsedInstance * 29 + 2.GetHashCode();

            // Assert
            Assert.IsNotNull(emptyInstance);
            Assert.AreEqual(expectedHashCodeEmptyInstance, emptyInstance.GetHashCode());
            Assert.IsNotNull(parsedInstance);
            Assert.AreEqual(expectedHashCodeParsedInstance, parsedInstance.GetHashCode());
        }

        [Test]
        public void Should_Generate_ToString_Method_For_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);

            // Act
            var emptyInstance = Activator.CreateInstance(updateSummaryMessageType);
            var parseMethod = updateSummaryMessageType.GetMethod("Parse", BindingFlags.Public | BindingFlags.Static);
            var parsedInstance = parseMethod.Invoke(null, new object[] { "P,AAPL,188.3500,52500,03/30/2021,19:59:14.503633,2" });

            // generate the expected string values
            var expectedStringEmptyInstance = $"Symbol: <NULL>, MostRecentTrade: {default(double)}, MostRecentTradeSize: {default(int)}, " +
                $"MostRecentTradeDate: {default(DateTime)}, " +
                $"MostRecentTradeTime: {default(TimeSpan)}, " +
                $"MostRecentTradeAggressor: {default(int)}";
            var expectedStringParsedInstance = $"Symbol: AAPL, MostRecentTrade: 188.35, MostRecentTradeSize: 52500, " +
                $"MostRecentTradeDate: {FieldParser.ParseDate("03/30/2021", FundamentalMessage.FundamentalDateTimeFormat)}, " +
                $"MostRecentTradeTime: {FieldParser.ParseTime("19:59:14.503633", UpdateSummaryMessage.UpdateMessageTimeFormat)}, " +
                $"MostRecentTradeAggressor: 2";

            // Assert
            Assert.IsNotNull(emptyInstance);            
            Assert.AreEqual(expectedStringEmptyInstance, emptyInstance.ToString());
            Assert.IsNotNull(parsedInstance);
            Assert.AreEqual(expectedStringParsedInstance, parsedInstance.ToString());
        }

        [Test]
        public void Should_Reuse_Generated_Type_For_Same_Partial_Fields()
        {
            // Arrange
            var updateSummaryMessageType1 = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(PartialFields);
            // same fields, same order
            var updateSummaryMessageType2 = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[]
            {
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeDate,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeAggressor
            });
            // same fields, different order
            var updateSummaryMessageType3 = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[]
            {
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeAggressor,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeDate,
            });

            // Assert
            Assert.AreSame(updateSummaryMessageType1, updateSummaryMessageType2);
            Assert.AreNotSame(updateSummaryMessageType1, updateSummaryMessageType3);
        }        

        #endregion Partial Fields Tests

        #region All Fields Tests

        [Test]
        public void Should_Generate_Valid_Object_For_All_FieldsSet()
        {
            // Arrange
            var updateSummaryMessageType = UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(AllFields);

            // Act
            var emptyInstance1 = Activator.CreateInstance(updateSummaryMessageType);
            var emptyInstance2 = Activator.CreateInstance(updateSummaryMessageType);

            // Assert

            // just smoke test all operations, make sure there are no failures
            Assert.IsNotNull(emptyInstance1);
            Assert.IsNotNull(emptyInstance2);
            Assert.AreEqual(emptyInstance1, emptyInstance2);
            emptyInstance1.GetHashCode();
            Assert.IsNotNull(emptyInstance1.ToString());
        }

        #endregion All Fields Tests

        #region Unhappy Path Tests

        [Test]
        public void Should_Throw_ArgumentNullException_When_FieldsSet_Is_Null()
        {
            Assert.Throws(typeof(ArgumentNullException), () => UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(null));
        }

        [Test]
        public void Should_Throw_ArgumentException_For_Empty_Fields()
        {
            Assert.Throws(typeof(ArgumentException), () => UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[0]));
        }

        [Test]
        public void Should_Throw_ArgumentException_For_Single_Field()
        {
            Assert.Throws(typeof(ArgumentException), () => UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[]
            {
                DynamicFieldset.Symbol
            }));
        }

        [Test]
        public void Should_Throw_ArgumentException_When_Symbol_Is_Not_First_Field()
        {
            Assert.Throws(typeof(ArgumentException), () => UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[]
            {
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeDate,
            }));
        }

        [Test]
        public void Should_Throw_ArgumentException_When_Duplicate_Fields_Specified()
        {
            Assert.Throws(typeof(ArgumentException), () => UpdateSummaryDynamicMessageTypesFactory.GenerateDynamicObjectType(new DynamicFieldset[]
            {
                DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeDate,
                DynamicFieldset.MostRecentTrade,
            }));
        }

        #endregion Unhappy Path Tests
    }
}