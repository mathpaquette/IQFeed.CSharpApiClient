using System;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public static class Level2ClientFactory
    {
        public static Level2Client CreateNew(string host, int port, TimeSpan snapshotTimeout, string protocolVersion = IQFeedDefault.ProtocolVersion)
        {
            var socketClient = new SocketClient(host, port);
            var level2RequestFormatter = new Level2RequestFormatter();
            var level2MessageHandler = new Level2MessageHandler();

            return new Level2Client(
                socketClient,
                level2RequestFormatter,
                level2MessageHandler,
                new Level2Snapshot(socketClient, level2RequestFormatter, level2MessageHandler, snapshotTimeout),
                protocolVersion
            );
        }

        public static Level2Client CreateNew(string protocolVersion = IQFeedDefault.ProtocolVersion)
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level2Port, Level2Default.SnapshotTimeout, protocolVersion);
        }

        public static Level2Client CreateNew(string host, int port, string protocolVersion = IQFeedDefault.ProtocolVersion)
        {
            return CreateNew(host, port, Level2Default.SnapshotTimeout, protocolVersion);
        }
    }
}