using System;
using IQFeed.CSharpApiClient.Streaming.Derivative;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Derivative
{
    public class DerivativeRequestFormatterTests
    {
        private DerivativeRequestFormatter _derivativeRequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _derivativeRequestFormatter = new DerivativeRequestFormatter();
        }

        [Test]
        public void Should_Format_BarWatch()
        {
            var request = _derivativeRequestFormatter.ReqBarWatch("aapl", 60, new DateTime(2018, 01, 01, 09, 30, 00), 10, 1000, new TimeSpan(9, 30, 00), new TimeSpan(16, 00, 00), "TEST", DerivativeIntervalType.S, 5);
            Assert.AreEqual(request, "BW,AAPL,60,20180101 093000,10,1000,093000,160000,TEST,s,5,\r\n");
        }

        [Test]
        public void Should_Format_BarUnwatch()
        {
            var request = _derivativeRequestFormatter.ReqBarUnwatch("aapl", "TEST");
            Assert.AreEqual(request, "BR,AAPL,TEST,\r\n");
        }
    }
}