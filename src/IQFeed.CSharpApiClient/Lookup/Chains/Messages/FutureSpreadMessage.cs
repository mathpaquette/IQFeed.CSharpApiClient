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
                FutureMessage.CreateFutureMessage(futureSymbol1),
                FutureMessage.CreateFutureMessage(futureSymbol2)
            );
        }

        public override string ToString()
        {
            return $"{Spread1} - {Spread2}";
        }
    }
}