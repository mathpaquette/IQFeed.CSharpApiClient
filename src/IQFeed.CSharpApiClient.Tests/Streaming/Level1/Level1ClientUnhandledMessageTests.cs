using System;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NSubstitute;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1
{
    public class Level1ClientUnhandledMessageTests
    {
        [Test]
        public void Should_Forward_UnhandledMessage_Subscriptions_To_The_Handler()
        {
            // Arrange
            var level1MessageHandler = Substitute.For<ILevel1MessageHandler>();
            var level1Client = new Level1Client(new SocketClient("localhost", 5009), new Level1RequestFormatter(), level1MessageHandler, Substitute.For<ILevel1Snapshot>());
            Action<UnhandledMessage> subscriber = message => { };

            // Act
            level1Client.UnhandledMessage += subscriber;
            level1Client.UnhandledMessage -= subscriber;

            // Assert
            level1MessageHandler.Received(1).UnhandledMessage += subscriber;
            level1MessageHandler.Received(1).UnhandledMessage -= subscriber;
        }

        [Test]
        public void Should_Forward_UnhandledMessage_Subscriptions_To_The_Dynamic_Handler()
        {
            // Arrange
            var level1DynamicMessageHandler = Substitute.For<ILevel1DynamicMessageHandler>();
            var level1DynamicClient = new Level1DynamicClient(
                new SocketClient("localhost", 5009),
                new Level1RequestFormatter(),
                level1DynamicMessageHandler,
                Substitute.For<ILevel1DynamicSnapshot>(),
                new[] { DynamicFieldset.Symbol });
            Action<UnhandledMessage> subscriber = message => { };

            // Act
            level1DynamicClient.UnhandledMessage += subscriber;
            level1DynamicClient.UnhandledMessage -= subscriber;

            // Assert
            level1DynamicMessageHandler.Received(1).UnhandledMessage += subscriber;
            level1DynamicMessageHandler.Received(1).UnhandledMessage -= subscriber;
        }
    }
}
