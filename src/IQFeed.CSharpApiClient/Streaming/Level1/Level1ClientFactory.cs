using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public static class Level1ClientFactory
    {
        public static Level1Client CreateNew(string host, int port, TimeSpan snapshotTimeout, DynamicFieldset[] dynamicFields = null)
        {
            var socketClient = new SocketClient(host, port);
            var level1RequestFormatter = new Level1RequestFormatter();
            var level1MessageHandler = new Level1MessageHandler(dynamicFields);

            return new Level1Client(
                socketClient,
                level1RequestFormatter,
                level1MessageHandler,
                new Level1Snapshot(socketClient, level1RequestFormatter, level1MessageHandler, snapshotTimeout)
                ,dynamicFields
            );
        }

        public static Level1Client CreateNew( DynamicFieldset[] dynamicFields = null)
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level1Port, Level1Default.SnapshotTimeout, dynamicFields);
        }

        public static Level1Client CreateNew(string host, int port, DynamicFieldset[] dynamicFields = null)
        {
            return CreateNew(host, port, Level1Default.SnapshotTimeout, dynamicFields);
        }
    }
}