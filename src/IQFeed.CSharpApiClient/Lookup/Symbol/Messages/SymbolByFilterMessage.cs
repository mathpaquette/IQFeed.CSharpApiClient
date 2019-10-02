using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SymbolByFilterMessage : ISymbolByFilter
    {
        public SymbolByFilterMessage(string symbol, int listedMarketId, int securityTypeId, string description, string requestId = null)
        {
            RequestId = requestId;
            Symbol = symbol;
            ListedMarketId = listedMarketId;
            SecurityTypeId = securityTypeId;
            Description = description;
        }

        public string RequestId { get; }
        public string Symbol { get; }
        public int ListedMarketId { get; }
        public int SecurityTypeId { get; }
        public string Description { get; }

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
            return $"{Symbol}";
        }
    }
}