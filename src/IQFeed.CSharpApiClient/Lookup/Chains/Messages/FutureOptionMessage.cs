using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureOptionMessage
    {
        private const string FutureOptionSymbolPattern = @"(.*)(C|P)(\d*)";

        private const int FutureSymbolComponent = 1;
        private const int FutureOptionSideComponent = 2;
        private const int FutureOptionStrikePriceComponent = 3;

        public string Symbol { get; }
        public FutureMessage Future { get; }
        public OptionSide Side { get; }
        public float StrikePrice { get; }

        public FutureOptionMessage(string symbol, FutureMessage future, OptionSide side, float strikePrice)
        {
            Symbol = symbol;
            Future = future;
            Side = side;
            StrikePrice = strikePrice;
        }

        public static FutureOptionMessage Parse(string futureOptionSymbol)
        {
            var m = Regex.Match(futureOptionSymbol, FutureOptionSymbolPattern);
            var future = FutureMessage.Parse(m.Groups[FutureSymbolComponent].Value);
            var optionSide = m.Groups[FutureOptionSideComponent].Value == "C" ? OptionSide.Call : OptionSide.Put;
            var strikePrice = float.Parse(m.Groups[FutureOptionStrikePriceComponent].Value, CultureInfo.InvariantCulture) / 100f;

            return new FutureOptionMessage(futureOptionSymbol, future, optionSide, strikePrice);
        }

        public override bool Equals(object obj)
        {
            return obj is FutureOptionMessage message &&
                   Symbol == message.Symbol &&
                   EqualityComparer<FutureMessage>.Default.Equals(Future, message.Future) &&
                   Side == message.Side &&
                   StrikePrice == message.StrikePrice;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Symbol);
                hash = hash * 29 + EqualityComparer<FutureMessage>.Default.GetHashCode(Future);
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