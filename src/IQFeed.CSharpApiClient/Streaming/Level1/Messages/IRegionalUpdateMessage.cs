using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public interface IRegionalUpdateMessage<T>
    {
        string Symbol { get; }
        string Exchange { get; }
        T RegionalBid { get; }
        int RegionalBidSize { get; }
        DateTime RegionalBidTime { get; }
        T RegionalAsk { get; }
        int RegionalAskSize { get; }
        DateTime RegionalAskTime { get; }
        int FractionDisplayCode { get; }
        int DecimalPrecision { get; }
        int MarketCenter { get; }
    }
}