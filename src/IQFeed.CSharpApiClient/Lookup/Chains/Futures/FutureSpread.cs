using System;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Futures
{
    [Serializable]
    public class FutureSpread
    {
        private const string FutureSpreadSymbolPattern = "(.*)(-)(.*)";

        private const int FutureSymbol1Component = 1;
        private const int FutureSymbol2Component = 3;

        public string Symbol { get; }
        public Future Spread1 { get; }
        public Future Spread2 { get; }

        private FutureSpread()
        {
            //empty constructor for serialization.
        }

        public FutureSpread(string symbol, Future spread1, Future spread2)
        {
            Symbol = symbol;
            Spread1 = spread1;
            Spread2 = spread2;
        }

        public static FutureSpread Parse(string futureSpreadSymbol)
        {
            var m = Regex.Match(futureSpreadSymbol, FutureSpreadSymbolPattern);
            var futureSymbol1 = m.Groups[FutureSymbol1Component].Value;
            var futureSymbol2 = m.Groups[FutureSymbol2Component].Value;

            return new FutureSpread(
                futureSpreadSymbol,
                Future.Parse(futureSymbol1),
                Future.Parse(futureSymbol2)
            );
        }

        public override bool Equals(object obj)
        {
            return obj is FutureSpread message &&
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