using System.Threading;
using IQFeed.CSharpApiClient.Streaming.Admin;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;
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

        [OneTimeTearDown]
        public void Cleanup()
        {
            // make sure that the IQFeed client is connected to remote servers
            _adminClient.ReqServerConnect();
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Receive_ProtocolMessage_When_Set()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _adminClient.Protocol += message => eventRaised.Set();

            // Act (admin should automatically send StatsMessages once connected)
            _adminClient.SetProtocol(IQFeedDefault.ProtocolVersion);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Receive_ClientName_In_ClientStatsMessage_When_Set()
        {
            // Arrange
            var myClientName = "MyClientName";
            var eventRaised = new ManualResetEvent(false);
            _adminClient.ClientStats += message =>
            {
                if (message.ClientName == myClientName)
                    eventRaised.Set();
            };

            // Act
            _adminClient.SetClientStats();
            _adminClient.SetClientName(myClientName);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [TestCase(ClientAppMessageType.Register)]
        [TestCase(ClientAppMessageType.Remove)]
        public void Should_Receive_ClientAppMessage_When_ClientApp(ClientAppMessageType clientAppMessageType)
        {
            // Arrange
            var productId = "MyProductId";
            var productVersion = "1.0.0.0";

            var eventRaised = new ManualResetEvent(false);
            _adminClient.ClientApp += message =>
            {
                if (message.Type == clientAppMessageType)
                    eventRaised.Set();
            };

            // Act
            if (clientAppMessageType == ClientAppMessageType.Register)
                _adminClient.RegisterClientApp(productId, productVersion);

            if (clientAppMessageType == ClientAppMessageType.Remove)
                _adminClient.RemoveClientApp(productId, productVersion);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Receive_LoginIdMessage_When_Set()
        {
            // Arrange
            var userLoginId = "MyLoginId";
            var eventRaised = new ManualResetEvent(false);
            _adminClient.LoginId += message =>
            {
                if (message.UserLoginId == userLoginId)
                    eventRaised.Set();
            };

            // Act
            _adminClient.SetLoginId(userLoginId);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test, Timeout(TimeoutMs)]
        public void Should_Receive_PasswordMessage_When_Set()
        {
            // Arrange
            var userPassword = "MyUserPassword";
            var eventRaised = new ManualResetEvent(false);
            _adminClient.Password += message =>
            {
                if (!string.IsNullOrEmpty(message.UserPassword))
                    eventRaised.Set();
            };

            // Act
            _adminClient.SetPassword(userPassword);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [TestCase(LoginInfoMessageType.Saved)]
        [TestCase(LoginInfoMessageType.NotSaved)]
        public void Should_Receive_LoginInfoMessage_When_SetLoginInfo(LoginInfoMessageType loginInfoMessageType)
        {
            // Arrange
           var eventRaised = new ManualResetEvent(false);
            _adminClient.LoginInfo += message =>
            {
                if (message.Type == loginInfoMessageType)
                    eventRaised.Set();
            };

            // Act
            if (loginInfoMessageType == LoginInfoMessageType.Saved)
                _adminClient.SetSaveLoginInfo();

            if (loginInfoMessageType == LoginInfoMessageType.NotSaved)
                _adminClient.SetSaveLoginInfo(false);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [TestCase(AutoConnectMessageType.On)]
        [TestCase(AutoConnectMessageType.Off)]
        public void Should_Receive_AutoConnectMessage_When_SetAutoConnect(AutoConnectMessageType autoConnectMessageType)
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _adminClient.AutoConnect += message =>
            {
                if (message.Type == autoConnectMessageType)
                    eventRaised.Set();
            };

            // Act
            if (autoConnectMessageType == AutoConnectMessageType.On)
                _adminClient.SetAutoconnect();

            if (autoConnectMessageType == AutoConnectMessageType.Off)
                _adminClient.SetAutoconnect(false);

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test]
        public void Should_Receive_StatsMessage_When_ReqServerConnect()
        {
            // Arrange
            var eventRaised = new ManualResetEvent(false);
            _adminClient.Stats += message =>
            {
                if (message.Status == StatsStatusType.Connected)
                    eventRaised.Set();
            };

            // Act
            _adminClient.ReqServerConnect();

            // Assert
            Assert.IsTrue(eventRaised.WaitOne());
        }

        [Test]
        public void Should_Receive_StatsMessage_When_ReqServerDisconnect()
        {
            // Arrange
            var connectedEventRaised = new ManualResetEvent(false);
            var disconnectedEventRaised = new ManualResetEvent(false);

            _adminClient.Stats += message =>
            {
                if (message.Status == StatsStatusType.Connected)
                    connectedEventRaised.Set();

                if (message.Status == StatsStatusType.NotConnected)
                    disconnectedEventRaised.Set();
            };

            // Act
            _adminClient.ReqServerConnect();
            connectedEventRaised.WaitOne();

            _adminClient.ReqServerDisconnect();

            // Assert
            Assert.IsTrue(disconnectedEventRaised.WaitOne());

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