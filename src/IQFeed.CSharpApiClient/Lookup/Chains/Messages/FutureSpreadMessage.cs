using System.Text.RegularExpressions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureSpreadMessage
    {
        private const string FutureSpreadSymbolPattern = "(.*)(-)(.*)";

        private const int FutureSymbol1Component = 1;
        private const int FutureSymbol2Component = 3;

        public string Symbol { get; }
        public FutureMessage Spread1 { get; }
        public FutureMessage Spread2 { get; }

        public FutureSpreadMessage(string symbol, FutureMessage spread1, FutureMessage spread2)
        {
            Symbol = symbol;
            Spread1 = spread1;
            Spread2 = spread2;
        }

        public static FutureSpreadMessage CreateFutureSpreadMessage(string futureSpreadSymbol)
        {
            var m = Regex.Match(futureSpreadSymbol, FutureSpreadSymbolPattern);
            var futureSymbol1 = m.Groups[FutureSymbol1Component].Value;
            var futureSymbol2 = m.Groups[FutureSymbol2Component].Value;

            return new FutureSpreadMessage(
                futureSpreadSymbol,
                FutureMessage.Parse(futureSymbol1),
                FutureMessage.Parse(futureSymbol2)
            );
        }

        public override bool Equals(object obj)
        {
            return obj is FutureSpreadMessage message &&
                   Symbol == message.Symbol &&
                   Spread1.Equals(message.Spread1) &&
                   Spread2.Equals(message.Spread2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + Spread1.GetHashCode();
                hash = hash * 29 + Spread2.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Spread1} - {Spread2}";
        }
    }
}