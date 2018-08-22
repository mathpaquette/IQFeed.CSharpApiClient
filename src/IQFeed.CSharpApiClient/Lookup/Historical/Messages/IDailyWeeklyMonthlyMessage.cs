using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public interface IDailyWeeklyMonthlyMessage : IMessage
    {
        float Close { get; }
        float High { get; }
        float Low { get; }
        float Open { get; }
        int OpenInterest { get; }
        long PeriodVolume { get; }
    }
}