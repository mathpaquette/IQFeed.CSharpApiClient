using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SymbolByFilterMessage
    {
        public SymbolByFilterMessage(string symbol, int listedMarketId, int securityTypeId, string description, string requestId = null)
        {
            Symbol = symbol;
            ListedMarketId = listedMarketId;
            SecurityTypeId = securityTypeId;
            Description = description;
            RequestId = requestId;
        }

        public string Symbol { get; private set; }
        public int ListedMarketId { get; private set; }
        public int SecurityTypeId { get; private set; }
        public string Description { get; private set; }
        public string RequestId { get; private set; }

        public static SymbolByFilterMessage Parse(string message)
        {
            var values = message.SplitFeedMessage(4);

            return new SymbolByFilterMessage(
                values[0],
                int.Parse(values[1], CultureInfo.InvariantCulture),
                int.Parse(values[2], CultureInfo.InvariantCulture),
                values[3]);
        }

        public static SymbolByFilterMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage(5);
            var requestId = values[0];

            return new SymbolByFilterMessage(
                values[1],
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                values[4],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is SymbolByFilterMessage message &&
                   RequestId == message.RequestId &&
                   Symbol == message.Symbol &&
                   ListedMarketId == message.ListedMarketId &&
                   SecurityTypeId == message.SecurityTypeId &&
                   Description == message.Description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + ListedMarketId.GetHashCode();
                hash = hash * 29 + SecurityTypeId.GetHashCode();
                hash = hash * 29 + Description.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(ListedMarketId)}: {ListedMarketId}, {nameof(SecurityTypeId)}: {SecurityTypeId}, {nameof(Description)}: {Description}, {nameof(RequestId)}: {RequestId}";
        }
    }
}