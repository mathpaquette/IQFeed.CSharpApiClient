using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_DailyWeeklyMonthlyMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2018-04-27,164.3300,160.6300,164.0000,162.3200,35655839,0,";

            // Act
            var dailyWeeklyMonthlyMessage = new DailyWeeklyMonthlyMessage<double>(new DateTime(2018,04,27), 164.33d, 160.63d, 164d, 162.32d, 35655839, 0);
            var dailyWeeklyMonthlyMessageFromValues = DailyWeeklyMonthlyMessage.Parse(message);

            // Assert
            Assert.AreEqual(dailyWeeklyMonthlyMessage, dailyWeeklyMonthlyMessageFromValues);
        }

        [Test]
        public void Should_Parse_DailyWeeklyMonthlyMessage_With_Large_PeriodVolume()
        {
            // Arrange
            var message = $"2018-04-27,164.3300,160.6300,164.0000,162.3200,{long.MaxValue},0,";

            // Act
            var dailyWeeklyMonthlyMessage = DailyWeeklyMonthlyMessage.Parse(message);

            // Assert
            Assert.AreEqual(dailyWeeklyMonthlyMessage.PeriodVolume, long.MaxValue);
        }
    }
}