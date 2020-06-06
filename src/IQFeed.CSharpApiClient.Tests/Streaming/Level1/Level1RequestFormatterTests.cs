using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    public class Level1RequestFormatterTests
    {
        private Level1RequestFormatter _level1RequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _level1RequestFormatter = new Level1RequestFormatter();
        }

        [Test]
        public void SetLogLevels_Should_Return_Formatted_Request()
        {
            // Arrange
            var logLevels = new[] { LoggingLevel.Admin, LoggingLevel.Debug };

            // Act
            var formatted = _level1RequestFormatter.SetLogLevels(logLevels);

            // Assert
            Assert.AreEqual(formatted, "S,SET LOG LEVELS,Admin,Debug\r\n");
        }

        [Test]
        public void SelectUpdateFieldName_Should_Return_Formatted_Request()
        {
            // Arrange
            var fields = new[]
            {
                //Feed always includes Symbol as first field, regardless of request
                //DynamicFieldset.Symbol,
                DynamicFieldset.MostRecentTrade,
                DynamicFieldset.MostRecentTradeSize,
                DynamicFieldset.MostRecentTradeTime,
                DynamicFieldset.MostRecentTradeMarketCenter,
                DynamicFieldset.TotalVolume,
                DynamicFieldset.Bid,
                DynamicFieldset.BidSize,
                DynamicFieldset.Ask,
                DynamicFieldset.AskSize,
                DynamicFieldset.Open,
                DynamicFieldset.High,
                DynamicFieldset.Low,
                DynamicFieldset.Close,
                DynamicFieldset.MessageContents,
                DynamicFieldset.MostRecentTradeConditions
            };

            // Act
            var formatted = _level1RequestFormatter.SelectUpdateFieldName(fields);

            // Assert
            Assert.AreEqual(formatted, "S,SELECT UPDATE FIELDS,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions\r\n");
        }
    }
}