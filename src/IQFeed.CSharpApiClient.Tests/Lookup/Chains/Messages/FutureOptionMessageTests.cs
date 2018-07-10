using System;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class FutureOptionMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FutureOptionMessage_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var futureOptionSymbol = "@ESU18C100000";

            // Act
            var futureOptionMessageParsed = FutureOptionMessage.Parse(futureOptionSymbol);
            var futureOptionMessage = new FutureOptionMessage(futureOptionSymbol, new FutureMessage("@ESU18", "@ES", new DateTime(2018, 09, 01)), OptionSide.Call, 1000);

            // Assert
            Assert.AreEqual(futureOptionMessageParsed, futureOptionMessage);
        }
    }
}