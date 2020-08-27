using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class IntervalMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_IntervalMessage_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = $"2018-06-29 16:12:40,185.2500,185.2600,185.2700,185.2800,{long.MaxValue},100,0,";

            // Act
            var intervalMessage = new IntervalMessage(new DateTime(2018, 06, 29, 16, 12, 40), 185.25, 185.26, 185.27, 185.28, long.MaxValue, 100, 0);
            var intervalMessageFromValues = IntervalMessage.Parse(message);

            // Assert
            Assert.AreEqual(intervalMessage, intervalMessageFromValues);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Csv_IntervalMessage_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var intervalMessage = new IntervalMessage(new DateTime(2020, 01, 01, 9, 30, 00), 1.01, 1.02, 1.03, 1.04, 1000, 1, 10);

            // Act
            var csv = intervalMessage.ToCsv();

            // Arrange
            var expectedCsv = "2020-01-01 09:30:00,1.01,1.02,1.03,1.04,1000,1,10";
            Assert.AreEqual(csv, expectedCsv);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Csv_IntervalMessage_With_RequestId_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var intervalMessage = new IntervalMessage(new DateTime(2020, 01, 01, 9, 30, 00), 1.01, 1.02, 1.03, 1.04, 1000, 1, 10, "TEST123");

            // Act
            var csv = intervalMessage.ToCsv();

            // Arrange
            var expectedCsv = "TEST123,2020-01-01 09:30:00,1.01,1.02,1.03,1.04,1000,1,10";
            Assert.AreEqual(csv, expectedCsv);
        }
    }
}