using System;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Messages
{
    /// <summary>
    /// Equity and Index
    /// </summary>
    public class EquityOptionMessage
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

        public EquityOptionMessage(string symbol, string equitySymbol, float strikePrice, DateTime expiration, OptionSide side)
        {
            Symbol = symbol;
            EquitySymbol = equitySymbol;
            StrikePrice = strikePrice;
            Expiration = expiration;
            Side = side;
        }

        public static EquityOptionMessage CreateEquityIndexOptionMessage(string optionSymbol)
        {
            var m = Regex.Match(optionSymbol, OptionSymbolPattern);
            var equitySymbol = m.Groups[EquitySymbolComponent].Value;

            var year = int.Parse($"20{m.Groups[ExpirationYearComponent].Value}");
            var date = int.Parse(m.Groups[ExpirationDateComponent].Value);
            var monthCode = EquityOptionMonthCode.Decode(m.Groups[ExpirationMonthComponent].Value);
            var expiration = new DateTime(year, monthCode.Month, date);

            var strikePrice = float.Parse(m.Groups[StrikePriceComponent].Value);
            var side = monthCode.Side;

            return new EquityOptionMessage(optionSymbol, equitySymbol, strikePrice, expiration, side);
        }

        public override string ToString()
        {
            return $"{Expiration.Date:dd MMM yy} {EquitySymbol}@{StrikePrice} {Side.ToString()}".ToUpper();
        }
    }
}