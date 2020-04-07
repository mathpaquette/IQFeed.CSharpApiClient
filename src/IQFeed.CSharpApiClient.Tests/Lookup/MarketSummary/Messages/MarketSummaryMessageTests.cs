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
            var message1 = "Field1,Field2,Field3,Field4";
            var message2 = "Value1,Value2,Value3,Value4";
            var expectedFields = new Dictionary<string, object>()
            {
                { "Field1", "Value1" },
                { "Field2", "Value2" },
                { "Field3", "Value3" },
                { "Field4", "Value4" }
            };

            var marketSummaryHandler = new MarketSummaryHandler<double>();

            // Act
            var marketSummaryMessage1Parsed = MarketSummaryMessage<double>.Parse(message1, marketSummaryHandler);
            var marketSummaryMessage2Parsed = MarketSummaryMessage<double>.Parse(message2, marketSummaryHandler);
            var marketSummaryMessage = new MarketSummaryMessage<double>(expectedFields);

            // Assert
            // MarketSummaryMessage1Parsed should be null, as it was the Fieldname pass
            Assert.IsNull(marketSummaryMessage1Parsed);
            Assert.IsNotNull(marketSummaryMessage2Parsed);
            Assert.AreEqual(marketSummaryMessage2Parsed, marketSummaryMessage);
        }
    }
}