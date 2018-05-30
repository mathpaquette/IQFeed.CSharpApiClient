using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminClientFactory
    {
        public static AdminClient CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.AdminPort)
        {
            return new AdminClient(
                new SocketClient(host, port),
                new AdminRequestFormatter(),
                new AdminMessageHandler()
            );
        }
    }
}