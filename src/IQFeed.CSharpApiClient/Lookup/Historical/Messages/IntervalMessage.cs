using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class IntervalMessage : IIntervalMessage
    {
        public const string IntervalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public IntervalMessage(DateTime timestamp, float high, float low, float open, float close, long totalVolume, int periodVolume, int numberOfTrades, string requestId = null)
        {
            RequestId = requestId;
            Timestamp = timestamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            TotalVolume = totalVolume;
            PeriodVolume = periodVolume;
            NumberOfTrades = numberOfTrades;
        }
        
        public string RequestId { get; }
        public DateTime Timestamp { get; }
        public float High { get; }
        public float Low { get; }
        public float Open { get; }
        public float Close { get; }
        public long TotalVolume { get; }
        public int PeriodVolume { get; }
        public int NumberOfTrades { get; }

        public static IntervalMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new IntervalMessage(
                DateTime.ParseExact(values[0], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture));
        }

        public static IntervalMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new IntervalMessage(
                DateTime.ParseExact(values[1], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                float.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is IntervalMessage message &&
                   RequestId == message.RequestId &&
                   Timestamp == message.Timestamp &&
                   High == message.High &&
                   Low == message.Low &&
                   Open == message.Open &&
                   Close == message.Close &&
                   TotalVolume == message.TotalVolume &&
                   PeriodVolume == message.PeriodVolume &&
                   NumberOfTrades == message.NumberOfTrades;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + High.GetHashCode();
                hash = hash * 29 + Low.GetHashCode();
                hash = hash * 29 + Open.GetHashCode();
                hash = hash * 29 + Close.GetHashCode();
                hash = hash * 29 + TotalVolume.GetHashCode();
                hash = hash * 29 + PeriodVolume.GetHashCode();
                hash = hash * 29 + NumberOfTrades.GetHashCode();
                return hash;
            }
        }
    }
}