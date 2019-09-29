using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SymbolBySicCodeMessage : ISymbolBySicCodeMessage
    {
        public SymbolBySicCodeMessage(int sicCode, string symbol, int listedMarketId, int securityTypeId, string description, string requestId = null)
        {
            RequestId = requestId;
            SicCode = sicCode;
            Symbol = symbol;
            ListedMarketId = listedMarketId;
            SecurityTypeId = securityTypeId;
            Description = description;
        }

        public string RequestId { get; }
        public int SicCode { get; }
        public string Symbol { get; }
        public int ListedMarketId { get; }
        public int SecurityTypeId { get; }
        public string Description { get; }

        public static SymbolBySicCodeMessage Parse(string message)
        {
            var values = message.SplitFeedMessage(5);

            return new SymbolBySicCodeMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture),
                values[1],
                int.Parse(values[2], CultureInfo.InvariantCulture),
                int.Parse(values[3], CultureInfo.InvariantCulture),
                values[4]);
        }

        public static SymbolBySicCodeMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage(6);
            var requestId = values[0];

            return new SymbolBySicCodeMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                int.Parse(values[3], CultureInfo.InvariantCulture),
                int.Parse(values[4], CultureInfo.InvariantCulture),
                values[5],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is SymbolBySicCodeMessage message &&
                   RequestId == message.RequestId &&
                   SicCode == message.SicCode &&
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
                hash = hash * 29 + SicCode.GetHashCode();
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