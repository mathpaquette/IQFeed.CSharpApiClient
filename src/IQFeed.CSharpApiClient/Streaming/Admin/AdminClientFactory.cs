using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminClientFactory
    {
        public static AdminClient CreateNew(string host, int port)
        {
            return new AdminClient(
                new SocketClient(host, port),
                new AdminRequestFormatter(),
                new AdminMessageHandler()
            );
        }

        public static AdminClient CreateNew()
        {
            return new AdminClient(
                new SocketClient(IQFeedDefault.Hostname, IQFeedDefault.AdminPort),
                new AdminRequestFormatter(),
                new AdminMessageHandler()
            );
        }
    }
}