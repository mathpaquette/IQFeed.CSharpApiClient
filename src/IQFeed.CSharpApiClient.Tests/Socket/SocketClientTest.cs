using IQFeed.CSharpApiClient.Socket;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Socket
{
    public class SocketClientTest
    {
        [Test]
        public void GetHost_Should_Parse_IP()
        {
            Assert.AreEqual(
                "172.17.0.1",
                SocketClient.GetHost("172.17.0.1").ToString());
        }

        [Test]
        public void GetHost_Should_Parse_Domain()
        {
            Assert.IsNotNull(SocketClient.GetHost("www.iqfeed.net"));
        }
    }
}