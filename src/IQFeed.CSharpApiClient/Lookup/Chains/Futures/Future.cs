using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Futures
{
    [Serializable]
    public class Future
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

        private Future() 
        {
            //empty constructor for serialization.
        }

        public Future(string symbol, string futureRoot, DateTime expiration)
        {
            Symbol = symbol;
            FutureRoot = futureRoot;
            Expiration = expiration;
        }

        public static Future Parse(string futureSymbol)
        {
            var m = Regex.Match(futureSymbol, FutureSymbolPattern);
            var futureRoot = m.Groups[FutureRootComponent].Value;
            var futureMonth = FutureMonthCode.Decode(m.Groups[FutureMonthCodeComponent].Value);
            var futureYear = int.Parse($"20{m.Groups[FutureYearComponent].Value}", CultureInfo.InvariantCulture);

            return new Future(futureSymbol, futureRoot, new DateTime(futureYear, futureMonth, 1));
        }

        public override bool Equals(object obj)
        {
            return obj is Future message &&
                   Symbol == message.Symbol &&
                   FutureRoot == message.FutureRoot &&
                   Expiration.Date.Year == message.Expiration.Date.Year &&
                   Expiration.Date.Month == message.Expiration.Date.Month;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + FutureRoot.GetHashCode();
                hash = hash * 29 + Expiration.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{FutureRoot} {Expiration.Date:MMM yy}".ToUpper();
        }
    }
}