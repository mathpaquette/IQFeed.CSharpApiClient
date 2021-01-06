using System;
using System.Globalization;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Messages
{
    public class IntervalBarMessage : IIntervalBarMessage
    {
        public const string IntervalBarMessageDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        public const string IntervalBarMessageWithRequestIdPattern = @"^.*,B[U|H|C],.*,.*,.*,.*,.*,.*,.*,.*,.*$";
        public const string IntervalBarMessageWithoutRequestIdPattern = @"^B[U|H|C],.*,.*,.*,.*,.*,.*,.*,.*,.*$";

        public static readonly Regex IntervalBarMessageWithRequestIdRegex = new Regex(IntervalBarMessageWithRequestIdPattern);
        public static readonly Regex IntervalBarMessageWithoutRequestIdRegex = new Regex(IntervalBarMessageWithoutRequestIdPattern);

        public IntervalBarMessage(
            IntervalBarType type,
            string symbol,
            DateTime timestamp,
            double open,
            double high,
            double low,
            double last,
            long cummulativeVolume,
            long intervalVolume,
            int numberOfTrades,
            string requestId = null)
        {
            Type = type;
            Symbol = symbol;
            Timestamp = timestamp;
            Open = open;
            High = high;
            Low = low;
            Last = last;
            CummulativeVolume = cummulativeVolume;
            IntervalVolume = intervalVolume;
            NumberOfTrades = numberOfTrades;
            RequestId = requestId;
        }

        public IntervalBarType Type { get; private set; }
        public string Symbol { get; private set; }
        public DateTime Timestamp { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Last { get; private set; }

        /// <summary>
        /// Last cummulative volume in the interval
        /// </summary>
        public long CummulativeVolume { get; private set; }

        /// <summary>
        /// Interval volume for the interval
        /// </summary>
        public long IntervalVolume { get; private set; }

        /// <summary>
        /// Number of trades in the interval (only valid for tick interval)
        /// </summary>
        public int NumberOfTrades { get; private set; }

        public string RequestId { get; private set; }

        public static IntervalBarMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            Enum.TryParse(values[0].Substring(1), out IntervalBarType type);
            var symbol = values[1];
            DateTime.TryParseExact(values[2], IntervalBarMessageDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            double.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);
            double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);
            double.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);
            double.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var last);
            long.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var cummulativeVolume);
            long.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var intervalVolume);
            int.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var numberOfTrades);

            return new IntervalBarMessage(type, symbol, timestamp, open, high, low, last, cummulativeVolume, intervalVolume, numberOfTrades);
        }

        public static IntervalBarMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();

            var requestId = values[0];
            Enum.TryParse(values[1].Substring(1), out IntervalBarType type);
            var symbol = values[2];
            DateTime.TryParseExact(values[3], IntervalBarMessageDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            double.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);
            double.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);
            double.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);
            double.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var last);
            long.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var cummulativeVolume);
            long.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var intervalVolume);
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var numberOfTrades);

            return new IntervalBarMessage(type, symbol, timestamp, open, high, low, last, cummulativeVolume, intervalVolume, numberOfTrades, requestId);
        }

        public static bool TryParse(string message, out IntervalBarMessage intervalBarMessage)
        {
            intervalBarMessage = null;

            if (IntervalBarMessageWithoutRequestIdRegex.IsMatch(message))
                intervalBarMessage = Parse(message);

            else if (IntervalBarMessageWithRequestIdRegex.IsMatch(message))
                intervalBarMessage = ParseWithRequestId(message);

            return intervalBarMessage != null;
        }

        public override bool Equals(object obj)
        {
            return obj is IntervalBarMessage message &&
                   RequestId == message.RequestId &&
                   Type == message.Type &&
                   Symbol == message.Symbol &&
                   Timestamp == message.Timestamp &&
                   Equals(Open, message.Open) &&
                   Equals(High, message.High) &&
                   Equals(Low, message.Low) &&
                   Equals(Last, message.Last) &&
                   CummulativeVolume == message.CummulativeVolume &&
                   IntervalVolume == message.IntervalVolume &&
                   NumberOfTrades == message.NumberOfTrades;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Type.GetHashCode();
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + Open.GetHashCode();
                hash = hash * 29 + High.GetHashCode();
                hash = hash * 29 + Low.GetHashCode();
                hash = hash * 29 + Last.GetHashCode();
                hash = hash * 29 + CummulativeVolume.GetHashCode();
                hash = hash * 29 + IntervalVolume.GetHashCode();
                hash = hash * 29 + NumberOfTrades.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(Symbol)}: {Symbol}, {nameof(Timestamp)}: {Timestamp}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Last)}: {Last}, {nameof(CummulativeVolume)}: {CummulativeVolume}, {nameof(IntervalVolume)}: {IntervalVolume}, {nameof(NumberOfTrades)}: {NumberOfTrades}, {nameof(RequestId)}: {RequestId}";
        }
    }
}