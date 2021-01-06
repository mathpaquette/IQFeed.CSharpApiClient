using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class DailyWeeklyMonthlyMessage : IDailyWeeklyMonthlyMessage
    {
        public const string DailyWeeklyMonthlyDateTimeFormat = "yyyy-MM-dd";

        public DailyWeeklyMonthlyMessage(DateTime timestamp, double high, double low, double open, double close, long periodVolume, int openInterest, string requestId = null)
        {
            Timestamp = timestamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            PeriodVolume = periodVolume;
            OpenInterest = openInterest;
            RequestId = requestId;
        }

        public DateTime Timestamp { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Open { get; private set; }
        public double Close { get; private set; }
        public long PeriodVolume { get; private set; }
        public int OpenInterest { get; private set; }
        public string RequestId { get; private set; }

        public static DailyWeeklyMonthlyMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new DailyWeeklyMonthlyMessage(
                timestamp: DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                high: double.Parse(values[1], CultureInfo.InvariantCulture),
                low: double.Parse(values[2], CultureInfo.InvariantCulture),
                open: double.Parse(values[3], CultureInfo.InvariantCulture),
                close: double.Parse(values[4], CultureInfo.InvariantCulture),
                periodVolume: long.Parse(values[5], CultureInfo.InvariantCulture),
                openInterest: int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public static DailyWeeklyMonthlyMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new DailyWeeklyMonthlyMessage(
                timestamp: DateTime.ParseExact(values[1], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                high: double.Parse(values[2], CultureInfo.InvariantCulture),
                low: double.Parse(values[3], CultureInfo.InvariantCulture),
                open: double.Parse(values[4], CultureInfo.InvariantCulture),
                close: double.Parse(values[5], CultureInfo.InvariantCulture),
                periodVolume: long.Parse(values[6], CultureInfo.InvariantCulture),
                openInterest: int.Parse(values[7], CultureInfo.InvariantCulture),
                requestId: requestId);
        }

        public static IEnumerable<DailyWeeklyMonthlyMessage> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public string ToCsv()
        {
            return RequestId == null ? 
                FormattableString.Invariant($"{Timestamp.ToString(DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture)},{High},{Low},{Open},{Close},{PeriodVolume},{OpenInterest}") : 
                FormattableString.Invariant($"{RequestId},{Timestamp.ToString(DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture)},{High},{Low},{Open},{Close},{PeriodVolume},{OpenInterest}");
        }

        public override bool Equals(object obj)
        {
            return obj is DailyWeeklyMonthlyMessage message &&
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
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Open)}: {Open}, {nameof(Close)}: {Close}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(OpenInterest)}: {OpenInterest}, {nameof(RequestId)}: {RequestId}";
        }
    }
}