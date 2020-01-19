using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2;
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

            var expectedMessage = new UpdateSummaryMessage<decimal>("@ES#", "MD01", 2982.25m, 2982.5m, 99, 238, askTime, date, "52", bidTime, 'T', 'T', 'F');
            UpdateSummaryMessage<decimal> updateSummaryMessage = null;
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

            var expectedMessage = new UpdateSummaryMessage<decimal>("@ES#", "MD01", 2982.25m, 2982.5m, 99, 238, askTime, date, "52", bidTime, 'T', 'T', 'F');
            UpdateSummaryMessage<decimal> updateSummaryMessage = null;
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
    }
}