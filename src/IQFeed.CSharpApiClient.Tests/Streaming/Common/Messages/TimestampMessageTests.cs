using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Common.Messages
{
    public class TimestampMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TimestampMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "T,20180709 14:18:31";

            // Act
            var timestampMessageParsed = TimestampMessage.Parse(message);
            var timestampMessage = new TimestampMessage(new DateTime(2018, 07, 09, 14, 18, 31));

            // Assert
            Assert.AreEqual(timestampMessageParsed, timestampMessage);
        }
    }
}