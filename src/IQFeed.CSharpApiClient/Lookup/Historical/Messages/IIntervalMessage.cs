using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IIntervalMessage<T> : IHistoricalMessage
    {
        T Close { get; }
        T High { get; }
        T Low { get; }
        int NumberOfTrades { get; }
        T Open { get; }
        int PeriodVolume { get; }
        long TotalVolume { get; }
    }
}