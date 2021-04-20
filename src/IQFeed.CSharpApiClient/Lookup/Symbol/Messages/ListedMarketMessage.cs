using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class ListedMarketMessage
    {
        public ListedMarketMessage(int listedMarketId, string shortName, string longName, int groupID, string shortGroupName, string requestId = null)
        {
            ListedMarketId = listedMarketId;
            ShortName = shortName;
            LongName = longName;
            GroupId = groupID;
            ShortGroupName = shortGroupName;
            RequestId = requestId;
        }

        public int ListedMarketId { get; private set; }
        public string ShortName { get; private set; }
        public string LongName { get; private set; }
        public int GroupId { get; private set; }
        public string ShortGroupName { get; private set; }
        public string RequestId { get; private set; }

        public static ListedMarketMessage Parse(string message)
        {
            // 6 fields for protocol <=6.1, 7 fields protocol==6.2 (without requestId) last field always null
            var values = message.SplitFeedMessage();

            if (values[0] == SymbolDefault.SymbolsDataId)
            {
                return new ListedMarketMessage(
                    int.Parse(values[1], CultureInfo.InvariantCulture),
                    values[2],
                    values[3],
                    int.Parse(values[4], CultureInfo.InvariantCulture),
                    values[5]);
            }

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
            if (values[1] == SymbolDefault.SymbolsDataId)
            {
                return new ListedMarketMessage(
                    int.Parse(values[2], CultureInfo.InvariantCulture),
                    values[3],
                    values[4],
                    int.Parse(values[5], CultureInfo.InvariantCulture),
                    values[6],
                    values[0]);
            }

            return new ListedMarketMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                values[3],
                int.Parse(values[4], CultureInfo.InvariantCulture),
                values[5],
                values[0]);
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

        public override string ToString()
        {
            return $"{nameof(ListedMarketId)}: {ListedMarketId}, {nameof(ShortName)}: {ShortName}, {nameof(LongName)}: {LongName}, {nameof(GroupId)}: {GroupId}, {nameof(ShortGroupName)}: {ShortGroupName}, {nameof(RequestId)}: {RequestId}";
        }
    }
}