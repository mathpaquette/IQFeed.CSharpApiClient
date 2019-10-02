using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SymbolByNaicsCodeMessage : ISymbolByNaicsCodeMessage
    {
        public SymbolByNaicsCodeMessage(int naicsCode, string symbol, int listedMarketId, int securityTypeId, string description, string requestId = null)
        {
            RequestId = requestId;
            NaicsCode = naicsCode;
            Symbol = symbol;
            ListedMarketId = listedMarketId;
            SecurityTypeId = securityTypeId;
            Description = description;
        }

        public string RequestId { get; }
        public int NaicsCode { get; }
        public string Symbol { get; }
        public int ListedMarketId { get; }
        public int SecurityTypeId { get; }
        public string Description { get; }

        public static SymbolByNaicsCodeMessage Parse(string message)
        {
            var values = message.SplitFeedMessage(5);

            return new SymbolByNaicsCodeMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture),
                values[1],
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                values[4]);
        }

        public static SymbolByNaicsCodeMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage(6);
            var requestId = values[0];

            return new SymbolByNaicsCodeMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                int.Parse(values[3], CultureInfo.InvariantCulture),
                int.Parse(values[4], CultureInfo.InvariantCulture),
                values[5],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is SymbolByNaicsCodeMessage message &&
                   RequestId == message.RequestId &&
                   NaicsCode == message.NaicsCode &&
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
                hash = hash * 29 + NaicsCode.GetHashCode();
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