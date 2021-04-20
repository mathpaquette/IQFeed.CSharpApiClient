using System;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class FutureSpreadMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FutureSpreadMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "LC,@ESU18-@ESH19,@ESU18-@ESM19,@ESU18-@ESU19,@ESU18-@ESZ18,@ESZ18-@ESH19,@ESZ18-@ESM19,@ESZ18-@ESU19,@ESH19-@ESM19,@ESH19-@ESU19,@ESM19-@ESU19,";
            var messageWithRequestId = "TESTREQUEST,LC,@ESU18-@ESH19,@ESU18-@ESM19,@ESU18-@ESU19,@ESU18-@ESZ18,@ESZ18-@ESH19,@ESZ18-@ESM19,@ESZ18-@ESU19,@ESH19-@ESM19,@ESH19-@ESU19,@ESM19-@ESU19,";

            // Act
            var futureSpreadMessageParsed = FutureSpreadMessage.Parse(message);
            var futureSpreadMessageWithRequestIdParsed = FutureSpreadMessage.ParseWithRequestId(messageWithRequestId);
            var futureSpread = new FutureSpread("@ESU18-@ESH19", new Future("@ESU18", "@ES", new DateTime(2018, 09, 01)), new Future("@ESH19", "@ES", new DateTime(2019, 03, 01)));

            // Assert
            Assert.AreEqual(futureSpreadMessageParsed.Chains.First(), futureSpread);
            Assert.AreEqual("TESTREQUEST", futureSpreadMessageWithRequestIdParsed.RequestId);
            Assert.AreEqual(futureSpreadMessageWithRequestIdParsed.Chains.First(), futureSpread);
        }
    }
}