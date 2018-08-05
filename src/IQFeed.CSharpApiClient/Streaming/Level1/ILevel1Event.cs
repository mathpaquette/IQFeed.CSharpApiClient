using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Event
    {
        event Action<FundamentalMessage> Fundamental;
        event Action<UpdateSummaryMessage> Summary;
        event Action<SystemMessage> System;
        event Action<SymbolNotFoundMessage> SymbolNotFound;
        event Action<ErrorMessage> Error;
        event Action<TimestampMessage> Timestamp;
        event Action<UpdateSummaryMessage> Update;
        event Action<RegionalUpdateMessage> Regional;
        event Action<NewsMessage> News;
    }
}