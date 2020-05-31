﻿using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class TickMessageTests
    {
        private readonly string _message;
        private readonly string _messageWithRequestId;

        public TickMessageTests()
        {
            _message = "2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A";
            _messageWithRequestId = "XYZ,2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A";
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TickMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);


            // Act
            var tickMessageParsed = TickMessage.Parse(_message);

            var timestamp = DateTime.ParseExact("2018-04-17 17:51:22.123456", TickMessage.TickDateTimeFormat, CultureInfo.InvariantCulture);
            var tickMessage = new TickMessage(timestamp, 96.07, 1061909, 0, 0.0, 0.0, 4145784264, 'O', 19, "143A");

            // Assert
            Assert.AreEqual(tickMessageParsed, tickMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_Message(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.Parse(_message);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.Parse(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_Message_With_RequestId(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.ParseWithRequestId(_messageWithRequestId);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.ParseWithRequestId(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }
    }
}