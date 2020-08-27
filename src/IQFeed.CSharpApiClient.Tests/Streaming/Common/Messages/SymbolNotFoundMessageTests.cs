using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Common.Messages
{
    public class SymbolNotFoundMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolNotFoundMessage_Culture_Independent(string cultureName)
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