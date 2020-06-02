using System;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public static class Level1ClientFactory
    {
        public static Level1Client CreateNew(string host, int port, TimeSpan snapshotTimeout)
        {
            var socketClient = new SocketClient(host, port);
            var level1RequestFormatter = new Level1RequestFormatter();
            var level1MessageHandler = new Level1MessageHandler();

            return new Level1Client(
                socketClient,
                level1RequestFormatter,
                level1MessageHandler,
                new Level1Snapshot(socketClient, level1RequestFormatter, level1MessageHandler, snapshotTimeout)
            );
        }

        public static Level1Client CreateNew()
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level1Port, Level1Default.SnapshotTimeout);
        }

        public static Level1Client CreateNew(string host, int port)
        {
            return CreateNew(host, port, Level1Default.SnapshotTimeout);
        }
    }
}