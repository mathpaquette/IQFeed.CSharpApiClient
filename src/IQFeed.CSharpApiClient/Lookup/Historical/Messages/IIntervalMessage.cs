using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IIntervalMessage : IHistoricalMessage
    {
        double Close { get; }
        double High { get; }
        double Low { get; }
        int NumberOfTrades { get; }
        double Open { get; }
        long PeriodVolume { get; }
        long TotalVolume { get; }
    }
}