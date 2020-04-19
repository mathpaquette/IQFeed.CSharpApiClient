using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Lookup.MarketSummary;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class MarketSummaryMessageTests
    {
        [Test]
        public void Should_Parse_MarketSummaryMessage()
        {
            // Arrange
            var message1 = "Symbol,Exchange,Type,Last,TradeSize,TradeDate,TradeTime";
            var message2 = "TEST,34,9,1.52,15,20200301,130125";
            var expectedFields = new Dictionary<string, object>()
            {
                { "Symbol", "TEST" },
                { "Exchange", 34 },
                { "Type", 9 },
                { "Last", 1.52d },
                { "TradeSize", 15 },
                { "TradeDate", new DateTime(2020, 03, 01) },
                { "TradeTime", new TimeSpan(13, 01, 25) }
            };

            var marketSummaryHandler = new MarketSummaryHandler<double>();

            // Act
            var marketSummaryMessage1Parsed = MarketSummaryMessage<double>.Parse(message1, marketSummaryHandler);
            var marketSummaryMessage2Parsed = MarketSummaryMessage<double>.Parse(message2, marketSummaryHandler);
            var marketSummaryMessage = MarketSummaryMessage<double>.ParseFromFieldsDictionary(expectedFields, marketSummaryHandler);

            // Assert
            // MarketSummaryMessage1Parsed should be null, as it was the Fieldname pass
            Assert.IsNull(marketSummaryMessage1Parsed);
            Assert.IsNotNull(marketSummaryMessage2Parsed);
            Assert.AreEqual(marketSummaryMessage2Parsed, marketSummaryMessage);
        }
    }
}