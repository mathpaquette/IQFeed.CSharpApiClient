using IQFeed.CSharpApiClient.Lookup.Symbol;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;

using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Symbol
{
    public class SymbolRequestFormatterTests
    {
        private SymbolRequestFormatter _symbolRequestFormatter;

        [SetUp]
        public void SetUp()
        {
            _symbolRequestFormatter = new SymbolRequestFormatter();
        }

        [Test]
        public void ReqSymbolsByFilter_Symbols_And_SecutiryTypes()
        {
            var request = _symbolRequestFormatter.ReqSymbolsByFilter(FieldToSearch.Symbols, "AAPL", FilterType.SecurityTypes, new int[] { 1 });
            Assert.AreEqual("SBF,s,AAPL,t,1,\r\n", request);
        }

        [Test]
        public void ReqSymbolsByFilter_Symbols_And_SecutiryTypes_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqSymbolsByFilter(FieldToSearch.Symbols, "AAPL", FilterType.SecurityTypes, new int[] { 1 }, "abcd");
            Assert.AreEqual("SBF,s,AAPL,t,1,abcd\r\n", request);
        }

        [Test]
        public void ReqSymbolsByFilter_Description_And_ListedMarkets()
        {
            var request = _symbolRequestFormatter.ReqSymbolsByFilter(FieldToSearch.Descriptions, "Apple", FilterType.ListedMarkets, new int[] { 1, 2, 3 });
            Assert.AreEqual("SBF,d,Apple,e,1 2 3,\r\n", request);
        }

        [Test]
        public void ReqSymbolsBySicCode()
        {
            var request = _symbolRequestFormatter.ReqSymbolsBySicCode("1234");
            Assert.AreEqual("SBS,1234,\r\n", request);
        }

        [Test]
        public void ReqSymbolsBySicCode_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqSymbolsBySicCode("78", "req");
            Assert.AreEqual("SBS,78,req\r\n", request);
        }

        [Test]
        public void ReqSymbolsByNaicsCode()
        {
            var request = _symbolRequestFormatter.ReqSymbolsByNaicsCode("123");
            Assert.AreEqual("SBN,123,\r\n", request);
        }

        [Test]
        public void ReqSymbolsByNaicsCode_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqSymbolsByNaicsCode("89", "req2");
            Assert.AreEqual("SBN,89,req2\r\n", request);
        }

        [Test]
        public void ReqReqListedMarkets()
        {
            var request = _symbolRequestFormatter.ReqListedMarkets();
            Assert.AreEqual("SLM,\r\n", request);
        }

        [Test]
        public void ReqReqListedMarkets_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqListedMarkets("req3");
            Assert.AreEqual("SLM,req3\r\n", request);
        }

        [Test]
        public void ReqSecurityTypes()
        {
            var request = _symbolRequestFormatter.ReqSecurityTypes();
            Assert.AreEqual("SST,\r\n", request);
        }

        [Test]
        public void ReqSecurityTypes_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqSecurityTypes("req4");
            Assert.AreEqual("SST,req4\r\n", request);
        }

        [Test]
        public void ReqTradeConditions()
        {
            var request = _symbolRequestFormatter.ReqTradeConditions();
            Assert.AreEqual("STC,\r\n", request);
        }

        [Test]
        public void ReqTradeConditions_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqTradeConditions("req5");
            Assert.AreEqual("STC,req5\r\n", request);
        }

        [Test]
        public void ReqSicCodes()
        {
            var request = _symbolRequestFormatter.ReqSicCodes();
            Assert.AreEqual("SSC,\r\n", request);
        }

        [Test]
        public void ReqSicCodes_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqSicCodes("req6");
            Assert.AreEqual("SSC,req6\r\n", request);
        }

        [Test]
        public void ReqNaicsCodes()
        {
            var request = _symbolRequestFormatter.ReqNaicsCodes();
            Assert.AreEqual("SNC,\r\n", request);
        }

        [Test]
        public void ReqNaicsCodes_WithRequestId()
        {
            var request = _symbolRequestFormatter.ReqNaicsCodes("req7");
            Assert.AreEqual("SNC,req7\r\n", request);
        }
    }
}