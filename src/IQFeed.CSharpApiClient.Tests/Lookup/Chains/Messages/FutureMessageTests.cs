using System;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains.Messages
{
    public class FutureMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_FutureOption_Message_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var futureSymbol = "@ESH19";

            // Act
            var futureMessageParsed = FutureMessage.Parse(futureSymbol);
            var futureMessage = new FutureMessage(futureSymbol, "@ES", new DateTime(2019, 03, 01));

            // Assert
            Assert.AreEqual(futureMessageParsed, futureMessage);
        }
    }
}