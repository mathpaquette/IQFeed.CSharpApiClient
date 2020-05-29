using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IDailyWeeklyMonthlyMessage : IHistoricalMessage
    {
        double Close { get; }
        double High { get; }
        double Low { get; }
        double Open { get; }
        int OpenInterest { get; }
        long PeriodVolume { get; }
    }
}