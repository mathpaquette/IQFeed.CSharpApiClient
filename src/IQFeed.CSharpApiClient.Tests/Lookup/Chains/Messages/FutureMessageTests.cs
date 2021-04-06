using System;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class FutureMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FutureMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "LC,@ESU18,@ESZ18,@ESH19,@ESM19,@ESU19,";
            var messageWithRequestId = "TESTREQUEST,LC,@ESU18,@ESZ18,@ESH19,@ESM19,@ESU19,";

            // Act
            var futureMessageParsed = FutureMessage.Parse(message);
            var futureMessageWithRequestIdParsed = FutureMessage.ParseWithRequestId(messageWithRequestId);
            var futureMessage = new Future("@ESU18", "@ES", new DateTime(2018, 09, 01));

            // Assert
            Assert.AreEqual(futureMessageParsed.Chains.First(), futureMessage);
            Assert.AreEqual("TESTREQUEST", futureMessageWithRequestIdParsed.RequestId);
            Assert.AreEqual(futureMessageWithRequestIdParsed.Chains.First(), futureMessage);
        }
    }
}