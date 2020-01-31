using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public abstract class IntervalMessage
    {
        public const string IntervalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public static IntervalMessage<decimal> ParseDecimal(string message)
        {
            var values = message.SplitFeedMessage();

            return new IntervalMessage<decimal>(
                DateTime.ParseExact(values[0], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[1], CultureInfo.InvariantCulture),
                decimal.Parse(values[2], CultureInfo.InvariantCulture),
                decimal.Parse(values[3], CultureInfo.InvariantCulture),
                decimal.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture));
        }

        public static IntervalMessage<decimal> ParseDecimalWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new IntervalMessage<decimal>(
                DateTime.ParseExact(values[1], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                decimal.Parse(values[2], CultureInfo.InvariantCulture),
                decimal.Parse(values[3], CultureInfo.InvariantCulture),
                decimal.Parse(values[4], CultureInfo.InvariantCulture),
                decimal.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                requestId);
        }

        public static IntervalMessage<double> Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new IntervalMessage<double>(
                DateTime.ParseExact(values[0], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[1], CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture));
        }

        public static IntervalMessage<double> ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new IntervalMessage<double>(
                DateTime.ParseExact(values[1], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                double.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                int.Parse(values[8], CultureInfo.InvariantCulture),
                requestId);
        }

        public static IntervalMessage<float> ParseFloat(string message)
        {
            var values = message.SplitFeedMessage();

            return new IntervalMessage<float>(
                DateTime.ParseExact(values[0], IntervalDateTimeFormat, CultureInfo.InvariantCulture),
                float.Parse(values[1], CultureInfo.InvariantCulture),
                float.Parse(values[2], CultureInfo.InvariantCulture),
                float.Parse(values[3], CultureInfo.InvariantCulture),
                float.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture));
        }

        public static IntervalMessage<float> ParseFloatWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new IntervalMessage<float>(
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

        public static IEnumerable<IntervalMessage<decimal>> ParseDecimalFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseDecimal, path)
                : LookupMessageFileParser.ParseFromFile(ParseDecimalWithRequestId, path);
        }

        public static IEnumerable<IntervalMessage<double>> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public static IEnumerable<IntervalMessage<float>> ParseFloatFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(ParseFloat, path)
                : LookupMessageFileParser.ParseFromFile(ParseFloatWithRequestId, path);
        }
    }

    public class IntervalMessage<T> : IntervalMessage, IIntervalMessage<T>
    {
        public IntervalMessage(DateTime timestamp, T high, T low, T open, T close, long totalVolume, int periodVolume, int numberOfTrades, string requestId = null)
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

        public string RequestId { get; private set; }
        public DateTime Timestamp { get; private set; }
        public T High { get; private set; }
        public T Low { get; private set; }
        public T Open { get; private set; }
        public T Close { get; private set; }
        public long TotalVolume { get; private set; }
        public int PeriodVolume { get; private set; }
        public int NumberOfTrades { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is IntervalMessage<T> message &&
                   RequestId == message.RequestId &&
                   Timestamp == message.Timestamp &&
                   Equals(High, message.High) &&
                   Equals(Low, message.Low) &&
                   Equals(Open, message.Open) &&
                   Equals(Close, message.Close) &&
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

        public override string ToString()
        {
            return $"{nameof(RequestId)}: {RequestId}, {nameof(Timestamp)}: {Timestamp}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Open)}: {Open}, {nameof(Close)}: {Close}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(NumberOfTrades)}: {NumberOfTrades}";
        }
    }
}