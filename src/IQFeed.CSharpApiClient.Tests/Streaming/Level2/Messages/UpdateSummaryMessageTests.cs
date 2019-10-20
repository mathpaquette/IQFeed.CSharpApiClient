using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2.Messages
{
    public class UpdateSummaryMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_UpdateSummaryMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2,@ESM19,MD01,2938.,2938.25,65,23,20:31:04.876740,2019-04-23,52,20:31:04.876740,T,T,F,";

            // Act
            var updateSummaryMessageParsed = UpdateSummaryMessage.Parse(message);
            DateTime.TryParseExact("20:31:04.876740", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTime);
            DateTime.TryParseExact("2019-04-23", UpdateSummaryMessage.UpdateMessageDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
            DateTime.TryParseExact("20:31:04.876740", UpdateSummaryMessage.UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTime);
            var updateSummaryMessage = new UpdateSummaryMessage("@ESM19", "MD01", 2938.0f, 2938.25f, 65, 23, bidTime, date, "52", askTime, 'T', 'T', 'F');

            // Assert
            Assert.AreEqual(updateSummaryMessageParsed, updateSummaryMessage);
        }
    }
}