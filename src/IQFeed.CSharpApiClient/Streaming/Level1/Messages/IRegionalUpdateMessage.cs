using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public interface IRegionalUpdateMessage
    {
        string Symbol { get; }
        string Exchange { get; }
        double RegionalBid { get; }
        int RegionalBidSize { get; }
        DateTime RegionalBidTime { get; }
        double RegionalAsk { get; }
        int RegionalAskSize { get; }
        DateTime RegionalAskTime { get; }
        int FractionDisplayCode { get; }
        int DecimalPrecision { get; }
        int MarketCenter { get; }
    }
}