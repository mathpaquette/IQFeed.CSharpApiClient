using IQFeed.CSharpApiClient.Streaming.Level2;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Level2
{
    public class Level2RequestFormatterTests
    {
        private Level2RequestFormatter _level2RequestFormatter;
        [SetUp]
        public void SetUp()
        {
            _level2RequestFormatter = new Level2RequestFormatter();
        }

        [Test]
        public void Should_Format_ReqWatch()
        {
            var request = _level2RequestFormatter.ReqWatch("aapl");
            Assert.AreEqual(request, "wAAPL\r\n");
        }

        [Test]
        public void Should_Format_ReqMarketMakerNameById()
        {
            var request = _level2RequestFormatter.ReqMarketMakerNameById("md02");
            Assert.AreEqual(request, "mMD02\r\n");
        }

        [Test]
        public void Should_Format_ReqUnwatch()
        {
            var request = _level2RequestFormatter.ReqUnwatch("aapl");
            Assert.AreEqual(request, "rAAPL\r\n");
        }

        [Test]
        public void Should_Format_ReqServerConnect()
        {
            var request = _level2RequestFormatter.ReqServerConnect();
            Assert.AreEqual(request, "c\r\n");
        }

        [Test]
        public void Should_Format_ReqServerDisconnect()
        {
            var request = _level2RequestFormatter.ReqServerDisconnect();
            Assert.AreEqual(request, "x\r\n");
        }
    }
}