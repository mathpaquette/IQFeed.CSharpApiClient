using System;
using IQFeed.CSharpApiClient.Lookup.Historical;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Historical
{
    public class HistoricalRequestFormatterTests
    {
        private HistoricalRequestFormatter _historicalRequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _historicalRequestFormatter = new HistoricalRequestFormatter();
        }

        [Test]
        public void ReqHistoryTickDatapoints()
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDatapoints("appl", 500, 1, "TEST", 500);
            Assert.AreEqual(request, "HTX,APPL,500,1,TEST,500\r\n");
        }

        [Test]
        public void ReqHistoryTickTimeframe()
        {
            var request = _historicalRequestFormatter.ReqHistoryTickTimeframe("aapl", new DateTime(2000, 01, 01, 9, 30, 00), new DateTime(2001, 01, 01, 16, 00, 00), 100000, new TimeSpan(9, 30, 00), new TimeSpan(16, 00, 00), 1, "TEST", 500);
            Assert.AreEqual(request, "HTT,AAPL,20000101 093000,20010101 160000,100000,093000,160000,1,TEST,500\r\n");
        }

        [Test]
        public void ReqHistoryIntervalDatapoints()
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDatapoints("aapl", 5, 10000, 0, "TEST", 25000, HistoricalIntervalType.V);
            Assert.AreEqual(request, "HIX,AAPL,5,10000,0,TEST,25000,v\r\n");
        }

        [Test]
        public void ReqHistoryIntervalDays()
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDays("aapl", 15, 25, 25000, new TimeSpan(9, 30, 00), new TimeSpan(16, 00, 00), 1, "TEST", 25000, HistoricalIntervalType.T);
            Assert.AreEqual(request, "HID,AAPL,15,25,25000,093000,160000,1,TEST,25000,t\r\n");
        }

        [Test]
        public void ReqHistoryIntervalTimeframe()
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalTimeframe("appl", 5, new DateTime(2000, 01, 01, 9, 30, 00), new DateTime(2001, 01, 01, 16, 00, 00), 25000, new TimeSpan(9, 30, 00), new TimeSpan(16, 00, 00), 1, "TEST", 15000, HistoricalIntervalType.S);
            Assert.AreEqual(request, "HIT,APPL,5,20000101 093000,20010101 160000,25000,093000,160000,1,TEST,15000,s\r\n");
        }

        [Test]
        public void ReqHistoryDailyDatapoints()
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyDatapoints("aapl", 180, 0, "TEST", 500);
            Assert.AreEqual(request, "HDX,AAPL,180,0,TEST,500\r\n");
        }

        [Test]
        public void ReqHistoryTickDays()
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDays("aapl", 180, 2000, new TimeSpan(9, 30, 00), new TimeSpan(16, 00, 00), 0, "TEST", 500);
            Assert.AreEqual(request, "HTD,AAPL,180,2000,093000,160000,0,TEST,500\r\n");
        }

        [Test]
        public void ReqHistoryDailyTimeframe()
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyTimeframe("aapl", new DateTime(2000, 01, 01), new DateTime(2010, 01, 01), 15000, 1, "TEST", 25000);
            Assert.AreEqual(request, "HDT,AAPL,20000101,20100101,15000,1,TEST,25000\r\n");
        }

        [Test]
        public void ReqHistoryWeeklyDatapoints()
        {
            var request = _historicalRequestFormatter.ReqHistoryWeeklyDatapoints("aapl", 35000, 0, "TEST", 100);
            Assert.AreEqual(request, "HWX,AAPL,35000,0,TEST,100\r\n");
        }

        [Test]
        public void ReqHistoryMonthlyDatapoints()
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints("aapl", 1000, 1, "TEST", 10000);
            Assert.AreEqual(request, "HMX,AAPL,1000,1,TEST,10000\r\n");
        }
    }
}