using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class IntervalMessage
    {
        public IntervalMessage(DateTime timestamp, float high, float low, float close, float open, int totalVolume, int periodVolume)
        {
            Timestamp = timestamp;
            High = high;
            Low = low;
            Close = close;
            Open = open;
            TotalVolume = totalVolume;
            PeriodVolume = periodVolume;
        }

        public DateTime Timestamp { get; }
        public float High { get; }
        public float Low { get; }
        public float Close { get; }
        public float Open { get; }
        public int TotalVolume { get; }
        public int PeriodVolume { get; }
    }
}