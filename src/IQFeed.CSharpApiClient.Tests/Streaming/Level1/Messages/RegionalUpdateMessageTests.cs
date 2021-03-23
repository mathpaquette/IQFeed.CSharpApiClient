using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class RegionalUpdateMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_RegionalUpdateMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "R,AAPL,X,1.23,100,13:00:00,1.24,200,13:00:01,0,1,2,";

            // Act
            var regionalUpdateMessageParsed = RegionalUpdateMessage.Parse(message);
            var regionalUpdateMessage = new RegionalUpdateMessage("AAPL", "X", 1.23, 100, new DateTime(2000, 1, 2, 13, 00, 00), 1.24, 200, new DateTime(2000, 1, 2, 13, 00, 01), 0, 1, 2);

            // Arrange
            Assert.AreEqual(regionalUpdateMessageParsed, regionalUpdateMessage);
        }
    }
}