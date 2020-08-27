using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class NewsMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_NewsMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "N,1234,1122,AAPL:NFLX,20000102 152001,Very cool news,";

            // Act
            var newsMessageParsed = NewsMessage.Parse(message);
            var newsMessage = new NewsMessage("1234", "1122", "AAPL:NFLX",new DateTime(2000,01,02, 15,20,01),"Very cool news");

            // Arrange
            Assert.AreEqual(newsMessageParsed, newsMessage);
        }
    }
}