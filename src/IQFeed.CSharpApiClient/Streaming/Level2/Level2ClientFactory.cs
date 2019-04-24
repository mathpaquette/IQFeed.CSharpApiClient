using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public static class Level2ClientFactory
    {
        public static Level2Client CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.Level2Port, int snapshotTimeoutMs = Level2Default.SnapshotTimeoutMs)
        {
            var socketClient = new SocketClient(host, port);
            var level2RequestFormatter = new Level2RequestFormatter();
            var level2MessageHandler = new Level2MessageHandler();

            return new Level2Client(
                socketClient,
                level2RequestFormatter,
                level2MessageHandler,
                new Level2Snapshot(socketClient, level2RequestFormatter, level2MessageHandler, snapshotTimeoutMs)
            );
        }
    }
}