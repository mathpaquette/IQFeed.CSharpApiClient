using System;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Derivative.Messages
{
    public class IntervalBarMessageTest
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0";
            
            // Act
            var intervalBarMessage = new IntervalBarMessage(IntervalBarType.C, "AAPL", new DateTime(2018, 1, 1, 9, 30, 0), 100.01, 101.23, 99.98, 100.93, 143562, 745, 0);
            var intervalBarMessageParsed = IntervalBarMessage.Parse(message);

            // Assert
            Assert.AreEqual(intervalBarMessage, intervalBarMessageParsed);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_Without_RequestId(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0";

            // Act
            var intervalBarMessage = new IntervalBarMessage(IntervalBarType.C, "AAPL", new DateTime(2018, 1, 1, 9, 30, 0), 100.01, 101.23, 99.98, 100.93, 143562, 745, 0, "TESTREQUEST");
            var intervalBarMessageParsed = IntervalBarMessage.ParseWithRequestId(message);

            // Assert
            Assert.AreEqual(intervalBarMessage, intervalBarMessageParsed);
        }
    }
}