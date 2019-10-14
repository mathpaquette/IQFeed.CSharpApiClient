using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level2;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Level2
{
    public class Level2ClientTests
    {
        private const string Symbol = "@ES#";
        private ILevel2Client _level2Client;

        public Level2ClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _level2Client = Level2ClientFactory.CreateNew();
            _level2Client.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _level2Client.Disconnect();
        }

        [Test]
        public async Task Should_Return_UpdateSummaryMessage()
        {
            // Act
            var updateSummaryMessage = await _level2Client.GetUpdateSummarySnapshotAsync(Symbol);

            // Assert
            Assert.AreEqual(updateSummaryMessage.Symbol, Symbol);
        }
    }
}