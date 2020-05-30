using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public interface IUpdateSummaryMessage<T>
    {
        string Symbol { get; }
        T MostRecentTrade { get; }
        int MostRecentTradeSize { get; }
        TimeSpan MostRecentTradeTime { get; }
        int MostRecentTradeMarketCenter { get; }
        int TotalVolume { get; }
        T Bid { get; }
        int BidSize { get; }
        T Ask { get; }
        int AskSize { get; }
        T Open { get; }
        T High { get; }
        T Low { get; }
        T Close { get; }
        string MessageContents { get; }
        string MostRecentTradeConditions { get; }
        int OpenInterest { get; }

    }
}