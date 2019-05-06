using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Admin;
using IQFeed.CSharpApiClient.Streaming.Derivative;
using IQFeed.CSharpApiClient.Streaming.Level1;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Integration.Client
{
    public class ClientTests
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public static void AdminClient_can_connect_and_terminate_without_error_forceIpv4(bool forceIpv4)
        {
            // Arrange
            SocketClient.ForceIpv4 = forceIpv4;

            // Act
            IQFeedLauncher.Start();

            var client = AdminClientFactory.CreateNew();
            client.Connect();
            client.Disconnect();

            IQFeedLauncher.Terminate();

            // Assert
            Assert.Pass($"IQFeedLauncher and the admin client were able to connect and disconnect/terminate without error with the 'SocketClient.ForceIpv4' value set to '{forceIpv4}'.");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public static void DerivativeClient_can_connect_and_terminate_without_error_forceIpv4(bool forceIpv4)
        {
            // Arrange
            SocketClient.ForceIpv4 = forceIpv4;

            // Act
            IQFeedLauncher.Start();

            var client = DerivativeClientFactory.CreateNew();
            client.Connect();
            client.Disconnect();

            IQFeedLauncher.Terminate();

            // Assert
            Assert.Pass($"IQFeedLauncher and the derivative client were able to connect and disconnect/terminate without error with the 'SocketClient.ForceIpv4' value set to '{forceIpv4}'.");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public static void Level1Client_can_connect_and_terminate_without_error_forceIpv4(bool forceIpv4)
        {
            // Arrange
            SocketClient.ForceIpv4 = forceIpv4;

            // Act
            IQFeedLauncher.Start();

            var client = Level1ClientFactory.CreateNew();
            client.Connect();
            client.Disconnect();

            IQFeedLauncher.Terminate();

            // Assert
            Assert.Pass($"IQFeedLauncher and the level1 client were able to connect and disconnect/terminate without error with the 'SocketClient.ForceIpv4' value set to '{forceIpv4}'.");
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public static void LookupClient_can_connect_and_terminate_without_error_forceIpv4(bool forceIpv4)
        {
            // Arrange
            SocketClient.ForceIpv4 = forceIpv4;

            // Act
            IQFeedLauncher.Start();

            var client = LookupClientFactory.CreateNew();
            client.Connect();
            client.Disconnect();

            IQFeedLauncher.Terminate();

            // Assert
            Assert.Pass($"IQFeedLauncher and the lookup client were able to connect and disconnect/terminate without error with the 'SocketClient.ForceIpv4' value set to '{forceIpv4}'.");
        }
    }
}