using System;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public interface IUpdateSummaryMessage<T>
    {
        string Symbol { get; }
        string MMID { get; }
        T Bid { get; }
        T Ask { get; }
        int BidSize { get; }
        int AskSize { get; }
        TimeSpan BidTime { get; }
        DateTime Date { get; }
        string ConditionCode { get; }
        TimeSpan AskTime { get; }
        char BidInfoValid { get; }
        char AskInfoValid { get; }
        char EndOfMessageGroup { get; }
    }
}