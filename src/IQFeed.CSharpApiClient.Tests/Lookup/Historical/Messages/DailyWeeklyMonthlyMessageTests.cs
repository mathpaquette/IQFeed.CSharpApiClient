using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessageTests
    {
        [Test]
        public void Should_Create_DailyWeeklyMonthlyMessage_From_String_Values()
        {
            // Arrange
            var dailyWeeklyMonthlyMessageValues = "2018-04-27,164.3300,160.6300,164.0000,162.3200,35655839,0,".Split(IQFeedDefault.ProtocolDelimiterCharacter);

            // Act
            var dailyWeeklyMonthlyMessage = new DailyWeeklyMonthlyMessage(new DateTime(2018,04,27), 164.33f, 160.63f, 164f, 162.32f, 35655839, 0);
            var dailyWeeklyMonthlyMessageFromValues = DailyWeeklyMonthlyMessage.CreateDailyWeeklyMonthlyMessage(dailyWeeklyMonthlyMessageValues);

            // Assert
            Assert.AreEqual(dailyWeeklyMonthlyMessage, dailyWeeklyMonthlyMessageFromValues);
        }
    }
}