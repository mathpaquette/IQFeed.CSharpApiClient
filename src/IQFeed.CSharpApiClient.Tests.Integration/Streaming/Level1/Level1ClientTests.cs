using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Level1
{
    public class Level1ClientTests
    {
        private const string Symbol = "AAPL";
        private ILevel1Client<double> _level1Client;

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
    }
}