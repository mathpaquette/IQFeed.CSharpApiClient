using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.News.Enums;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Lookup.News
{
    public class NewsFacadeTests
    {
        private const int TimeoutMs = 30000;
        private static readonly string[] Symbols = { "AAPL", "MSFT" };
        private const string RequestId = "TEST";

        private LookupClient _lookupClient;

        public NewsFacadeTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _lookupClient = LookupClientFactory.CreateNew();
            _lookupClient.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _lookupClient.Disconnect();
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_Lines_When_GetNewsConfigurationAsync()
        {
            var lines = await _lookupClient.News.GetNewsConfigurationAsync(FormatType.XML);
            Assert.Greater(lines.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_NewsHeadlinesMessages_When_GetNewsHeadlinesAsync()
        {
            var newsHeadlinesMessages = await _lookupClient.News.GetNewsHeadlinesAsync(limit: 10, requestId: RequestId);
            Assert.AreEqual(newsHeadlinesMessages.Count(), 10);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_Lines_When_GetNewsStoryAsync()
        {
            var newsHeadline = (await _lookupClient.News.GetNewsHeadlinesAsync(limit: 1)).First();
            var lines = await _lookupClient.News.GetNewsStoryAsync(newsHeadline.HeadlineId, NewsFormatType.XML);
            Assert.Greater(lines.Count(), 0);
        }

        [Test, MaxTime(TimeoutMs)]
        public async Task Should_Return_Lines_When_GetNewsStoryCountAsync()
        {
            var lines = await _lookupClient.News.GetNewsStoryCountAsync(Symbols, FormatType.Text);
            Assert.Greater(lines.Count(), 0);
        }
    }
}