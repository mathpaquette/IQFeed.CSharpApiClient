using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2;
using IQFeed.CSharpApiClient.Streaming.Level2.Enums;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2
{
    class Level2MessageHandlerTests
    {
        private Level2MessageHandler _level2MessageHandler;

        [SetUp]
        public void SetUp()
        {
            _level2MessageHandler = new Level2MessageHandler();
        }

        [Test]
        public void Should_Receive_Summary()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("Z,@ES#,MD01,2982.25,2982.5,99,238,12:49:52.696621,2019-10-18,52,12:49:52.541104,T,T,F,\r\n");
            DateTime.TryParseExact("12:49:52.696621", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            var date = new DateTime(2019, 10, 18);
            DateTime.TryParseExact("12:49:52.541104", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);

            var expectedMessage = new UpdateSummaryMessage("@ES#", "MD01", 2982.25, 2982.5, 99, 238, askTime, date, "52", bidTime, 'T', 'T', 'F');
            UpdateSummaryMessage updateSummaryMessage = null;
            _level2MessageHandler.Summary += msg =>
            {
                updateSummaryMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(updateSummaryMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_Update()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("2,@ES#,MD01,2982.25,2982.5,99,238,12:49:52.696622,2019-10-18,52,12:49:52.541105,T,T,F,\r\n");
            DateTime.TryParseExact("12:49:52.696622", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            var date = new DateTime(2019, 10, 18);
            DateTime.TryParseExact("12:49:52.541105", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);

            var expectedMessage = new UpdateSummaryMessage("@ES#", "MD01", 2982.25, 2982.5, 99, 238, askTime, date, "52", bidTime, 'T', 'T', 'F');
            UpdateSummaryMessage updateSummaryMessage = null;
            _level2MessageHandler.Update += msg =>
            {
                updateSummaryMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(updateSummaryMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_Timestamp()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("T,20191018 12:49:55\r\n");
            var expectedMessage = new TimestampMessage(new DateTime(2019, 10, 18, 12, 49, 55));

            TimestampMessage timestampMessage = null;
            _level2MessageHandler.Timestamp += msg =>
            {
                timestampMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(timestampMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_MarketMakerName()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("M,MD02,Market depth 2\r\n");
            var expectedMessage = new MarketMakerNameMessage("MD02", "Market depth 2");

            MarketMakerNameMessage marketMakerNameMessage = null;
            _level2MessageHandler.Query += msg =>
            {
                marketMakerNameMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(marketMakerNameMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_System()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("S,SERVER CONNECTED\r\n");
            var expectedMessage = new SystemMessage("SERVER CONNECTED", "S,SERVER CONNECTED");

            SystemMessage systemMessage = null;
            _level2MessageHandler.System += msg =>
            {
                systemMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(systemMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_SymbolNotFound()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("n,INVALID_SYMBOL_NAME\r\n");
            var expectedMessage = new SymbolNotFoundMessage("INVALID_SYMBOL_NAME");

            SymbolNotFoundMessage symbolNotFoundMessage = null;
            _level2MessageHandler.SymbolNotFound += msg =>
            {
                symbolNotFoundMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(symbolNotFoundMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_Error()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("E,INVALID_SYMBOL_NAME\r\n");
            var expectedMessage = new ErrorMessage("INVALID_SYMBOL_NAME");

            ErrorMessage errorMessage = null;
            _level2MessageHandler.Error += msg =>
            {
                errorMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(errorMessage, expectedMessage);
        }

        [Test]
        public void Should_Throw_Exception()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("U,INVALID,RESPONSE\r\n");

            // Act
            Assert.Throws<Exception>(() => _level2MessageHandler.ProcessMessages(message, message.Length));
        }

        [Test]
        public void Should_Receive_Multiple_Events_When_Message_Block_Contains_Multiple()
        {
            // Arrange
            var count = 0;
            var messages = new List<string>
            {
                "M,MD02,Market depth 2\r\n",
                "M,MD02,Market depth 2\r\n",
                "\r\n",
                "\r\n"
            };
            var messageBytes = TestHelper.GetMessageBytes(messages);
            _level2MessageHandler.Query += message => count += 1;

            // Act
            _level2MessageHandler.ProcessMessages(messageBytes, messageBytes.Length);

            // Arrange
            Assert.AreEqual(count, 2);
        }

        [Test]
        public void Should_Receive_PriceLevelOrderMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("0,@ESM19,12345678,MD01,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelOrderMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", PriceLevelOrderMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var expectedMessage = new PriceLevelOrderMessage(Level2MessageType.PriceLevelOrder, "@ESM19", 12345678, "MD01", Level2Side.Sell, 2938.25, 65, 10, 2, orderTime, orderDate);

            PriceLevelOrderMessage receivedMessage = null;
            _level2MessageHandler.PriceLevelOrder += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_OrderAddUpdateSummaryMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("3,@ESM19,12345678,MD01,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", OrderAddUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderAddUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var expectedMessage = new OrderAddUpdateSummaryMessage(Level2MessageType.OrderAdd, "@ESM19", 12345678, "MD01", Level2Side.Sell, 2938.25, 65, 10, 2, orderTime, orderDate);

            OrderAddUpdateSummaryMessage receivedMessage = null;
            _level2MessageHandler.OrderAdd += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_OrderLevelUpdateMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("4,@ESM19,12345678,MD01,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", OrderAddUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderAddUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var expectedMessage = new OrderAddUpdateSummaryMessage(Level2MessageType.OrderLevelUpdate, "@ESM19", 12345678, "MD01", Level2Side.Sell, 2938.25, 65, 10, 2, orderTime, orderDate);

            OrderAddUpdateSummaryMessage receivedMessage = null;
            _level2MessageHandler.OrderUpdate += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_OrderAddSummaryMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("6,@ESM19,12345678,MD01,A,2938.25,65,10,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", OrderAddUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderAddUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var expectedMessage = new OrderAddUpdateSummaryMessage(Level2MessageType.OrderLevelSummary, "@ESM19", 12345678, "MD01", Level2Side.Sell, 2938.25, 65, 10, 2, orderTime, orderDate);

            OrderAddUpdateSummaryMessage receivedMessage = null;
            _level2MessageHandler.OrderSummary += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_OrderDeleteMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("5,@ESM19,12345678,,A,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", OrderDeleteMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var orderTime);
            DateTime.TryParseExact("2019-04-23", OrderDeleteMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var orderDate);
            var expectedMessage = new OrderDeleteMessage(Level2MessageType.OrderDelete, "@ESM19", 12345678, Level2Side.Sell, orderTime, orderDate);

            OrderDeleteMessage receivedMessage = null;
            _level2MessageHandler.OrderDelete += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_PriceLevelSummaryMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("7,@ESM19,B,2938.25,65,11,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact("2019-04-23", PriceLevelUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var expectedMessage = new PriceLevelUpdateSummaryMessage(Level2MessageType.PriceLevelSummary, "@ESM19", Level2Side.Buy, 2938.25, 65, 10, 2, time, date);

            PriceLevelUpdateSummaryMessage receivedMessage = null;
            _level2MessageHandler.PriceLevelSummary += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_PriceLevelUpdateMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("8,@ESM19,B,2938.25,65,11,2,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelUpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact("2019-04-23", PriceLevelUpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var expectedMessage = new PriceLevelUpdateSummaryMessage(Level2MessageType.PriceLevelUpdate, "@ESM19", Level2Side.Buy, 2938.25, 65, 10, 2, time, date);

            PriceLevelUpdateSummaryMessage receivedMessage = null;
            _level2MessageHandler.PriceLevelSummary += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }

        [Test]
        public void Should_Receive_PriceLevelDeleteMessages()
        {
            // Arrange
            var message = TestHelper.GetMessageBytes("9,@ESM19,A,2938.25,20:31:04.876740,2019-04-23,\r\n");
            TimeSpan.TryParseExact("20:31:04.876740", PriceLevelDeleteMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var time);
            DateTime.TryParseExact("2019-04-23", PriceLevelDeleteMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            var expectedMessage = new PriceLevelDeleteMessage(Level2MessageType.PriceLevelDelete, "@ESM19", Level2Side.Sell, 2938.25, time, date);

            PriceLevelDeleteMessage receivedMessage = null;
            _level2MessageHandler.PriceLevelDelete += msg =>
            {
                receivedMessage = msg;
            };

            // Act
            _level2MessageHandler.ProcessMessages(message, message.Length);

            // Assert
            Assert.AreEqual(receivedMessage, expectedMessage);
        }
    }
}