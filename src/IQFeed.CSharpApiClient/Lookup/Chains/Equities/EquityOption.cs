using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Equities
{
    /// <summary>
    /// Equity and Index
    /// </summary>
    [Serializable]
    public class EquityOption
    {
        private const string OptionSymbolPattern = @"(.{1,5})(\d{2})(\d{2})([A-Z])(.+)";

        private const int EquitySymbolComponent = 1;
        private const int ExpirationYearComponent = 2;
        private const int ExpirationDateComponent = 3;
        private const int ExpirationMonthComponent = 4;
        private const int StrikePriceComponent = 5;

        public string Symbol { get; }
        public string EquitySymbol { get; }
        public float StrikePrice { get; }
        public DateTime Expiration { get; }
        public OptionSide Side { get; }
        private EquityOption()
        {
            //empty constructor for serialization.
        }

        public EquityOption(string symbol, string equitySymbol, float strikePrice, DateTime expiration, OptionSide side)
        {
            Symbol = symbol;
            EquitySymbol = equitySymbol;
            StrikePrice = strikePrice;
            Expiration = expiration;
            Side = side;
        }

        public static EquityOption Parse(string optionSymbol)
        {
            var m = Regex.Match(optionSymbol, OptionSymbolPattern);
            var equitySymbol = m.Groups[EquitySymbolComponent].Value;

            var year = int.Parse($"20{m.Groups[ExpirationYearComponent].Value}", CultureInfo.InvariantCulture);
            var date = int.Parse(m.Groups[ExpirationDateComponent].Value, CultureInfo.InvariantCulture);
            var monthCode = EquityOptionMonthCode.Decode(m.Groups[ExpirationMonthComponent].Value);
            var expiration = new DateTime(year, monthCode.Month, date);

            var strikePrice = float.Parse(m.Groups[StrikePriceComponent].Value, CultureInfo.InvariantCulture);
            var side = monthCode.Side;

            return new EquityOption(optionSymbol, equitySymbol, strikePrice, expiration, side);
        }

        public override bool Equals(object obj)
        {
            return obj is EquityOption message &&
                   Symbol == message.Symbol &&
                   EquitySymbol == message.EquitySymbol &&
                   StrikePrice == message.StrikePrice &&
                   Expiration.Date == message.Expiration.Date &&
                   Side == message.Side;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + EquitySymbol.GetHashCode();
                hash = hash * 29 + StrikePrice.GetHashCode();
                hash = hash * 29 + Expiration.GetHashCode();
                hash = hash * 29 + Side.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{EquitySymbol} {Expiration.Date:dd MMM yy} {Side} {StrikePrice}".ToUpper();
        }
    }
}