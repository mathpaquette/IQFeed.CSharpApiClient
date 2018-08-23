using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IIntervalMessage : IHistoricalMessage
    {
        float Close { get; }
        float High { get; }
        float Low { get; }
        int NumberOfTrades { get; }
        float Open { get; }
        int PeriodVolume { get; }
        long TotalVolume { get; }
    }
}