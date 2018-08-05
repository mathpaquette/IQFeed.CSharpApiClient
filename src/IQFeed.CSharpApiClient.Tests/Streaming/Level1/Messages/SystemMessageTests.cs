using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class SystemMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SystemMessage_Culture_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "S,STATS,,,0,0,1,0,0,0,,,Not Connected,6.0.0.4,474614,0,0,,0.02,0.02,,";

            // Act
            var systemMessageParsed = SystemMessage.Parse(message);
            var systemMessage = new SystemMessage("STATS", message);

            // Assert
            Assert.AreEqual(systemMessageParsed, systemMessage);
        }
    }
}