using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public static class Level1ClientFactory
    {
        public static Level1Client CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.Level1Port, int snapshotTimeoutMs = Level1Default.SnapshotTimeoutMs)
        {
            var socketClient = new SocketClient(host, port);
            var level1RequestFormatter = new Level1RequestFormatter();
            var level1MessageHandler = new Level1MessageHandler();

            return new Level1Client(
                socketClient,
                level1RequestFormatter,
                level1MessageHandler,
                new Level1Snapshot(socketClient, level1RequestFormatter, level1MessageHandler, snapshotTimeoutMs)
            );
        }
    }
}