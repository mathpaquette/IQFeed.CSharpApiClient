using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;
using NUnit.Framework;
using System;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Derivative
{
    public class IntervalBarMessageTest
    {
        [TestCase(
            ",BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0",
            IntervalBarType.C, "AAPL", 2018, 1, 1, 9, 30, 0, 100.01,
            101.23, 99.98, 100.93, 143562, 745, 0, null)]
        [TestCase(
            ",BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0,",
            IntervalBarType.C, "AAPL", 2018, 1, 1, 9, 30, 0, 100.01,
            101.23, 99.98, 100.93, 143562, 745, 0, null)]
        [TestCase(
            "test-request,BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0,",
            IntervalBarType.C, "AAPL", 2018, 1, 1, 9, 30, 0, 100.01,
            101.23, 99.98, 100.93, 143562, 745, 0, "test-request")]
        [TestCase(
            "test-request,BC,AAPL,2018-01-01 09:30:00,100.01,101.23,99.98,100.93,143562,745,0",
            IntervalBarType.C, "AAPL", 2018, 1, 1, 9, 30, 0, 100.01,
            101.23, 99.98, 100.93, 143562, 745, 0, "test-request")]
        public void Should_Parse(
            string bar, IntervalBarType type, string symbol, int year, int month, int day,
            int hour, int minute, int seconds, decimal open, decimal high, decimal low,
            decimal last, int cumulativeVolume, int intervalVolume, int numberOfTrades,
            string requestId)
        {
            Assert.IsTrue(IntervalBarMessage.TryParse(
                bar, out IntervalBarMessage<decimal> msg));
            Assert.AreEqual(
                new IntervalBarMessage<decimal>(
                    type, symbol, new DateTime(year, month, day, hour, minute, seconds),
                    open, high, low, last, cumulativeVolume, intervalVolume,
                    numberOfTrades, requestId).ToString(),
                msg.ToString());
        }
    }
}