using System;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level2.Handlers;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public static class Level2ClientFactory
    {
        public static Level2Client<T> CreateNew<T>(
            string host,
            int port,
            TimeSpan snapshotTimeout,
            ILevel2MessageHandler<T> level2MessageHandler)
        {
            var socketClient = new SocketClient(host, port);
            var level2RequestFormatter = new Level2RequestFormatter();

            return new Level2Client<T>(
                socketClient,
                level2RequestFormatter,
                level2MessageHandler,
                new Level2Snapshot<T>(socketClient, level2RequestFormatter, level2MessageHandler, snapshotTimeout)
            );
        }

        public static Level2Client<double> CreateNew()
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.Level2Port, Level2Default.SnapshotTimeout, new Level2MessageDoubleHandler());
        }

        public static Level2Client<double> CreateNew(string host, int port)
        {
            return CreateNew(host, port, Level2Default.SnapshotTimeout, new Level2MessageDoubleHandler());
        }

        public static Level2Client<double> CreateNew(string host, int port, TimeSpan snapshotTimeout)
        {
            return CreateNew(host, port, snapshotTimeout, new Level2MessageDoubleHandler());
        }
    }
}