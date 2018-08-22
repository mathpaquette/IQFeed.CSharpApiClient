using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IIntervalMessage
    {
        float Close { get; }
        float High { get; }
        float Low { get; }
        int NumberOfTrades { get; }
        float Open { get; }
        int PeriodVolume { get; }
        DateTime Timestamp { get; }
        long TotalVolume { get; }
    }
}