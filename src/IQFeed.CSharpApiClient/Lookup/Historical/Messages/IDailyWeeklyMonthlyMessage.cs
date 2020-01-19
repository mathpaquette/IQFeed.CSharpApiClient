using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IDailyWeeklyMonthlyMessage<T> : IHistoricalMessage
    {
        T Close { get; }
        T High { get; }
        T Low { get; }
        T Open { get; }
        int OpenInterest { get; }
        long PeriodVolume { get; }
    }
}