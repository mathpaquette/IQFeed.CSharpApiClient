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

        public static FutureOptionMessage CreateFutureOptionMessage(string futureOptionSymbol)
        {
            var m = Regex.Match(futureOptionSymbol, FutureOptionSymbolPattern);
            var future = FutureMessage.CreateFutureMessage(m.Groups[FutureSymbolComponent].Value);
            var optionSide = m.Groups[FutureOptionSideComponent].Value == "C" ? OptionSide.Call : OptionSide.Put;
            var strikePrice = float.Parse(m.Groups[FutureOptionStrikePriceComponent].Value) / 100f;

            return new FutureOptionMessage(futureOptionSymbol, future, optionSide, strikePrice);
        }

        public override string ToString()
        {
            return $"{Future} {Side} {StrikePrice}".ToUpper();
        }
    }
}