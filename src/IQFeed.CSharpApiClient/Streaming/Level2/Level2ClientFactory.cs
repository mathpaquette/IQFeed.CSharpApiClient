using System;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public static class Level2ClientFactory
    {
        public static Level2Client CreateNew(string host, int port, TimeSpan snapshotTimeout)
        {
            var socketClient = new SocketClient(host, port);
            var level2RequestFormatter = new Level2RequestFormatter();
            var level2MessageHandler = new Level2MessageHandler();

            return new Level2Client(
                socketClient,
                level2RequestFormatter,
                level2MessageHandler,
                new Level2Snapshot(socketClient, level2RequestFormatter, level2MessageHandler, snapshotTimeout)
            );
        }

        public static Level2Client CreateNew()
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level2Port, Level2Default.SnapshotTimeout);
        }

        public static Level2Client CreateNew(string host, int port)
        {
            return CreateNew(host, port, Level2Default.SnapshotTimeout);
        }
    }
}