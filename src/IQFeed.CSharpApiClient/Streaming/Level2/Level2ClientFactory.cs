using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public static class Level2ClientFactory
    {
        public static Level2Client<decimal> CreateNew()
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.Level2Port,
                Level2Default.SnapshotTimeoutMs,
                new Level2MessageHandler());
        }

        public static Level2Client<T> CreateNew<T>(
            string host,
            int port,
            int snapshotTimeoutMs,
            ILevel2MessageHandler<T> level2MessageHandler)
        {
            var socketClient = new SocketClient(host, port);
            var level2RequestFormatter = new Level2RequestFormatter();

            return new Level2Client<T>(
                socketClient,
                level2RequestFormatter,
                level2MessageHandler,
                new Level2Snapshot<T>(socketClient, level2RequestFormatter, level2MessageHandler, snapshotTimeoutMs)
            );
        }
    }
}