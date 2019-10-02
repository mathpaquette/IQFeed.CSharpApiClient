using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;
using IQFeed.CSharpApiClient.Tests.Common;
using IQFeed.CSharpApiClient.Tests.Common.TestCases;

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

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage = new SymbolByFilterMessage("E", 7, 1, "ENI");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByFilterMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "Z,1,1,A,B,C,D,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.Parse(message);
            var symbolByFilterMessage = new SymbolByFilterMessage("Z", 1, 1, "A,B,C,D");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolByFilterMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,BRSI,3,1,BALLISTIC RECOVERY SYSTEMS, IN,";

            // Act
            var symbolByFilterMessageParsed = SymbolByFilterMessage.ParseWithRequestId(message);
            var symbolByFilterMessage = new SymbolByFilterMessage("BRSI", 3, 1, "BALLISTIC RECOVERY SYSTEMS, IN", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolByFilterMessageParsed, symbolByFilterMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolBySicCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "8322,PRSC,1,1,PROVIDENCE SERVICE CORP,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.Parse(message);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8322, "PRSC", 1, 1, "PROVIDENCE SERVICE CORP");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolBySicCodeMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "8322,PRSC,1,1,PROVIDENCE,SERVICE,CORP,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.Parse(message);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8322, "PRSC", 1, 1, "PROVIDENCE,SERVICE,CORP");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolBySicCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,8361,CHCR,4,1,COMPREHENSIVE CARE,";

            // Act
            var symbolBySicCodeMessageParsed = SymbolBySicCodeMessage.ParseWithRequestId(message);
            var symbolBySicCodeMessage = new SymbolBySicCodeMessage(8361, "CHCR", 4, 1, "COMPREHENSIVE CARE", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolBySicCodeMessageParsed, symbolBySicCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByNaicsCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "924110,EESC,4,1,EASTERN ENVTL SOLTNS CP,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.Parse(message);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(924110, "EESC", 4, 1, "EASTERN ENVTL SOLTNS CP");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SymbolByNaicsCodeMessage_WithCommasInDescription(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "924110,EESC,4,1,EASTERN ENVTL SOLTNS, CP,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.Parse(message);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(924110, "EESC", 4, 1, "EASTERN ENVTL SOLTNS, CP");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SymbolByNaicsCodeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,928110,TCNH,4,1,TECHNEST HOLDINGS INC,";

            // Act
            var symbolByNaicsCodeMessageParsed = SymbolByNaicsCodeMessage.ParseWithRequestId(message);
            var symbolByNaicsCodeMessage = new SymbolByNaicsCodeMessage(928110, "TCNH", 4, 1, "TECHNEST HOLDINGS INC", "TESTREQUEST");

            // Assert
            Assert.AreEqual(symbolByNaicsCodeMessageParsed, symbolByNaicsCodeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_ListedMarketMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "2,NCM,National Capital Market,5,NASDAQ,";

            // Act
            var listedMarketMessageParsed = ListedMarketMessage.Parse(message);
            var listedMarketMessage = new ListedMarketMessage(2, "NCM", "National Capital Market", 5, "NASDAQ");

            // Assert
            Assert.AreEqual(listedMarketMessageParsed, listedMarketMessage);
        }
        
        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_ListedMarketMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,7,NYSE,New York Stock Exchange,7,NYSE,";

            // Act
            var listedMarketMessageParsed = ListedMarketMessage.ParseWithRequestId(message);
            var listedMarketMessage = new ListedMarketMessage(7, "NYSE", "New York Stock Exchange", 7, "NYSE", "TESTREQUEST");

            // Assert
            Assert.AreEqual(listedMarketMessageParsed, listedMarketMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SecurityTypeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "1,EQUITY,Equity,";

            // Act
            var securityTypeMessageParsed = SecurityTypeMessage.Parse(message);
            var securityTypeMessage = new SecurityTypeMessage(1, "EQUITY", "Equity");

            // Assert
            Assert.AreEqual(securityTypeMessageParsed, securityTypeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_SecurityTypeMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,2,IEOPTION,Index/Equity Option,";

            // Act
            var securityTypeMessageParsed = SecurityTypeMessage.ParseWithRequestId(message);
            var securityTypeMessage = new SecurityTypeMessage(2, "IEOPTION", "Index/Equity Option", "TESTREQUEST");

            // Assert
            Assert.AreEqual(securityTypeMessageParsed, securityTypeMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_TradeConditionMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "1,REGULAR,Normal Trade,";

            // Act
            var tradeConditionMessageParsed = TradeConditionMessage.Parse(message);
            var tradeConditionMessage = new TradeConditionMessage(1, "REGULAR", "Normal Trade");

            // Assert
            Assert.AreEqual(tradeConditionMessageParsed, tradeConditionMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_TradeConditionMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,21,SPLIT,Split Trade,";

            // Act
            var tradeConditionMessageParsed = TradeConditionMessage.ParseWithRequestId(message);
            var tradeConditionMessage = new TradeConditionMessage(21, "SPLIT", "Split Trade", "TESTREQUEST");

            // Assert
            Assert.AreEqual(tradeConditionMessageParsed, tradeConditionMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_SicCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "0112,RICE,";

            // Act
            var sicCodeInfoMessageParsed = SicCodeInfoMessage.Parse(message);
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

            // Act
            var sicCodeInfoMessageParsed = SicCodeInfoMessage.ParseWithRequestId(message);
            var sicCodeInfoMessage = new SicCodeInfoMessage(133, "SUGARCANE AND SUGAR BEETS", "TESTREQUEST");

            // Assert
            Assert.AreEqual(sicCodeInfoMessageParsed, sicCodeInfoMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_Parse_NaicsCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "111110,Soybean Farming,";

            // Act
            var naicsCodeInfoMessageParsed = NaicsCodeInfoMessage.Parse(message);
            var naicsCodeInfoMessage = new NaicsCodeInfoMessage(111110, "Soybean Farming");

            // Assert
            Assert.AreEqual(naicsCodeInfoMessageParsed, naicsCodeInfoMessage);
        }

        [Test, TestCaseSource(typeof(CultureNameTestCase), nameof(CultureNameTestCase.CultureNames))]
        public void Should_ParseWithRequestId_NaicsCodeInfoMessage(string cultureName)
        {
            // Arrange
            TestHelper.SetThreadCulture(cultureName);
            var message = "TESTREQUEST,334220,Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing,";

            // Act
            var naicsCodeInfoMessageParsed = NaicsCodeInfoMessage.ParseWithRequestId(message);
            var naicsCodeInfoMessage = new NaicsCodeInfoMessage(334220, "Radio and Television Broadcasting and Wireless Communications Equipment Manufacturing", "TESTREQUEST");

            // Assert
            Assert.AreEqual(naicsCodeInfoMessageParsed, naicsCodeInfoMessage);
        }
    }
}