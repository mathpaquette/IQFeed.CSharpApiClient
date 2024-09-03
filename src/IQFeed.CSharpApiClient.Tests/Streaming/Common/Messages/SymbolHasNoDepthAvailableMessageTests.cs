using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Common.Messages
{
    public class SymbolHasNoDepthAvailableMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolHasNoDepthAvailableMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "q,MSFT";

            // Act
            var symbolHasNoDepthAvailableMessageParsed = SymbolHasNoDepthAvailableMessage.Parse(message);
            var symbolHasNoDepthAvailableMessage = new SymbolHasNoDepthAvailableMessage("MSFT");

            // Assert
            Assert.AreEqual(symbolHasNoDepthAvailableMessageParsed, symbolHasNoDepthAvailableMessage);
        }
    }
}