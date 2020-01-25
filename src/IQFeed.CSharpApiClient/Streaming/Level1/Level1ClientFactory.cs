using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public static class Level1ClientFactory
    {
        public static Level1Client<T> CreateNew<T>(
            string host,
            int port,
            int snapshotTimeoutMs,
            ILevel1MessageHandler<T> level1MessageHandler)
        {
            var socketClient = new SocketClient(host, port);
            var level1RequestFormatter = new Level1RequestFormatter();

            return new Level1Client<T>(
                socketClient,
                level1RequestFormatter,
                level1MessageHandler,
                new Level1Snapshot<T>(socketClient, level1RequestFormatter, level1MessageHandler, snapshotTimeoutMs)
            );
        }

        public static Level1Client<decimal> CreateNew(string host, int port, int snapshotTimeoutMs = Level1Default.SnapshotTimeoutMs)
        {
            return CreateNew(host, port, snapshotTimeoutMs, new Level1MessageDecimalHandler());
        }

        public static Level1Client<decimal> CreateNew()
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level1Port, Level1Default.SnapshotTimeoutMs, new Level1MessageDecimalHandler());
        }
    }
}