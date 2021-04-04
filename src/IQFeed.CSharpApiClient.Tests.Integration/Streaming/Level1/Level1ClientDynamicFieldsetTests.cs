using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Level1
{
    public class Level1ClientDynamicFieldsetTests
    {
        private const string Symbol = "AAPL";
        private const string NotFoundSymbol = "NotFoundSymbol";
        private ILevel1Client _level1Client;
        private Level1MessageHandler _level1MessageHandler;

        private DynamicFieldset[] _fieldNames = new DynamicFieldset[]
        {
            DynamicFieldset.Symbol, DynamicFieldset.MostRecentTrade, DynamicFieldset.MostRecentTradeSize,
            DynamicFieldset.MostRecentTradeTime, DynamicFieldset.MostRecentTradeMarketCenter,
            DynamicFieldset.TotalVolume, DynamicFieldset.Bid, DynamicFieldset.BidSize,
            DynamicFieldset.Ask, DynamicFieldset.AskSize, DynamicFieldset.Open, DynamicFieldset.High,
            DynamicFieldset.Low, DynamicFieldset.Close, DynamicFieldset.MessageContents,
            DynamicFieldset.MostRecentTradeConditions, DynamicFieldset.MostRecentTradeAggressor,
            DynamicFieldset.MostRecentTradeDayCode
        };


        public Level1ClientDynamicFieldsetTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _level1Client = Level1ClientFactory.CreateNew();
            _level1Client = Level1ClientFactory.CreateNew(_level1MessageHandler);
            _level1Client.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _level1Client.Disconnect();
        }

        [Test]
        public async Task Should_Return_UpdateSummaryMessageWithDynamicFields()
        {
            // Arrange
            //  already done in setup

            // Act
            var updateSummaryMessage = await _level1Client.GetUpdateSummarySnapshotAsync(Symbol);

            // Assert
            Assert.AreEqual(updateSummaryMessage.DynamicFields.Symbol, Symbol);
        }
    }
}
