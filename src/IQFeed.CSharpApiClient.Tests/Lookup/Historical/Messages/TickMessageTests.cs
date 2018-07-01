using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class TickMessageTests
    {
        [Test]
        public void Should_Create_TickMessage_From_String_Values()
        {
            // Arrange
            var tickMessageValues = "2018-04-17 17:51:22.000000,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A".Split(IQFeedDefault.ProtocolDelimiterCharacter);

            // Act
            var tickMessage = new TickMessage(new DateTime(2018, 04, 17, 17, 51, 22), 96.07f, 1061909, 0, 0.0f, 0.0f, 4145784264, 'O', 19, "143A");
            var tickMessageFromValues = TickMessage.CreateTickMessage(tickMessageValues);

            // Assert
            Assert.AreEqual(tickMessage, tickMessageFromValues);
        }
    }
}