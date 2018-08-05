using System;
using System.Globalization;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Messages
{
    public class IntervalBarMessage
    {
        private const string IntervalBarMessageDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private const string IntervalBarMessageWithRequestIdPattern = @"^.*,B[U|H|C],.*,.*,.*,.*,.*,.*,.*,.*,.*,$";
        private const string IntervalBarMessageWithoutRequestIdPattern = @"^B[U|H|C],.*,.*,.*,.*,.*,.*,.*,.*,.*,$";

        private static readonly Regex IntervalBarMessageWithRequestIdRegex = new Regex(IntervalBarMessageWithRequestIdPattern);
        private static readonly Regex IntervalBarMessageWithoutRequestIdRegex = new Regex(IntervalBarMessageWithoutRequestIdPattern);

        public IntervalBarMessage(IntervalBarType type, string symbol, DateTime timestamp, float open, float high, float low, float last,
            int cummulativeVolume, int intervalVolume, int numberOfTrades, string requestId = null)
        {
            RequestId = requestId;
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
        }

        public string RequestId { get; }
        public IntervalBarType Type { get; }
        public string Symbol { get; }
        public DateTime Timestamp { get; }
        public float Open { get; }
        public float High { get; }
        public float Low { get; }
        public float Last { get; }

        /// <summary>
        /// Last cummulative volume in the interval
        /// </summary>
        public int CummulativeVolume { get; }

        /// <summary>
        /// Interval volume for the interval
        /// </summary>
        public int IntervalVolume { get; }
       
        /// <summary>
        /// Number of trades in the interval (only valid for tick interval)
        /// </summary>
        public int NumberOfTrades { get; }

        public static bool TryParse(string message, out IntervalBarMessage intervalBarMessage)
        {
            intervalBarMessage = null;

            if (IntervalBarMessageWithoutRequestIdRegex.IsMatch(message))
                intervalBarMessage = ParseWithoutRequestId(message);

            else if (IntervalBarMessageWithRequestIdRegex.IsMatch(message))
                intervalBarMessage = ParseWithRequestId(message);

            return intervalBarMessage != null;
        }

        public static IntervalBarMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();

            var requestId = values[0];
            Enum.TryParse(values[1].Substring(1), out IntervalBarType type);
            var symbol = values[2];
            DateTime.TryParseExact(values[3], IntervalBarMessageDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            float.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);
            float.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);
            float.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);
            float.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var last);
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var cummulativeVolume);
            int.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var intervalVolume);
            int.TryParse(values[10], NumberStyles.Any, CultureInfo.InvariantCulture, out var numberOfTrades);

            return new IntervalBarMessage(type, symbol, timestamp, open, high, low, last, cummulativeVolume, intervalVolume, numberOfTrades, requestId);
        }

        public static IntervalBarMessage ParseWithoutRequestId(string message)
        {
            var values = message.SplitFeedMessage();

            Enum.TryParse(values[0].Substring(1), out IntervalBarType type);
            var symbol = values[1];
            DateTime.TryParseExact(values[2], IntervalBarMessageDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            float.TryParse(values[3], NumberStyles.Any, CultureInfo.InvariantCulture, out var open);
            float.TryParse(values[4], NumberStyles.Any, CultureInfo.InvariantCulture, out var high);
            float.TryParse(values[5], NumberStyles.Any, CultureInfo.InvariantCulture, out var low);
            float.TryParse(values[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var last);
            int.TryParse(values[7], NumberStyles.Any, CultureInfo.InvariantCulture, out var cummulativeVolume);
            int.TryParse(values[8], NumberStyles.Any, CultureInfo.InvariantCulture, out var intervalVolume);
            int.TryParse(values[9], NumberStyles.Any, CultureInfo.InvariantCulture, out var numberOfTrades);

            return new IntervalBarMessage(type, symbol, timestamp, open, high, low, last, cummulativeVolume, intervalVolume, numberOfTrades);
        }

        public override bool Equals(object obj)
        {
            return obj is IntervalBarMessage message &&
                   RequestId == message.RequestId &&
                   Type == message.Type &&
                   Symbol == message.Symbol &&
                   Timestamp == message.Timestamp &&
                   Open == message.Open &&
                   High == message.High &&
                   Low == message.Low &&
                   Last == message.Last &&
                   CummulativeVolume == message.CummulativeVolume &&
                   IntervalVolume == message.IntervalVolume &&
                   NumberOfTrades == message.NumberOfTrades;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId.GetHashCode();
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
            return $"{nameof(RequestId)}: {RequestId}, {nameof(Type)}: {Type}, {nameof(Symbol)}: {Symbol}, {nameof(Timestamp)}: {Timestamp}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Last)}: {Last}, {nameof(CummulativeVolume)}: {CummulativeVolume}, {nameof(IntervalVolume)}: {IntervalVolume}, {nameof(NumberOfTrades)}: {NumberOfTrades}";
        }
    }
}