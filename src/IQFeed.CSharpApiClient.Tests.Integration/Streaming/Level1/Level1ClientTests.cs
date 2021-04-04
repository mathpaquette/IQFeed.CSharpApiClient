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