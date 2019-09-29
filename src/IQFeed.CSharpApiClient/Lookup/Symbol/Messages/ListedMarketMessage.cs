using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class ListedMarketMessage : IListedMarket
    {
        public ListedMarketMessage(int listedMarketId, string shortName, string longName, int groupID, string shortGroupName, string requestId = null)
        {
            RequestId = requestId;
            ListedMarketId = listedMarketId;
            ShortName = shortName;
            LongName = longName;
            GroupId = groupID;
            ShortGroupName = shortGroupName;
        }

        public string RequestId { get; }
        public int ListedMarketId { get; }
        public string ShortName { get; }
        public string LongName { get; }
        public int GroupId { get; }
        public string ShortGroupName { get; }

        public static ListedMarketMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new ListedMarketMessage(                
                int.Parse(values[0], CultureInfo.InvariantCulture),
                values[1],
                values[2],
                int.Parse(values[3], CultureInfo.InvariantCulture),
                values[4]);
        }

        public static ListedMarketMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new ListedMarketMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                values[3],
                int.Parse(values[4], CultureInfo.InvariantCulture),
                values[5],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is ListedMarketMessage message &&
                   RequestId == message.RequestId &&
                   ListedMarketId == message.ListedMarketId &&
                   ShortName == message.ShortName &&
                   LongName == message.LongName &&
                   GroupId == message.GroupId &&
                   ShortGroupName == message.ShortGroupName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + ListedMarketId.GetHashCode();
                hash = hash * 29 + ShortName.GetHashCode();
                hash = hash * 29 + LongName.GetHashCode();
                hash = hash * 29 + GroupId.GetHashCode();
                hash = hash * 29 + ShortGroupName.GetHashCode();
                return hash;
            }
        }
    }
}