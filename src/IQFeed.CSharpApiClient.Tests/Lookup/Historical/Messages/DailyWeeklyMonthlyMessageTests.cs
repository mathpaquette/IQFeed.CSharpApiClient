using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_DailyWeeklyMonthlyMessage_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2018-04-27,164.3300,160.6300,164.0000,162.3200,35655839,0,";

            // Act
            var dailyWeeklyMonthlyMessage =
                new DailyWeeklyMonthlyMessage(new DateTime(2018, 04, 27), 164.33, 160.63, 164, 162.32, 35655839, 0);
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

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Csv_DailyWeeklyMonthlyMessage_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var dailyWeeklyMonthlyMessage = new DailyWeeklyMonthlyMessage(new DateTime(2020, 01, 02, 9, 30, 0), 1.21, 1.22, 1.23, 1.24, 10, 1);

            // Act
            var csv = dailyWeeklyMonthlyMessage.ToCsv();

            // Assert
            var expectedCsv = "2020-01-02,1.21,1.22,1.23,1.24,10,1";
            Assert.AreEqual(csv, expectedCsv);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Csv_DailyWeeklyMonthlyMessage_With_RequestId_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var dailyWeeklyMonthlyMessage = new DailyWeeklyMonthlyMessage(new DateTime(2020, 01, 02, 9, 30, 0), 1.21, 1.22, 1.23, 1.24, 10, 1, "TEST123");

            // Act
            var csv = dailyWeeklyMonthlyMessage.ToCsv();

            // Assert
            var expectedCsv = "TEST123,2020-01-02,1.21,1.22,1.23,1.24,10,1";
            Assert.AreEqual(csv, expectedCsv);
        }
    }
}