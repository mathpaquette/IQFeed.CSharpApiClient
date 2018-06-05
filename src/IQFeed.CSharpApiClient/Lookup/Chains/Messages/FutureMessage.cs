using System;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    public class FutureMessage
    {
        private const string FutureSymbolPattern = @"(.*)([F|G|H|J|K|M|N|Q|U|V|X|Z])(\d{2})";

        private const int FutureRootComponent = 1;
        private const int FutureMonthCodeComponent = 2;
        private const int FutureYearComponent = 3;

        public string Symbol { get; }
        public string FutureRoot { get; }
        
        /// <summary>
        /// Represent only year and month of contract expiration 
        /// Exact trade date has to be found using fundamental data 
        /// </summary>
        public DateTime Expiration { get; }

        public FutureMessage(string symbol, string futureRoot, DateTime expiration)
        {
            Symbol = symbol;
            FutureRoot = futureRoot;
            Expiration = expiration;
        }

        public static FutureMessage CreateFutureMessage(string futureSymbol)
        {
            var m = Regex.Match(futureSymbol, FutureSymbolPattern);
            var futureRoot = m.Groups[FutureRootComponent].Value;
            var futureMonth = FutureMonthCode.Decode(m.Groups[FutureMonthCodeComponent].Value);
            var futureYear = int.Parse($"20{m.Groups[FutureYearComponent].Value}");

            return new FutureMessage(futureSymbol, futureRoot, new DateTime(futureYear, futureMonth, 1));
        }

        public override string ToString()
        {
            return $"{FutureRoot} {Expiration.Date:MMM yy}".ToUpper();
        }
    }
}