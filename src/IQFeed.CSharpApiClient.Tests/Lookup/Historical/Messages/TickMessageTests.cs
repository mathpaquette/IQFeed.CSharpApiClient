using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical.Messages
{
    public class TickMessageTests
    {
        private readonly string _messageProtocol60;
        private readonly string _messageProtocol60WithRequestId;
        private readonly string _messageProtocol61;
        private readonly string _messageProtocol61WithRequestId;
        private readonly string _messageProtocol62;
        private readonly string _messageProtocol62WithRequestId;
        private readonly TickMessage _expectedMessageProtocol60;
        private readonly TickMessage _expectedMessageProtocol61;
        private readonly TickMessage _expectedMessageProtocol62;

        public TickMessageTests()
        {
            _messageProtocol60 = "2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A";
            _messageProtocol60WithRequestId = "XYZ,2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A";
            _messageProtocol61 = "2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A,2,17";
            _messageProtocol61WithRequestId = "XYZ,2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A,2,17";
            _messageProtocol62 = "LH,2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A,2,17";
            _messageProtocol62WithRequestId = "XYZ,LH,2018-04-17 17:51:22.123456,96.0700,1061909,0,0.0000,0.0000,4145784264,O,19,143A,2,17";

            var timestamp = DateTime.ParseExact("2018-04-17 17:51:22.123456", TickMessage.TickDateTimeFormat, CultureInfo.InvariantCulture);
            _expectedMessageProtocol60 = new TickMessage(timestamp, 96.07, 1061909, 0, 0.0, 0.0, 4145784264, 'O', 19, "143A", 0, 0);
            _expectedMessageProtocol61 = new TickMessage(timestamp, 96.07, 1061909, 0, 0.0, 0.0, 4145784264, 'O', 19, "143A", 2, 17);
            _expectedMessageProtocol62 = new TickMessage(timestamp, 96.07, 1061909, 0, 0.0, 0.0, 4145784264, 'O', 19, "143A", 2, 17);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TickMessage_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var tickMessageParsed = TickMessage.Parse(_messageProtocol61);

            // Assert
            Assert.AreEqual(tickMessageParsed, _expectedMessageProtocol61);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TickMessageProtocol60_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var tickMessageParsed = TickMessage.Parse(_messageProtocol60);

            // Assert
            Assert.AreEqual(tickMessageParsed, _expectedMessageProtocol60);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TickMessageProtocol62_Culture_Independent(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var tickMessageParsed = TickMessage.Parse(_messageProtocol62);

            // Assert
            Assert.AreEqual(tickMessageParsed, _expectedMessageProtocol62);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_Message(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.Parse(_messageProtocol61);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.Parse(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_MessageProtocol60(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.Parse(_messageProtocol60);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.Parse(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_MessageProtocol62(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.Parse(_messageProtocol62);

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
            var tickMessage1 = TickMessage.ParseWithRequestId(_messageProtocol61WithRequestId);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.ParseWithRequestId(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_MessageProtocol60_With_RequestId(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.ParseWithRequestId(_messageProtocol60WithRequestId);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.ParseWithRequestId(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Convert_Csv_To_Original_MessageProtocol62_With_RequestId(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var tickMessage1 = TickMessage.ParseWithRequestId(_messageProtocol62WithRequestId);

            // Act
            var tickMessage1Csv = tickMessage1.ToCsv();
            var tickMessage2 = TickMessage.ParseWithRequestId(tickMessage1Csv);

            // Assert
            Assert.AreEqual(tickMessage2, tickMessage1);
        }

        [Test]
        public void Should_TryParse_Return_False_When_Invalid_Data_Overflow()
        {
            // Act
            var parsed = TickMessage.TryParse($"2018-04-17 17:51:22.123456,96.0700,{long.MaxValue},0,0.0000,0.0000,4145784264,O,19,143A,2,17", out _);

            // Assert
            Assert.IsFalse(parsed);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_TryParse_Return_True(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var parsed = TickMessage.TryParse(_messageProtocol61, out var tickMessage);

            // Assert
            Assert.IsTrue(parsed);
            Assert.AreEqual(tickMessage, _expectedMessageProtocol61);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_TryParse_Protocol60_Return_True(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var parsed = TickMessage.TryParse(_messageProtocol60, out var tickMessage);

            // Assert
            Assert.IsTrue(parsed);
            Assert.AreEqual(tickMessage, _expectedMessageProtocol60);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_TryParse_Protocol62_Return_True(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);

            // Act
            var parsed = TickMessage.TryParse(_messageProtocol62, out var tickMessage);

            // Assert
            Assert.IsTrue(parsed);
            Assert.AreEqual(tickMessage, _expectedMessageProtocol62);
        }

    }
}