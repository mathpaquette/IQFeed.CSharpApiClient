using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class IntervalMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_IntervalMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = $"2018-06-29 16:12:40,185.2500,185.2600,185.2700,185.2800,{long.MaxValue},100,0,";

            // Act
            var intervalMessage = new IntervalMessage<double>(new DateTime(2018, 06, 29, 16, 12, 40), 185.25d, 185.26d, 185.27d, 185.28d, long.MaxValue, 100, 0);
            var intervalMessageFromValues = IntervalMessage.Parse(message);

            // Assert
            Assert.AreEqual(intervalMessage, intervalMessageFromValues);
        }
    }
}