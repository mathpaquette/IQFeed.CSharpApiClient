using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class TradeCorrectionMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TradeCorrectionMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "C,AAPL,I,03/30/2021,19:59:14.503633,183.3600,13,101123,8801,17";

            // Act
            var tradeCorrectionMessageParsed = TradeCorrectionMessage.Parse(message);
            DateTime.TryParseExact("03/30/2021", TradeCorrectionMessage.TradeCorrectionMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var tradeDate);
            TimeSpan.TryParseExact("19:59:14.503633", TradeCorrectionMessage.TradeCorrectionMessageTimeFormat, CultureInfo.InvariantCulture, out var tradeTime);
            var tradeCorrectionMessage = new TradeCorrectionMessage("AAPL", "I", tradeDate, tradeTime, 183.3600, 13, 101123, "8801", 17);

            // Assert
            Assert.AreEqual(tradeCorrectionMessageParsed, tradeCorrectionMessage);
        }
    }
}