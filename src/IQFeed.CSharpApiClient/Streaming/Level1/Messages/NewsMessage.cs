using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class NewsMessage
    {
        public const string NewsMessageDatetimeFormat = "yyyyMMdd HHmmss";

        public NewsMessage(string distributorCode, string storyId, string symbolList, DateTime timestamp, string headline)
        {
            DistributorCode = distributorCode;
            StoryId = storyId;
            SymbolList = symbolList;
            Timestamp = timestamp;
            Headline = headline;
        }

        public string DistributorCode { get; }
        public string StoryId { get; }
        public string SymbolList { get; }
        public DateTime Timestamp { get; }
        public string Headline { get; }

        public static NewsMessage CreateNewsMessage(string[] values)
        {
            var distributorCode = values[0];
            var storyId = values[1];
            var symbolList = values[2];
            DateTime.TryParseExact(values[3], NewsMessageDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            var headline = values[4];

            return new NewsMessage(distributorCode, storyId, symbolList, timestamp, headline);
        }
    }
}