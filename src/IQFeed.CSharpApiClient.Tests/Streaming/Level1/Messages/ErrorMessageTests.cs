using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class ErrorMessageTests
    {

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_ErrorMessage_Culture_Indepedant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "E,Disconnected from server";

            // Act
            var errorMessageParsed = ErrorMessage.Parse(message);
            var errorMessage = new ErrorMessage("Disconnected from server");

            // Assert
            Assert.AreEqual(errorMessageParsed, errorMessage);
        }
    }
}