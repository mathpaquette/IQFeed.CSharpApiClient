using System;
using System.Collections.Generic;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages
{
    public class IntervalMessage : IIntervalMessage
    {
        public const string IntervalDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public IntervalMessage(DateTime timestamp, double high, double low, double open, double close, long totalVolume, long periodVolume, int numberOfTrades, string requestId = null)
        {
            Timestamp = timestamp;
            High = high;
            Low = low;
            Open = open;
            Close = close;
            TotalVolume = totalVolume;
            PeriodVolume = periodVolume;
            NumberOfTrades = numberOfTrades;
            RequestId = requestId;
        }

        public DateTime Timestamp { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Open { get; private set; }
        public double Close { get; private set; }
        public long TotalVolume { get; private set; }
        public long PeriodVolume { get; private set; }
        public int NumberOfTrades { get; private set; }
        public string RequestId { get; private set; }

        public static IntervalMessage Parse(string message)
        {
            if (TryParseInner(message, out var intervalMessage, false))
            {
                return intervalMessage;
            }

            throw new Exception($"Unable to parse message into IntervalMessage\nmessage={message}");
        }

        public static IntervalMessage ParseWithRequestId(string message)
        {
            if (TryParseInner(message, out var intervalMessage, true))
            {
                return intervalMessage;
            }

            throw new Exception($"Unable to parse message into IntervalMessage\nmessage={message}");
        }

        public static bool TryParse(string message, out IntervalMessage intervalMessage)
        {
            return TryParseInner(message, out intervalMessage, false);
        }

        public static bool TryParseWithRequestId(string message, out IntervalMessage intervalMessage)
        {
            return TryParseInner(message, out intervalMessage, true);
        }

        public static IEnumerable<IntervalMessage> ParseFromFile(string path, bool hasRequestId = false)
        {
            return hasRequestId == false
                ? LookupMessageFileParser.ParseFromFile(Parse, path)
                : LookupMessageFileParser.ParseFromFile(ParseWithRequestId, path);
        }

        public string ToCsv()
        {
            return RequestId == null
                ? FormattableString.Invariant($"{Timestamp.ToString(IntervalDateTimeFormat, CultureInfo.InvariantCulture)},{High},{Low},{Open},{Close},{TotalVolume},{PeriodVolume},{NumberOfTrades}")
                : FormattableString.Invariant($"{RequestId},{Timestamp.ToString(IntervalDateTimeFormat, CultureInfo.InvariantCulture)},{High},{Low},{Open},{Close},{TotalVolume},{PeriodVolume},{NumberOfTrades}");
        }

        public override bool Equals(object obj)
        {
            return obj is IntervalMessage message &&
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
            return $"{nameof(Timestamp)}: {Timestamp}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Open)}: {Open}, {nameof(Close)}: {Close}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(PeriodVolume)}: {PeriodVolume}, {nameof(NumberOfTrades)}: {NumberOfTrades}, {nameof(RequestId)}: {RequestId}";
        }

        private static bool TryParseInner(string message, out IntervalMessage intervalMessage, bool hasRequestId)
        {
            var messageDataIdIndex = hasRequestId ? 1 : 0;
            var indexBase = messageDataIdIndex;
            var values = message.SplitFeedMessage();
            if (values[messageDataIdIndex] == HistoricalMessageHandler.HistoricalDataId)
            {
                // protocol 6.2
                return TryParseInnerParser(values, hasRequestId, ++indexBase, out intervalMessage);
            }

            // protocol <6.2
            return TryParseInnerParser(values, hasRequestId, indexBase, out intervalMessage);
        }

        private static bool TryParseInnerParser(string[] values, bool hasRequestId, int indexBase, out IntervalMessage intervalMessage)
        {
            intervalMessage = null;
            DateTime timestamp = default;
            double high = default;
            double low = default;
            double open = default;
            double close = default;
            long totalVolume = default;
            long periodVolume = default;
            int numberOfTrades = default;

            var parsed =
                         DateTime.TryParseExact(values[indexBase + 0], IntervalDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out timestamp) &&
                         double.TryParse(values[indexBase + 1], NumberStyles.Any, CultureInfo.InvariantCulture, out high) &&
                         double.TryParse(values[indexBase + 2], NumberStyles.Any, CultureInfo.InvariantCulture, out low) &&
                         double.TryParse(values[indexBase + 3], NumberStyles.Any, CultureInfo.InvariantCulture, out open) &&
                         double.TryParse(values[indexBase + 4], NumberStyles.Any, CultureInfo.InvariantCulture, out close) &&
                         long.TryParse(values[indexBase + 5], NumberStyles.Any, CultureInfo.InvariantCulture, out totalVolume) &&
                         long.TryParse(values[indexBase + 6], NumberStyles.Any, CultureInfo.InvariantCulture, out periodVolume) &&
                         int.TryParse(values[indexBase + 7], NumberStyles.Any, CultureInfo.InvariantCulture, out numberOfTrades);

            if (parsed)
            {
                intervalMessage = new IntervalMessage(
                    timestamp, 
                    high, 
                    low, 
                    open, 
                    close, 
                    totalVolume, 
                    periodVolume, 
                    numberOfTrades,
                    hasRequestId ? values[0] : null);
            }

            return parsed;
        }
    }
}