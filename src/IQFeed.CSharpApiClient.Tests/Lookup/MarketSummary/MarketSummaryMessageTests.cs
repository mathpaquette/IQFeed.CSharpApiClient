using System;
using IQFeed.CSharpApiClient.Lookup.MarketSummary;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.MarketSummary
{
    public class MarketSummaryMessageTests
    {
        [Test]
        public void Should_Parse_MarketSummaryMessage()
        {
            // Arrange
            var requestId = "TEST";
            var symbol = "MSFT";
            var last = 1.234D;
            var tradeDate = new DateTime(2021, 03, 24);
            var tradeTime = new TimeSpan(11, 42, 12);
            var message1 = "RequestId,Symbol,Last,TradeDate,TradeTime";
            var message2 = "TEST,MSFT,1.234,20210324,114212";
            var values = new string[] {"TEST", "MSFT", "1.234", "20210324", "114212"};

            var marketSummaryHandler = new MarketSummaryHandler();

            // Act
            var marketSummaryMessage1Parsed = MarketSummaryMessage.Parse(message1, marketSummaryHandler);
            var marketSummaryMessage2Parsed = MarketSummaryMessage.Parse(message2, marketSummaryHandler);
            var marketSummaryMessage1 = new MarketSummaryMessage(symbol, 0, 0, last, null, null, tradeDate,
                tradeTime, null, null, null, null, null, null, null, null, null, null, null, 
                null, null, null, null, null, null, null, null, null,
                null, null, null, null, null, null, null, requestId);
            var marketSummaryMessage2 = new MarketSummaryMessage(values, marketSummaryHandler);

            // Assert
            // MarketSummaryMessage1Parsed should be null, as it was the Fieldname pass
            Assert.IsNull(marketSummaryMessage1Parsed);
            Assert.IsNotNull(marketSummaryMessage2Parsed);
            Assert.AreEqual(marketSummaryMessage2Parsed, marketSummaryMessage1);
            Assert.AreEqual(marketSummaryMessage2Parsed, marketSummaryMessage2);
        }
    }
}