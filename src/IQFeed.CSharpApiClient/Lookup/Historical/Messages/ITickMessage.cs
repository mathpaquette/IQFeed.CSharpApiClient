using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface ITickMessage
    {
        float Ask { get; }
        char BasisForLast { get; }
        float Bid { get; }
        float Last { get; }
        int LastSize { get; }
        long TickId { get; }
        DateTime Timestamp { get; }
        int TotalVolume { get; }
        string TradeConditions { get; }
        int TradeMarketCenter { get; }
    }
}