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
            if (TryParseInner(message, out var dailyWeeklyMonthlyMessage, false))
            {
                return dailyWeeklyMonthlyMessage;
            }

            throw new Exception($"Unable to parse message into DailyWeeklyMonthlyMessage\nmessage={message}");
        }

        public static DailyWeeklyMonthlyMessage ParseWithRequestId(string message)
        {
            if (TryParseInner(message, out var dailyWeeklyMonthlyMessage, true))
            {
                return dailyWeeklyMonthlyMessage;
            }

            throw new Exception($"Unable to parse message into DailyWeeklyMonthlyMessage\nmessage={message}");
        }

        public static bool TryParse(string message, out DailyWeeklyMonthlyMessage dailyWeeklyMonthlyMessage)
        {
            return TryParseInner(message, out dailyWeeklyMonthlyMessage, false);
        }

        public static bool TryParseWithRequestId(string message, out DailyWeeklyMonthlyMessage dailyWeeklyMonthlyMessage)
        {
            return TryParseInner(message, out dailyWeeklyMonthlyMessage, true);
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

        private static bool TryParseInner(string message, out DailyWeeklyMonthlyMessage dailyWeeklyMonthlyMessage, bool hasRequestId)
        {
            var messageDataIdIndex = hasRequestId ? 1 : 0;
            var indexBase = messageDataIdIndex;
            var values = message.SplitFeedMessage();
            if (values[messageDataIdIndex] == HistoricalMessageHandler.HistoricalDataId)
            {
                // protocol 6.2
                return TryParseInnerParser(values, hasRequestId, ++indexBase, out dailyWeeklyMonthlyMessage);
            }

            // protocol <6.2
            return TryParseInnerParser(values, hasRequestId, indexBase, out dailyWeeklyMonthlyMessage);
        }

        private static bool TryParseInnerParser(string[] values, bool hasRequestId, int indexBase, out DailyWeeklyMonthlyMessage dailyWeeklyMonthlyMessage)
        {
            dailyWeeklyMonthlyMessage = default;
            DateTime timestamp = default;
            double high = default;
            double low = default;
            double open = default;
            double close = default;
            long periodVolume = default;
            int openInterest = default;

            var parsed =
                         DateTime.TryParseExact(values[indexBase + 0], DailyWeeklyMonthlyDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp) &&
                         double.TryParse(values[indexBase + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out high) &&
                         double.TryParse(values[indexBase + 2], NumberStyles.Any, CultureInfo.InvariantCulture, out low) &&
                         double.TryParse(values[indexBase + 3], NumberStyles.Any, CultureInfo.InvariantCulture, out open) &&
                         double.TryParse(values[indexBase + 4], NumberStyles.Any, CultureInfo.InvariantCulture, out close) &&
                         long.TryParse(values[indexBase + 5], NumberStyles.Any, CultureInfo.InvariantCulture, out periodVolume) &&
                         int.TryParse(values[indexBase + 6], NumberStyles.Any, CultureInfo.InvariantCulture, out openInterest);

            if (parsed)
            {
                dailyWeeklyMonthlyMessage =
                    new DailyWeeklyMonthlyMessage(
                        timestamp, 
                        high, 
                        low, 
                        open, 
                        close, 
                        periodVolume, 
                        openInterest,
                        hasRequestId ? values[0] : null);
            }

            return parsed;

        }
    }
}