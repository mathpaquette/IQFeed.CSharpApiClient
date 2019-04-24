using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2.Messages
{
    public class TimestampMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TimestampMessage_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            char[] chr = { '\r' };
            var message = "T,20190417 14:18:31\r";

            // Act
            var timestampMessageParsed = TimestampMessage.Parse(message.TrimEnd(chr));
            var timestampMessage = new TimestampMessage(new DateTime(2019, 04, 17, 14, 18, 31));

            // Assert
            Assert.AreEqual(timestampMessageParsed, timestampMessage);
        }
    }
}
