using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class IntervalMessageTests
    {
        [Test]
        public void Should_Create_IntervalMessage_From_String_Values()
        {
            // Arrange
            var intervalMessageValues = "2018-06-29 16:12:40,185.2500,185.2600,185.2700,185.2800,20671162,100,0,".Split(IQFeedDefault.ProtocolDelimiterCharacter);

            // Act
            var intervalMessage = new IntervalMessage(new DateTime(2018, 06, 29, 16, 12, 40), 185.25f, 185.26f, 185.27f, 185.28f, 20671162, 100);
            var intervalMessageFromValues = IntervalMessage.CreateIntervalMessage(intervalMessageValues);

            // Assert
            Assert.AreEqual(intervalMessage, intervalMessageFromValues);
        }
    }
}