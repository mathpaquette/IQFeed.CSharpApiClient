using System;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Chains
{
    public class EquityOptionMessageTests
    {
        [Test]
        public void Should_Return_Parsed_EquityOptionMessage()
        {
            // Arrange
            var optionSymbol = "AAPL1806G167.5";

            // Act
            var equityOptionMessage = EquityOptionMessage.CreateEquityIndexOptionMessage(optionSymbol);

            // Assert
            Assert.AreEqual(equityOptionMessage.Symbol, optionSymbol);
            Assert.AreEqual(equityOptionMessage.EquitySymbol, "AAPL");
            Assert.AreEqual(equityOptionMessage.Expiration.Date, new DateTime(2018, 07, 06));
            Assert.AreEqual(equityOptionMessage.Side, OptionSide.Call);
            Assert.AreEqual(equityOptionMessage.StrikePrice, 167.5);
        }
    }
}