using IQFeed.CSharpApiClient.Streaming.Admin;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Streaming.Admin
{
    public class AdminRequestFormatterTests
    {
        private readonly AdminRequestFormatter _adminRequestFormatter;

        public AdminRequestFormatterTests()
        {
            _adminRequestFormatter = new AdminRequestFormatter();
        }

        [Test]
        public void SetClientName_Should_Return_Formatted_Request()
        {
            // Arrange
            var clientName = "MyClientName";

            // Act
            var formatted = _adminRequestFormatter.SetClientName(clientName);

            // Assert
            Assert.AreEqual(formatted, $"S,SET CLIENT NAME,{clientName}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [Test]
        public void RegisterClientApp_Should_Return_Formatted_Request()
        {
            // Arrange
            var productId = "MyProductId";
            var productVersion = "1.0.0.0";

            // Act
            var formatted = _adminRequestFormatter.RegisterClientApp(productId, productVersion);

            // Assert
            Assert.AreEqual(formatted, $"S,REGISTER CLIENT APP,{productId},{productVersion}{IQFeedDefault.ProtocolTerminatingCharacters}".ToUpper());
        }

        [Test]
        public void RemoveClientApp_Should_Return_Formatted_Request()
        {
            // Arrange
            var productId = "MyProductId";
            var productVersion = "1.0.0.0";

            // Act
            var formatted = _adminRequestFormatter.RemoveClientApp(productId, productVersion);

            // Assert
            Assert.AreEqual(formatted, $"S,REMOVE CLIENT APP,{productId},{productVersion}{IQFeedDefault.ProtocolTerminatingCharacters}".ToUpper());
        }

        [Test]
        public void SetLoginId_Should_Return_Formatted_Request()
        {
            // Arrange
            var userLoginId = "MyUserId";

            // Act
            var formatted = _adminRequestFormatter.SetLoginId(userLoginId);

            // Assert
            Assert.AreEqual(formatted, $"S,SET LOGINID,{userLoginId}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [Test]
        public void SetPassword_Should_Return_Formatted_Request()
        {
            // Arrange
            var userPassword = "MyUserPassword";

            // Act
            var formatted = _adminRequestFormatter.SetPassword(userPassword);

            // Assert
            Assert.AreEqual(formatted, $"S,SET PASSWORD,{userPassword}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetSaveLoginInfo_Should_Return_Formatted_Request(bool on)
        {
            // Arrange
            var value = on ? "On" : "Off";

            // Act
            var formatted = _adminRequestFormatter.SetSaveLoginInfo(on);

            // Assert
            Assert.AreEqual(formatted, $"S,SET SAVE LOGIN INFO,{value}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetAutoconnect_Should_Return_Formatted_Request(bool on)
        {
            // Arrange
            var value = on ? "On" : "Off";

            // Act
            var formatted = _adminRequestFormatter.SetAutoconnect(on);

            // Assert
            Assert.AreEqual(formatted, $"S,SET AUTOCONNECT,{value}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [Test]
        public void ReqServerConnect_Should_Return_Formatted_Request()
        {
            // Act
            var formatted = _adminRequestFormatter.ReqServerConnect();

            // Assert
            Assert.AreEqual(formatted, $"S,CONNECT{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [Test]
        public void ReqServerDisconnect_Should_Return_Formatted_Request()
        {
            // Act
            var formatted = _adminRequestFormatter.ReqServerDisconnect();

            // Assert
            Assert.AreEqual(formatted, $"S,DISCONNECT{IQFeedDefault.ProtocolTerminatingCharacters}");
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetClientStats_Should_Return_Formatted_Request(bool on)
        {
            // Arrange
            var value = on ? "ON" : "OFF";

            // Act
            var formatted = _adminRequestFormatter.SetClientStats(on);

            // Assert
            Assert.AreEqual(formatted, $"S,CLIENTSTATS {value}{IQFeedDefault.ProtocolTerminatingCharacters}");
        }
    }
}