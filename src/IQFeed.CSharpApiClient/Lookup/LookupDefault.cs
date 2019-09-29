﻿namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupDefault
    {
        /// <summary>
        /// Default timeout before canceling tasks (5 minutes)
        /// This can be changed using LookupClientFactory parameters 
        /// </summary>
        public const int TimeoutMs = 60 * 1000 * 5;

        /// <summary>
        /// Default buffer size for Lookup SocketClient
        /// Note: SocketClients connected to the lookup port, we need to assign them larger buffer
        /// than usual because since we're waiting for the \r\n pattern to notify upper layer that we
        /// completed a new message, some particular responses such on ReqChainIndexEquityOptionAsync
        /// IQFeed will return on the same line all matching symbols causing overflow
        /// </summary>
        public const int BufferSize = 32 * 1024;
    }
}