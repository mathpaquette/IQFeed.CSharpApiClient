using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.News.Messages
{
    public class NewsHeadlinesMessage
    {
        // 20200611133103
        public const string NewsHeadlinesTimestampFormat = "yyyyMMddHHmmss";

        public string RequestId { get; private set; }
        public string Source { get; private set; }
        public string HeadlineId { get; private set; }
        public string[] Symbols { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string HeadlineText { get; private set; }

        public NewsHeadlinesMessage(string source, string headlineId, string[] symbols, DateTime timestamp, string headlineText, string requestId = null)
        {
            RequestId = requestId;
            Source = source;
            HeadlineId = headlineId;
            Symbols = symbols;
            Timestamp = timestamp;
            HeadlineText = headlineText;
        }

        public static NewsHeadlinesMessage Parse(string message)
        {
            // N,BEN,22290175664,:BZRatings::AAPL::AMD::AVGO::INTC::MRVL::NVDA::TSLA::XLNX:,20200612131321,Why Key Intel Chip Design Exec's Departure Is Positive For AMD, Nvidia
            var values = message.SplitFeedMessage();
            var symbols = values[3].Replace("::", ",").Replace(":", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new NewsHeadlinesMessage(
                values[1],
                values[2],
                symbols,
                DateTime.ParseExact(values[4], NewsHeadlinesTimestampFormat, CultureInfo.InvariantCulture),
                values[5]
            );
        }

        public static NewsHeadlinesMessage ParseWithRequestId(string message)
        {
            // TEST,N,BEN,22290175664,:BZRatings::AAPL::AMD::AVGO::INTC::MRVL::NVDA::TSLA::XLNX:,20200612131321,Why Key Intel Chip Design Exec's Departure Is Positive For AMD, Nvidia
            var values = message.SplitFeedMessage();
            var symbols = values[4].Replace("::", ",").Replace(":", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            return new NewsHeadlinesMessage(
                values[2],
                values[3],
                symbols,
                DateTime.ParseExact(values[5], NewsHeadlinesTimestampFormat, CultureInfo.InvariantCulture),
                values[6],
                values[0]
            );
        }

        public override bool Equals(object obj)
        {
            return obj is NewsHeadlinesMessage message &&
                   RequestId == message.RequestId &&
                   Source == message.Source &&
                   HeadlineId == message.HeadlineId &&
                   Symbols == message.Symbols &&
                   Timestamp == message.Timestamp &&
                   HeadlineText == message.HeadlineText;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Source.GetHashCode();
                hash = hash * 29 + HeadlineId.GetHashCode();
                hash = hash * 29 + Symbols.GetHashCode();
                hash = hash * 29 + Timestamp.GetHashCode();
                hash = hash * 29 + HeadlineText.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(RequestId)}: {RequestId}, {nameof(Source)}: {Source}, {nameof(HeadlineId)}: {HeadlineId}, {nameof(Symbols)}: {Symbols}, {nameof(Timestamp)}: {Timestamp}, {nameof(HeadlineText)}: {HeadlineText}";
        }
    }
}