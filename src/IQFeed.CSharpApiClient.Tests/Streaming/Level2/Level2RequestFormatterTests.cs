using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Streaming.Level2;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2
{
    public class Level2RequestFormatterTests
    {
        //private Level2RequestFormatter _level2RequestFormatter;

        //[SetUp]
        //public void SetUp()
        //{
        //    _level2RequestFormatter = new Level2RequestFormatter();
        //}

        //[Test]
        //public void SetLogLevels_Should_Return_Formatted_Request()
        //{
        //    // Arrange
        //    var logLevels = new[] { LoggingLevel.Admin, LoggingLevel.Debug };

        //    // Act
        //    var formatted = _level2RequestFormatter.SetLogLevels(logLevels);

        //    // Assert
        //    Assert.AreEqual(formatted, "S,SET LOG LEVELS,Admin,Debug\r\n");
        //}

        //[Test]
        //public void SelectUpdateFieldName_Should_Return_Formatted_Request()
        //{
        //    // Arrange
        //    var fields = new[]
        //    {
        //        DynamicFieldset.Symbol,
        //        DynamicFieldset.MostRecentTrade,
        //        DynamicFieldset.MostRecentTradeSize,
        //        DynamicFieldset.MostRecentTradeTime,
        //        DynamicFieldset.MostRecentTradeMarketCenter,
        //        DynamicFieldset.TotalVolume,
        //        DynamicFieldset.Bid,
        //        DynamicFieldset.BidSize,
        //        DynamicFieldset.Ask,
        //        DynamicFieldset.AskSize,
        //        DynamicFieldset.Open,
        //        DynamicFieldset.High,
        //        DynamicFieldset.Low,
        //        DynamicFieldset.Close,
        //        DynamicFieldset.MessageContents,
        //        DynamicFieldset.MostRecentTradeConditions
        //    };

        //    // Act
        //    var formatted = _level2RequestFormatter.SelectUpdateFieldName(fields);

        //    // Assert
        //    Assert.AreEqual(formatted, "S,SELECT UPDATE FIELDS,Symbol,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions\r\n");
        //}
    }
}