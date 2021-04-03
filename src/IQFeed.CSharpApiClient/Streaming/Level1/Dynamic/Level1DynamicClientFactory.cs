using System;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Handlers;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public static class Level1DynamicClientFactory
    {
        public static ILevel1DynamicClient CreateNew(string host, int port, TimeSpan snapshotTimeout, ILevel1DynamicMessageHandler level1DynamicMessageHandler, params DynamicFieldset[] fieldNames)
        {
            var socketClient = new SocketClient(host, port);
            var level1RequestFormatter = new Level1RequestFormatter();

            return new Level1DynamicClient(
                socketClient,
                level1RequestFormatter,
                level1DynamicMessageHandler,
                new Level1DynamicSnapshot(socketClient, level1RequestFormatter, level1DynamicMessageHandler, snapshotTimeout),
                fieldNames
            );
        }

        public static ILevel1DynamicClient CreateNew(params DynamicFieldset[] fieldNames)
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level1Port, Level1Default.SnapshotTimeout, new Level1DynamicMessageHandler(), fieldNames);
        }

        public static ILevel1DynamicClient CreateNew(ILevel1DynamicMessageHandler level1DynamicMessageHandler, params DynamicFieldset[] fieldNames)
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level1Port, Level1Default.SnapshotTimeout, level1DynamicMessageHandler, fieldNames);
        }

        public static ILevel1DynamicClient CreateNew(string host, int port, params DynamicFieldset[] fieldNames)
        {
            return CreateNew(host, port, Level1Default.SnapshotTimeout, new Level1DynamicMessageHandler(), fieldNames);
        }
    }
}