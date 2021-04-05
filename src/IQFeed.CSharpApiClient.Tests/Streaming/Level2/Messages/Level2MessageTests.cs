using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Level2;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2.Messages
{
    public class Level2MessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_UpdateSummaryMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2,@ESM19,MD01,2938.,2938.25,65,23,20:31:04.876740,2019-04-23,52,20:31:04.876740,T,T,F,";

            // Act
            var updateSummaryMessageParsed = UpdateSummaryMessage.Parse(message);
            DateTime.TryParseExact("20:31:04.876740", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);
            DateTime.TryParseExact("2019-04-23", UpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            DateTime.TryParseExact("20:31:04.876740", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            var updateSummaryMessage = new UpdateSummaryMessage("@ESM19", "MD01", 2938.0, 2938.25, 65, 23, bidTime, date, "52", askTime, 'T', 'T', 'F');

            // Assert
            Assert.AreEqual(updateSummaryMessageParsed, updateSummaryMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_OrderAddUpdateSummaryMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var priceLevelOrderMessageString = "0,@ESM19,12345678,MD01,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,";
            var orderAddMessageString = "3,@ESM19,12345678,MD01,B,2938.25,65,11,2,20:31:04.876740,2019-04-23,";

            // Act
            var priceLevelOrderMessageParsed = OrderAddUpdateSummaryMessage.Parse(priceLevelOrderMessageString);
            var orderAddMessageParsed = OrderAddUpdateSummaryMessage.Parse(orderAddMessageString);
            TimeSpan.TryParseExact("20:31:04.876740", OrderAddUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderAddUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var priceLevelOrderMessage = new OrderAddUpdateSummaryMessage(Level2MessageType.PriceLevelOrder, "@ESM19", 12345678, "MD01", Level2Side.Sell, 2938.25, 65, 10, 2, orderTime, orderDate);
            var orderAddMessage = new OrderAddUpdateSummaryMessage(Level2MessageType.OrderAdd, "@ESM19", 12345678, "MD01", Level2Side.Buy, 2938.25, 65, 10, 2, orderTime, orderDate);

            // Assert
            Assert.AreEqual(priceLevelOrderMessageParsed, priceLevelOrderMessage);
            Assert.AreEqual(orderAddMessageParsed, orderAddMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_OrderDeleteMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var orderDeleteMessageString = "5,@ESM19,12345678,,A,20:31:04.876740,2019-04-23,";

            // Act
            var orderDeleteMessageParsed = OrderDeleteMessage.Parse(orderDeleteMessageString);
            TimeSpan.TryParseExact("20:31:04.876740", OrderDeleteMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderDeleteMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var orderDeleteMessage = new OrderDeleteMessage(Level2MessageType.OrderDelete, "@ESM19", 12345678, Level2Side.Sell, orderTime, orderDate);

            // Assert
            Assert.AreEqual(orderDeleteMessageParsed, orderDeleteMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_PriceLevelUpdateSummaryMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var priceLevelUpdateMessageString = "8,@ESM19,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,";
            var priceLevelSummaryMessageString = "7,@ESM19,B,2938.25,65,11,2,20:31:04.876740,2019-04-23,";

            // Act
            var priceLevelUpdateMessageParsed = PriceLevelUpdateSummaryMessage.Parse(priceLevelUpdateMessageString);
            var priceLevelSummaryMessageParsed = PriceLevelUpdateSummaryMessage.Parse(priceLevelSummaryMessageString);
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact("2019-04-23", PriceLevelUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var priceLevelUpdateMessage = new PriceLevelUpdateSummaryMessage(Level2MessageType.PriceLevelUpdate, "@ESM19", Level2Side.Sell, 2938.25, 65, 10, 2, time, date);
            var priceLevelSummaryMessage = new PriceLevelUpdateSummaryMessage(Level2MessageType.PriceLevelSummary, "@ESM19", Level2Side.Buy, 2938.25, 65, 10, 2, time, date);

            // Assert
            Assert.AreEqual(priceLevelUpdateMessageParsed, priceLevelUpdateMessage);
            Assert.AreEqual(priceLevelSummaryMessageParsed, priceLevelSummaryMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_PriceLevelDeleteMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var priceLevelDeleteMessageString = "9,@ESM19,A,2938.25,20:31:04.876740,2019-04-23,";

            // Act
            var priceLevelDeleteMessageParsed = PriceLevelDeleteMessage.Parse(priceLevelDeleteMessageString);
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelDeleteMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact("2019-04-23", PriceLevelDeleteMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var priceLevelDeleteMessage = new PriceLevelDeleteMessage(Level2MessageType.PriceLevelDelete, "@ESM19", Level2Side.Sell, 2938.25, time, date);

            // Assert
            Assert.AreEqual(priceLevelDeleteMessageParsed, priceLevelDeleteMessage);
        }

    }
}