using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class SymbolByFilterMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByFilterMessage_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "E,7,1,ENI,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage = new SymbolByFilterMessage("E", 7, 1, "ENI");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByFilterMessageWithCommasInDescription_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "Z,1,1,A,B,C,D,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage = new SymbolByFilterMessage("Z", 1, 1, "A,B,C,D");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
        }
    }
}