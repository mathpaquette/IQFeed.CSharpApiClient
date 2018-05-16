using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public static class Level1ClientFactory
    {
        public  static Level1Client CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.Level1Port)
        {
            return new Level1Client(
                new SocketClient(host, port), 
                new Level1RequestFormatter(), 
                new Level1MessageHandler()
            );
        }
    }
}