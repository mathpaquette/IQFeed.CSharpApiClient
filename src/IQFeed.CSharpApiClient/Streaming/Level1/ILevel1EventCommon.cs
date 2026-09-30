using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1EventCommon
    {
        event Action<FundamentalMessage> Fundamental;
        event Action<SystemMessage> System;
        event Action<SymbolNotFoundMessage> SymbolNotFound;
        event Action<ErrorMessage> Error;
        event Action<TimestampMessage> Timestamp;
        event Action<RegionalUpdateMessage> Regional;
        event Action<NewsMessage> News;

        /// <summary>
        /// Raised for every received line that could not be processed: empty, of an unknown message type, or failing while processed.
        /// The line is skipped and the rest of the received batch is still processed.
        /// </summary>
        event Action<UnhandledMessage> UnhandledMessage;
    }
}