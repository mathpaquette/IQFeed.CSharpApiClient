using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessage
    {
        public DailyWeeklyMonthlyMessage(DateTime timestamp, float high, float low, float close, float open, int periodVolume, int openInterest)
        {
            Timestamp = timestamp;
            High = high;
            Low = low;
            Close = close;
            Open = open;
            PeriodVolume = periodVolume;
            OpenInterest = openInterest;
        }

        public DateTime Timestamp { get; }
        public float High { get; }
        public float Low { get; }
        public float Close { get; }
        public float Open { get; }
        public int PeriodVolume { get; }
        public int OpenInterest { get; }
    }
}