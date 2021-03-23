using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Streaming.Level1;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Level1
{
    public class Level1ClientTests
    {
        private const string Symbol = "AAPL";
        private const string NotFoundSymbol = "NotFoundSymbol";
        private ILevel1Client _level1Client;

        public Level1ClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _level1Client = Level1ClientFactory.CreateNew();
            _level1Client.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _level1Client.Disconnect();
        }

        [Test]
        public async Task Should_Return_FundamentalMessage()
        {
            // Act
            var fundamentalMessage = await _level1Client.GetFundamentalSnapshotAsync(Symbol);

            // Assert
            Assert.AreEqual(fundamentalMessage.Symbol, Symbol);
        }

        [Test]
        public async Task Should_Return_UpdateSummaryMessage()
        {
            // Act
            var updateSummaryMessage = await _level1Client.GetUpdateSummarySnapshotAsync(Symbol);

            // Assert
            Assert.AreEqual(updateSummaryMessage.Symbol, Symbol);
        }

        [Test]
        public async Task Should_Return_UpdateSummaryMessageWithDynamicFields()
        {
            // Arrange
            var fieldNames = new DynamicFieldset[]
            {
                DynamicFieldset.Symbol, DynamicFieldset.MostRecentTrade, DynamicFieldset.MostRecentTradeSize, 
                DynamicFieldset.MostRecentTradeTime, DynamicFieldset.MostRecentTradeMarketCenter,
                DynamicFieldset.TotalVolume, DynamicFieldset.Bid, DynamicFieldset.BidSize, 
                DynamicFieldset.Ask, DynamicFieldset.AskSize, DynamicFieldset.Open, DynamicFieldset.High, 
                DynamicFieldset.Low, DynamicFieldset.Close, DynamicFieldset.MessageContents, 
                DynamicFieldset.MostRecentTradeConditions, DynamicFieldset.MostRecentTradeAggressor,
                DynamicFieldset.MostRecentTradeDayCode
            };

            var level1MessageHandler = new Level1MessageDynamicHandler();
            var level1Client = Level1ClientFactory.CreateNew(level1MessageHandler);

            try
            {
                // Act
                level1Client.Connect();

                level1Client.SelectUpdateFieldName(fieldNames);
                var updateSummaryMessage = await level1Client.GetUpdateSummarySnapshotAsync(Symbol);

                // Assert
                Assert.AreEqual(updateSummaryMessage.DynamicFields.Symbol, Symbol);

            }
            finally
            {
                level1Client.Disconnect();
            }

        [Test]
        public void Should_Throw_Exceptions_When_SymbolNotFound_FundamentalSnapshot()
        {
            // Assert
            Assert.ThrowsAsync<SymbolNotFoundIQFeedException>(async () => await _level1Client.GetFundamentalSnapshotAsync(NotFoundSymbol));
        }

        [Test]
        public void Should_Throw_Exceptions_When_SymbolNotFound_For_UpdateSummarySnapshot()
        {
            // Assert
            Assert.ThrowsAsync<SymbolNotFoundIQFeedException>(async () => await _level1Client.GetUpdateSummarySnapshotAsync(NotFoundSymbol));

        }
    }
}