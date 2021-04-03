using System;
using IQFeed.CSharpApiClient.Extensions.Lookup.Historical;
using IQFeed.CSharpApiClient.Extensions.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Lookup.Historical
{
    public class HistoricalBarTests
    {
        private HistoricalBar _historicalBar;

        [SetUp]
        public void SetUp()
        {
            _historicalBar = new HistoricalBar(new DateTime(2020, 01, 01, 9, 30, 00), 1.21, 1.22, 1.23, 1.24, 1000, 1, 100, 1, 12345.12345);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Return_Csv(string cultureName)
        {
            // Act
            TestHelper.SetThreadCulture(cultureName);
            var csv = _historicalBar.ToCsv();

            // Assert
            var expectedCsv = "2020-01-01 09:30:00,1.21,1.22,1.23,1.24,1000,1,100,1,12345.1234";
            Assert.AreEqual(expectedCsv, csv);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var csv = "2020-01-01 09:30:00,1.21,1.22,1.23,1.24,1000,1,100,1,12345.12345";

            // Act
            var parsed = HistoricalBar.Parse(csv);

            // Assert
            Assert.AreEqual(parsed, _historicalBar);
        }
    }
}