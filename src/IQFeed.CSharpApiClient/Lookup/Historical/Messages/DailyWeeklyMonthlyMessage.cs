using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public abstract class DailyWeeklyMonthlyMessage
    {
        public const string DailyWeeklyMonthlyDateTimeFormat = "yyyy-MM-dd";

        public static DailyWeeklyMonthlyMessage<decimal> Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new DailyWeeklyMonthlyMessage<decimal>(
                DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[1], CultureInfo.InvariantCulture),
                decimal.Parse(values[2], CultureInfo.InvariantCulture),
                decimal.Parse(values[3], CultureInfo.InvariantCulture),
                decimal.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public static DailyWeeklyMonthlyMessage<decimal> ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new DailyWeeklyMonthlyMessage<decimal>(
                DateTime.ParseExact(values[1], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[2], CultureInfo.InvariantCulture),
                decimal.Parse(values[3], CultureInfo.InvariantCulture),
                decimal.Parse(values[4], CultureInfo.InvariantCulture),
                decimal.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                requestId);
        }

        public static DailyWeeklyMonthlyMessage<double> ParseDouble(string message)
        {
            var values = message.SplitFeedMessage();

            return new DailyWeeklyMonthlyMessage<double>(
                DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[1], CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public static DailyWeeklyMonthlyMessage<double> ParseDoubleWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new DailyWeeklyMonthlyMessage<double>(
                DateTime.ParseExact(values[1], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                double.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                requestId);
        }

        public static DailyWeeklyMonthlyMessage<float> ParseFloat(string message)
        {
            var values = message.SplitFeedMessage();

            return new DailyWeeklyMonthlyMessage<float>(
                DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public static DailyWeeklyMonthlyMessage<float> ParseFloatWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new DailyWeeklyMonthlyMessage<float>(
                DateTime.ParseExact(values[1], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                float.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                requestId);
        }

        public static IEnumerable<DailyWeeklyMonthlyMessage<decimal>> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public static IEnumerable<DailyWeeklyMonthlyMessage<double>> ParseDoubleFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseDouble, path)
                : LookupMessageFileParser.ParseFromFile(ParseDoubleWithRequestId, path);
        }

        public static IEnumerable<DailyWeeklyMonthlyMessage<float>> ParseFloatFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseFloat, path)
                : LookupMessageFileParser.ParseFromFile(ParseFloatWithRequestId, path);
        }
    }

    public class DailyWeeklyMonthlyMessage<T> : IDailyWeeklyMonthlyMessage<T>
    {
        public DailyWeeklyMonthlyMessage(DateTime timestamp, T high, T low, T open, T close, long periodVolume, int openInterest, string requestId = null)
        {
            RequestId = requestId;
            Timestamp = timestamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            PeriodVolume = periodVolume;
            OpenInterest = openInterest;
        }

        public string RequestId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public T High { get; private set; }
        public T Low { get; private set; }
        public T Open { get; private set; }
        public T Close { get; private set; }
        public long PeriodVolume { get; private set; }
        public int OpenInterest { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is DailyWeeklyMonthlyMessage<decimal> message &&
                   RequestId == message.RequestId &&
                   Timestamp == message.Timestamp &&
                   Equals(High, message.High) &&
                   Equals(Low, message.Low) &&
                   Equals(Open, message.Open) &&
                   Equals(Close, message.Close) &&
                   PeriodVolume == message.PeriodVolume &&
                   OpenInterest == message.OpenInterest;
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
                hash = hash * 29 + PeriodVolume.GetHashCode();
                hash = hash * 29 + OpenInterest.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(RequestId)}: {RequestId}, {nameof(Timestamp)}: {Timestamp}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Open)}: {Open}, {nameof(Close)}: {Close}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(OpenInterest)}: {OpenInterest}";
        }
    }
}