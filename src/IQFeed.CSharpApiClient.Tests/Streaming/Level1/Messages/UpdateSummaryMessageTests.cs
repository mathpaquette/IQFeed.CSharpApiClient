using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level1.Messages
{
    public class UpdateSummaryMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_UpdateSummaryMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "P,AAPL,188.3500,52500,19:59:14.503633,19,0,188.2500,100,188.3600,100,,,,187.9700,Cbacv,8801,2,17,";

            // Act
            var updateSummaryMessageParsed = UpdateSummaryMessage.Parse(message);
            DateTime.TryParseExact("19:59:14.503633", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            var updateSummaryMessage = new UpdateSummaryMessage<double>("AAPL", 188.35d, 52500, timestamp, 19, 0, 188.25d, 100, 188.36d, 100, 0, 0, 0, 187.97d, "Cbacv", "8801", 2, 17);

            // Assert
            Assert.AreEqual(updateSummaryMessageParsed, updateSummaryMessage);
        }
    }
}