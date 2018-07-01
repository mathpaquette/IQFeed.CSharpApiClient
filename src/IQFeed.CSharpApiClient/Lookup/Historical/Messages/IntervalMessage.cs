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

        public static IntervalMessage CreateIntervalMessage(string[] values)
        {
            return new IntervalMessage(
                DateTime.Parse(values[0]),
                float.Parse(values[1]),
                float.Parse(values[2]),
                float.Parse(values[3]),
                float.Parse(values[4]),
                int.Parse(values[5]),
                int.Parse(values[6]));
        }

        public override bool Equals(object obj)
        {
            return obj is IntervalMessage message &&
                   Timestamp == message.Timestamp &&
                   High == message.High &&
                   Low == message.Low &&
                   Close == message.Close &&
                   Open == message.Open &&
                   TotalVolume == message.TotalVolume &&
                   PeriodVolume == message.PeriodVolume;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + High.GetHashCode();
                hash = hash * 29 + Low.GetHashCode();
                hash = hash * 29 + Close.GetHashCode();
                hash = hash * 29 + Open.GetHashCode();
                hash = hash * 29 + TotalVolume.GetHashCode();
                hash = hash * 29 + PeriodVolume.GetHashCode();
                return hash;
            }
        }
    }
}