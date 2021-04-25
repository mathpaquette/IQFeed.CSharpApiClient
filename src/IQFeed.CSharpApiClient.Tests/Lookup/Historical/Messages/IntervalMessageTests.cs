using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class IntervalMessageTests
    {
        private readonly string _message;
        private readonly IntervalMessage _expectedMessage;
        
        public IntervalMessageTests()
        {
            _message = $"2018-06-29 16:12:40,185.2500,185.2600,185.2700,185.2800,{long.MaxValue},100,0,";
            _expectedMessage =  new IntervalMessage(new DateTime(2018, 06, 29, 16, 12, 40), 185.25, 185.26, 185.27, 185.28, long.MaxValue, 100, 0);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_IntervalMessage_Invariant(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var intervalMessageFromValues = IntervalMessage.Parse(_message);

            // Assert
            Assert.AreEqual(intervalMessageFromValues, _expectedMessage);
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

        [Test]
        public void Should_TryParse_Return_False_When_Invalid_Data_Overflow()
        {
            // Act
            var parsed = IntervalMessage.TryParse($"2018-06-29 16:12:40,185.2500,185.2600,185.2700,185.2800,0,0,{long.MaxValue},", out _);

            // Assert
            Assert.IsFalse(parsed);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_TryParse_Return_True(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var parsed = IntervalMessage.TryParse(_message, out var intervalMessage);

            // Assert
            Assert.IsTrue(parsed);
            Assert.AreEqual(intervalMessage, _expectedMessage);
        }
    }
}