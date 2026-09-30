using System;
using System.Collections.Generic;
using System.Text;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Handlers
{
    public class Level1MessageHandlerTests
    {
        private Level1MessageHandler _level1MessageHandler;
        private List<UnhandledMessage> _unhandledMessages;
        private List<SystemMessage> _systemMessages;
        private List<TimestampMessage> _timestampMessages;

        [SetUp]
        public void SetUp()
        {
            _level1MessageHandler = new Level1MessageHandler();
            _unhandledMessages = new List<UnhandledMessage>();
            _systemMessages = new List<SystemMessage>();
            _timestampMessages = new List<TimestampMessage>();

            _level1MessageHandler.UnhandledMessage += _unhandledMessages.Add;
            _level1MessageHandler.System += _systemMessages.Add;
            _level1MessageHandler.Timestamp += _timestampMessages.Add;
        }

        [Test]
        public void Should_Process_Known_Messages_Without_Raising_UnhandledMessage()
        {
            // Act
            Process(_level1MessageHandler, "S,KEY,abc\r\nT,20260929 19:25:16\r\n");

            // Assert
            Assert.AreEqual(1, _systemMessages.Count);
            Assert.AreEqual(1, _timestampMessages.Count);
            Assert.IsEmpty(_unhandledMessages);
        }

        [Test]
        public void Should_Raise_UnhandledMessage_For_Unknown_Type_And_Process_The_Other_Lines()
        {
            // Act
            Process(_level1MessageHandler, "S,KEY,abc\r\nX,unknown,type\r\nT,20260929 19:25:16\r\n");

            // Assert
            Assert.AreEqual(1, _systemMessages.Count);
            Assert.AreEqual(1, _timestampMessages.Count);
            Assert.AreEqual(1, _unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.UnknownType, _unhandledMessages[0].Reason);
            Assert.AreEqual("X,unknown,type\r", _unhandledMessages[0].Message);
            Assert.IsNull(_unhandledMessages[0].Exception);
        }

        [Test]
        public void Should_Raise_UnhandledMessage_For_Empty_Line_And_Process_The_Other_Lines()
        {
            // Act
            Process(_level1MessageHandler, "S,KEY,abc\n\nT,20260929 19:25:16\n");

            // Assert
            Assert.AreEqual(1, _systemMessages.Count);
            Assert.AreEqual(1, _timestampMessages.Count);
            Assert.AreEqual(1, _unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.Empty, _unhandledMessages[0].Reason);
        }

        [Test]
        public void Should_Raise_UnhandledMessage_For_A_Lone_Line_Feed()
        {
            // Act
            Process(_level1MessageHandler, "\n");

            // Assert
            Assert.AreEqual(1, _unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.Empty, _unhandledMessages[0].Reason);
        }

        [Test]
        public void Should_Raise_UnhandledMessage_When_Parsing_Fails_And_Process_The_Lines_After_It()
        {
            // Act: a bare "S" has no system message type field to parse
            Process(_level1MessageHandler, "S\nT,20260929 19:25:16\nS,KEY,abc\n");

            // Assert
            Assert.AreEqual(1, _timestampMessages.Count, "the lines after the one that failed must still be processed");
            Assert.AreEqual(1, _systemMessages.Count);
            Assert.AreEqual(1, _unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.ProcessingFailed, _unhandledMessages[0].Reason);
            Assert.AreEqual("S", _unhandledMessages[0].Message);
            Assert.IsNotNull(_unhandledMessages[0].Exception);
        }

        [Test]
        public void Should_Raise_UnhandledMessage_When_A_Subscriber_Throws_And_Process_The_Lines_After_It()
        {
            // Arrange
            var subscriberException = new InvalidOperationException("subscriber failed");
            _level1MessageHandler.Timestamp += message => throw subscriberException;

            // Act
            Process(_level1MessageHandler, "T,20260929 19:25:16\nS,KEY,abc\n");

            // Assert
            Assert.AreEqual(1, _systemMessages.Count);
            Assert.AreEqual(1, _unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.ProcessingFailed, _unhandledMessages[0].Reason);
            Assert.AreSame(subscriberException, _unhandledMessages[0].Exception);
        }

        [Test]
        public void Should_Not_Throw_For_Unknown_Type_Without_Subscribers()
        {
            // Arrange
            var level1MessageHandler = new Level1MessageHandler();

            // Act & Assert
            Assert.DoesNotThrow(() => Process(level1MessageHandler, "X,unknown,type\n\n"));
        }

        [Test]
        public void Should_Raise_UnhandledMessage_From_The_Dynamic_Handler()
        {
            // Arrange
            var level1DynamicMessageHandler = new Level1DynamicMessageHandler();
            level1DynamicMessageHandler.SetDynamicFields(DynamicFieldset.Symbol, DynamicFieldset.Bid);
            var unhandledMessages = new List<UnhandledMessage>();
            var summaries = new List<IUpdateSummaryDynamicMessage>();
            level1DynamicMessageHandler.UnhandledMessage += unhandledMessages.Add;
            level1DynamicMessageHandler.Summary += summaries.Add;

            // Act
            Process(level1DynamicMessageHandler, "X,unknown,type\nP,AAPL,322.68\n");

            // Assert
            Assert.AreEqual(1, summaries.Count);
            Assert.AreEqual(1, unhandledMessages.Count);
            Assert.AreEqual(UnhandledMessageReason.UnknownType, unhandledMessages[0].Reason);
        }

        private static void Process(BaseLevel1MessageHandler level1MessageHandler, string messages)
        {
            var messageBytes = Encoding.ASCII.GetBytes(messages);
            level1MessageHandler.ProcessMessages(messageBytes, messageBytes.Length);
        }
    }
}
