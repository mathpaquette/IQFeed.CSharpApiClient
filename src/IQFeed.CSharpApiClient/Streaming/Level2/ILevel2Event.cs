using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2Event<T>
    {
        event Action<UpdateSummaryMessage<T>> Summary;
        event Action<UpdateSummaryMessage<T>> Update;
        event Action<SymbolNotFoundMessage> SymbolNotFound;
        event Action<MarketMakerNameMessage> Query;
        event Action<ErrorMessage> Error;
        event Action<TimestampMessage> Timestamp;
        event Action<SystemMessage> System;
    }
}