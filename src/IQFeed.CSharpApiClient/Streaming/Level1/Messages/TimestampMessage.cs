using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    // T,[YYYYMMDD HH:mm:SS]<LF>
    public class TimestampMessage
    {
        private const string TimestampMessageDatetimeFormat = "yyyyMMdd HH:mm:ss";

        public TimestampMessage(DateTime timestamp)
        {
            Timestamp = timestamp;
        }

        public DateTime Timestamp { get; }

        public static TimestampMessage CreateTimestampMessage(string value)
        {
            DateTime.TryParseExact(value, TimestampMessageDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            return new TimestampMessage(timestamp);
        }

        public override string ToString()
        {
            return $"{nameof(Timestamp)}: {Timestamp}";
        }
    }
}