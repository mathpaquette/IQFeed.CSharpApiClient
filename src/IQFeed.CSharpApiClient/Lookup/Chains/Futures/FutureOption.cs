using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Futures
{
    public class FutureOption
    {
        private const string FutureOptionSymbolPattern = @"(.*)(C|P)(\d*)";

        private const int FutureSymbolComponent = 1;
        private const int FutureOptionSideComponent = 2;
        private const int FutureOptionStrikePriceComponent = 3;

        public string Symbol { get; }
        public Future Future { get; }
        public OptionSide Side { get; }
        public double StrikePrice { get; }

        public FutureOption(string symbol, Future future, OptionSide side, double strikePrice)
        {
            Symbol = symbol;
            Future = future;
            Side = side;
            StrikePrice = strikePrice;
        }

        public static FutureOption Parse(string futureOptionSymbol)
        {
            var m = Regex.Match(futureOptionSymbol, FutureOptionSymbolPattern);
            var future = Future.Parse(m.Groups[FutureSymbolComponent].Value);
            var optionSide = m.Groups[FutureOptionSideComponent].Value == "C" ? OptionSide.Call : OptionSide.Put;
            var strikePrice = double.Parse(m.Groups[FutureOptionStrikePriceComponent].Value, CultureInfo.InvariantCulture) / 100f;

            return new FutureOption(futureOptionSymbol, future, optionSide, strikePrice);
        }

        public override bool Equals(object obj)
        {
            return obj is FutureOption message &&
                   Symbol == message.Symbol &&
                   EqualityComparer<Future>.Default.Equals(Future, message.Future) &&
                   Side == message.Side &&
                   StrikePrice == message.StrikePrice;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + Future.GetHashCode();
                hash = hash * 29 + Side.GetHashCode();
                hash = hash * 29 + StrikePrice.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Future} {Side} {StrikePrice}".ToUpper();
        }
    }
}