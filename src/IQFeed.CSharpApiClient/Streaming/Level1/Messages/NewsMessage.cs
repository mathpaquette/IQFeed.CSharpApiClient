using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    [Serializable]
    public class NewsMessage
    {
        public const string NewsMessageDatetimeFormat = "yyyyMMdd HHmmss";

        private NewsMessage()
        {
            //empty constructor for serialization.
        }
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

        public static NewsMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            var distributorCode = values[1];
            var storyId = values[2];
            var symbolList = values[3];
            DateTime.TryParseExact(values[4], NewsMessageDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var timestamp);
            var headline = values[5];

            return new NewsMessage(distributorCode, storyId, symbolList, timestamp, headline);
        }

        public override bool Equals(object obj)
        {
            return obj is NewsMessage message &&
                   DistributorCode == message.DistributorCode &&
                   StoryId == message.StoryId &&
                   SymbolList == message.SymbolList &&
                   Timestamp == message.Timestamp &&
                   Headline == message.Headline;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + DistributorCode.GetHashCode();
                hash = hash * 29 + StoryId.GetHashCode();
                hash = hash * 29 + Timestamp.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(DistributorCode)}: {DistributorCode}, {nameof(StoryId)}: {StoryId}, {nameof(SymbolList)}: {SymbolList}, {nameof(Timestamp)}: {Timestamp}, {nameof(Headline)}: {Headline}";
        }
    }
}