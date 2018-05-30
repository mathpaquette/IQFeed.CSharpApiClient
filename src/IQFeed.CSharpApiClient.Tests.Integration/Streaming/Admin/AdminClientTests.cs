using System.Threading;
using IQFeed.CSharpApiClient.Streaming.Admin;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Streaming.Admin
{
    public class AdminClientTests
    {
        private const int TimeoutMs = 30000;
        private AdminClient _adminClient;

        public AdminClientTests()
        {
            IQFeedLauncher.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _adminClient = AdminClientFactory.CreateNew();
            _adminClient.Connect();
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Receive_StatsMessage_When_Connected()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _adminClient.Stats += message => eventRaised.Set();

            // Act (admin should automatically send StatsMessages once connected)

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }
    }
}