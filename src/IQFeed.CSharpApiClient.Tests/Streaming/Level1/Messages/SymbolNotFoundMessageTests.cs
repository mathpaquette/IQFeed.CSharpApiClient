using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class SymbolNotFoundMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolNotFoundMessage_Culture_Independant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "n,AAPL";

            // Act
            var symbolNotFoundMessageParsed = SymbolNotFoundMessage.Parse(message);
            var symbolNotFoundMessage = new SymbolNotFoundMessage("AAPL");

            // Assert
            Assert.AreEqual(symbolNotFoundMessageParsed, symbolNotFoundMessage);
        }
    }
}