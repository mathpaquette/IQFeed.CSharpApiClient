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
                DateTime.ParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[1], CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                long.Parse(values[5], CultureInfo.InvariantCulture),
                int.Parse(values[6], CultureInfo.InvariantCulture));
        }

        public static bool TryParse(string message, out DailyWeeklyMonthlyMessage dailyWeeklyMonthlyMessage)
        {
            DateTime timestamp = default;
            double high = default;
            double low = default;
            double open = default;
            double close = default;
            long periodVolume = default;
            int openInterest = default;

            var values = message.SplitFeedMessage();
            var parsed = values.Length >= 7 &&
                         DateTime.TryParseExact(values[0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp) &
                         double.TryParse(values[1], NumberStyles.Any, CultureInfo.InvariantCulture, out high) &
                         double.TryParse(values[2], NumberStyles.Any, CultureInfo.InvariantCulture, out low) &
                         double.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out open) &
                         double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out close) &
                         long.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out periodVolume) &
                         int.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out openInterest);

            dailyWeeklyMonthlyMessage = new DailyWeeklyMonthlyMessage(timestamp, high, low, open, close, periodVolume, openInterest);
            return parsed;
        }

        public static DailyWeeklyMonthlyMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new DailyWeeklyMonthlyMessage(
                DateTime.ParseExact(values[1], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture),
                double.Parse(values[2], CultureInfo.InvariantCulture),
                double.Parse(values[3], CultureInfo.InvariantCulture),
                double.Parse(values[4], CultureInfo.InvariantCulture),
                double.Parse(values[5], CultureInfo.InvariantCulture),
                long.Parse(values[6], CultureInfo.InvariantCulture),
                int.Parse(values[7], CultureInfo.InvariantCulture),
                requestId);
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