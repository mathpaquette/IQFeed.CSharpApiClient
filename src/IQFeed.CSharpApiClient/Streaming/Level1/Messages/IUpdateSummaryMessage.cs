using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public interface IUpdateSummaryMessage
    {
        string Symbol { get; }
        double MostRecentTrade { get; }
        int MostRecentTradeSize { get; }
        TimeSpan MostRecentTradeTime { get; }
        int MostRecentTradeMarketCenter { get; }
        int TotalVolume { get; }
        double Bid { get; }
        int BidSize { get; }
        double Ask { get; }
        int AskSize { get; }
        double Open { get; }
        double High { get; }
        double Low { get; }
        double Close { get; }
        string MessageContents { get; }
        string MostRecentTradeConditions { get; }

        [Obsolete("Please use ILevel1DynamicClient instead. This field will be removed soon!")]
        Level1DynamicFields DynamicFields { get; }
    }
}