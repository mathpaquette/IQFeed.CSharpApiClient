using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Symbol.Messages
{
    public class SymbolByFilterMessageTests
    {
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByFilterMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "E,7,1,ENI,";
            var message62 = "LS,E,7,1,ENI,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage62Parsed = SymbolByFilterMessage.Parse(message62);
            var symbolByFilterMessage = new SymbolByFilterMessage("E", 7, 1, "ENI");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
            Assert.AreEqual(symbolByFilterMessage62Parsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByFilterMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "Z,1,1,A,B,C,D,";
            var message62 = "LS,Z,1,1,A,B,C,D,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage62Parsed = SymbolByFilterMessage.Parse(message62);
            var symbolByFilterMessage = new SymbolByFilterMessage("Z", 1, 1, "A,B,C,D");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
            Assert.AreEqual(symbolByFilterMessage62Parsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolByFilterMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,BRSI,3,1,BALLISTIC RECOVERY SYSTEMS, IN,";
            var message62 = "TESTREQUEST,LS,BRSI,3,1,BALLISTIC RECOVERY SYSTEMS, IN,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.ParseWithRequestId(message);
            var symbolByFilterMessage62Parsed = SymbolByFilterMessage.ParseWithRequestId(message62);
            var symbolByFilterMessage = new SymbolByFilterMessage("BRSI", 3, 1, "BALLISTIC RECOVERY SYSTEMS, IN", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
            Assert.AreEqual(symbolByFilterMessage62Parsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolBySicCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "8322,PRSC,1,1,PROVIDENCE SERVICE CORP,";
            var message62 = "LS,8322,PRSC,1,1,PROVIDENCE SERVICE CORP,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.Parse(message);
            var symbolBySicCodeMessage62Parsed = SymbolBySicCodeMessage.Parse(message62);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8322, "PRSC", 1, 1, "PROVIDENCE SERVICE CORP");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
            Assert.AreEqual(symbolBySicCodeMessage62Parsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolBySicCodeMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "8322,PRSC,1,1,PROVIDENCE,SERVICE,CORP,";
            var message62 = "LS,8322,PRSC,1,1,PROVIDENCE,SERVICE,CORP,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.Parse(message);
            var symbolBySicCodeMessage62Parsed = SymbolBySicCodeMessage.Parse(message62);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8322, "PRSC", 1, 1, "PROVIDENCE,SERVICE,CORP");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
            Assert.AreEqual(symbolBySicCodeMessage62Parsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolBySicCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,8361,CHCR,4,1,COMPREHENSIVE CARE,";
            var message62 = "TESTREQUEST,LS,8361,CHCR,4,1,COMPREHENSIVE CARE,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.ParseWithRequestId(message);
            var symbolBySicCodeMessage62Parsed = SymbolBySicCodeMessage.ParseWithRequestId(message62);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8361, "CHCR", 4, 1, "COMPREHENSIVE CARE", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
            Assert.AreEqual(symbolBySicCodeMessage62Parsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByNaicsCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "924110,EESC,4,1,EASTERN ENVTL SOLTNS CP,";
            var message62 = "LS,924110,EESC,4,1,EASTERN ENVTL SOLTNS CP,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.Parse(message);
            var symbolByNaicsCodeMessage62Parsed = SymbolByNaicsCodeMessage.Parse(message62);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(924110, "EESC", 4, 1, "EASTERN ENVTL SOLTNS CP");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
            Assert.AreEqual(symbolByNaicsCodeMessage62Parsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByNaicsCodeMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "924110,EESC,4,1,EASTERN ENVTL SOLTNS, CP,";
            var message62 = "LS,924110,EESC,4,1,EASTERN ENVTL SOLTNS, CP,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.Parse(message);
            var symbolByNaicsCodeMessage62Parsed = SymbolByNaicsCodeMessage.Parse(message62);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(924110, "EESC", 4, 1, "EASTERN ENVTL SOLTNS, CP");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
            Assert.AreEqual(symbolByNaicsCodeMessage62Parsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolByNaicsCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,928110,TCNH,4,1,TECHNEST HOLDINGS INC,";
            var message62 = "TESTREQUEST,LS,928110,TCNH,4,1,TECHNEST HOLDINGS INC,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.ParseWithRequestId(message);
            var symbolByNaicsCodeMessage62Parsed = SymbolByNaicsCodeMessage.ParseWithRequestId(message62);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(928110, "TCNH", 4, 1, "TECHNEST HOLDINGS INC", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
            Assert.AreEqual(symbolByNaicsCodeMessage62Parsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_ListedMarketMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2,NCM,National Capital Market,5,NASDAQ,";
            var message62 = "LS,2,NCM,National Capital Market,5,NASDAQ,";

            // Act
            var listedMarketMessageParsed = ListedMarketMessage.Parse(message);
            var listedMarketMessage62Parsed = ListedMarketMessage.Parse(message62);
            var listedMarketMessage = new ListedMarketMessage(2, "NCM", "National Capital Market", 5, "NASDAQ");

            // Assert
            Assert.AreEqual(listedMarketMessageParsed, listedMarketMessage);
            Assert.AreEqual(listedMarketMessage62Parsed, listedMarketMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_ListedMarketMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,7,NYSE,New York Stock Exchange,7,NYSE,";
            var message62 = "TESTREQUEST,LS,7,NYSE,New York Stock Exchange,7,NYSE,";

            // Act
            var listedMarketMessageParsed = ListedMarketMessage.ParseWithRequestId(message);
            var listedMarketMessage62Parsed = ListedMarketMessage.ParseWithRequestId(message62);
            var listedMarketMessage = new ListedMarketMessage(7, "NYSE", "New York Stock Exchange", 7, "NYSE", "TESTREQUEST");

            // Assert
            Assert.AreEqual(listedMarketMessageParsed, listedMarketMessage);
            Assert.AreEqual(listedMarketMessage62Parsed, listedMarketMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SecurityTypeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "1,EQUITY,Equity,";
            var message62 = "LS,1,EQUITY,Equity,";

            // Act
            var securityTypeMessageParsed = SecurityTypeMessage.Parse(message);
            var securityTypeMessage62Parsed = SecurityTypeMessage.Parse(message62);
            var securityTypeMessage = new SecurityTypeMessage(1, "EQUITY", "Equity");

            // Assert
            Assert.AreEqual(securityTypeMessageParsed, securityTypeMessage);
            Assert.AreEqual(securityTypeMessage62Parsed, securityTypeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SecurityTypeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,2,IEOPTION,Index/Equity Option,";
            var message62 = "TESTREQUEST,LS,2,IEOPTION,Index/Equity Option,";

            // Act
            var securityTypeMessageParsed = SecurityTypeMessage.ParseWithRequestId(message);
            var securityTypeMessage62Parsed = SecurityTypeMessage.ParseWithRequestId(message62);
            var securityTypeMessage = new SecurityTypeMessage(2, "IEOPTION", "Index/Equity Option", "TESTREQUEST");

            // Assert
            Assert.AreEqual(securityTypeMessageParsed, securityTypeMessage);
            Assert.AreEqual(securityTypeMessage62Parsed, securityTypeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TradeConditionMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "1,REGULAR,Normal Trade,";
            var message62 = "LS,1,REGULAR,Normal Trade,";

            // Act
            var tradeConditionMessageParsed = TradeConditionMessage.Parse(message);
            var tradeConditionMessage62Parsed = TradeConditionMessage.Parse(message62);
            var tradeConditionMessage = new TradeConditionMessage(1, "REGULAR", "Normal Trade");

            // Assert
            Assert.AreEqual(tradeConditionMessageParsed, tradeConditionMessage);
            Assert.AreEqual(tradeConditionMessage62Parsed, tradeConditionMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_TradeConditionMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,21,SPLIT,Split Trade,";
            var message62 = "TESTREQUEST,LS,21,SPLIT,Split Trade,";

            // Act
            var tradeConditionMessageParsed = TradeConditionMessage.ParseWithRequestId(message);
            var tradeConditionMessage62Parsed = TradeConditionMessage.ParseWithRequestId(message62);
            var tradeConditionMessage = new TradeConditionMessage(21, "SPLIT", "Split Trade", "TESTREQUEST");

            // Assert
            Assert.AreEqual(tradeConditionMessageParsed, tradeConditionMessage);
            Assert.AreEqual(tradeConditionMessage62Parsed, tradeConditionMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SicCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "0112,RICE,";
            var message62 = "LS,0112,RICE,";

            // Act
            var sicCodeInfoMessageParsed = SicCodeInfoMessage.Parse(message);
            var sicCodeInfoMessage62Parsed = SicCodeInfoMessage.Parse(message62);
            var sicCodeInfoMessage = new SicCodeInfoMessage(112, "RICE");

            // Assert
            Assert.AreEqual(sicCodeInfoMessageParsed, sicCodeInfoMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SicCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,0133,SUGARCANE AND SUGAR BEETS,";
            var message62 = "TESTREQUEST,LS,0133,SUGARCANE AND SUGAR BEETS,";

            // Act
            var sicCodeInfoMessageParsed = SicCodeInfoMessage.ParseWithRequestId(message);
            var sicCodeInfoMessage62Parsed = SicCodeInfoMessage.ParseWithRequestId(message62);
            var sicCodeInfoMessage = new SicCodeInfoMessage(133, "SUGARCANE AND SUGAR BEETS", "TESTREQUEST");

            // Assert
            Assert.AreEqual(sicCodeInfoMessageParsed, sicCodeInfoMessage);
            Assert.AreEqual(sicCodeInfoMessage62Parsed, sicCodeInfoMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_NaicsCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "111110,Soybean Farming,";
            var message62 = "LS,111110,Soybean Farming,";

            // Act
            var naicsCodeInfoMessageParsed = NaicsCodeInfoMessage.Parse(message);
            var naicsCodeInfoMessage62Parsed = NaicsCodeInfoMessage.Parse(message62);
            var naicsCodeInfoMessage = new NaicsCodeInfoMessage(111110, "Soybean Farming");

            // Assert
            Assert.AreEqual(naicsCodeInfoMessageParsed, naicsCodeInfoMessage);
            Assert.AreEqual(naicsCodeInfoMessage62Parsed, naicsCodeInfoMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_NaicsCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,334220,Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing,";
            var message62 = "TESTREQUEST,LS,334220,Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing,";

            // Act
            var naicsCodeInfoMessageParsed = NaicsCodeInfoMessage.ParseWithRequestId(message);
            var naicsCodeInfoMessage62Parsed = NaicsCodeInfoMessage.ParseWithRequestId(message62);
            var naicsCodeInfoMessage = new NaicsCodeInfoMessage(334220, "Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing", "TESTREQUEST");

            // Assert
            Assert.AreEqual(naicsCodeInfoMessageParsed, naicsCodeInfoMessage);
            Assert.AreEqual(naicsCodeInfoMessage62Parsed, naicsCodeInfoMessage);
        }
    }
}