using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessage
    {
        public const string DailyWeeklyMonthlyDateTimeFormat = "yyyy-MM-dd";

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

        public static DailyWeeklyMonthlyMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            return new DailyWeeklyMonthlyMessage(
                DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                int.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public override bool Equals(object obj)
        {
            return obj is DailyWeeklyMonthlyMessage message &&
                   Timestamp == message.Timestamp &&
                   High == message.High &&
                   Low == message.Low &&
                   Close == message.Close &&
                   Open == message.Open &&
                   PeriodVolume == message.PeriodVolume &&
                   OpenInterest == message.OpenInterest;
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
                hash = hash * 29 + PeriodVolume.GetHashCode();
                hash = hash * 29 + OpenInterest.GetHashCode();
                return hash;
            }
        }
    }
}