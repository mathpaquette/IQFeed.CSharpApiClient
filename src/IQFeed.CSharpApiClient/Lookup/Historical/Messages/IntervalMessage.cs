using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class IntervalMessage
    {
        public const string IntervalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

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

        public static IntervalMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new IntervalMessage(
                DateTime.ParseExact(values[0], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                int.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
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