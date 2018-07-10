using System;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class FutureSpreadMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FutureSpreadMessage_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var futureSpreadSymbol = "@ESU18-@ESH19";

            // Act
            var futureSpreadMessageParsed = FutureSpreadMessage.CreateFutureSpreadMessage(futureSpreadSymbol);
            var futureSpreadMessage = new FutureSpreadMessage(futureSpreadSymbol, new FutureMessage("@ESU18", "@ES", new DateTime(2018, 09, 01)), new FutureMessage("@ESH19", "@ES", new DateTime(2019, 03, 01)));

            // Assert
            Assert.AreEqual(futureSpreadMessageParsed, futureSpreadMessage);
        }
    }
}