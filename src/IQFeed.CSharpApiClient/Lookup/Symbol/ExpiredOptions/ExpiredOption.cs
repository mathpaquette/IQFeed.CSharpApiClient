using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions
{
    public class ExpiredOption
    {
        public const string LastUpdateDateTimeFormat = "yyyyMMdd";

        public EquityOption EquityOption { get; }
        public int SecurityType { get; }
        public string Title { get; }
        public int Exchange { get; }
        public DateTime LastUpdate { get; }

        public static ExpiredOption Parse(string line)
        {
            // ZZQ1020B30,2, FEB 10 C 30,,14,20100114
            var values = line.Split(',');

            var equityOption = EquityOption.Parse(values[0]);
            int.TryParse(values[1], out var securityType);
            var title = values[2].Trim();
            // skip values[3]
            int.TryParse(values[4], out var exchange);
            DateTime.TryParseExact(values[5], LastUpdateDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var lastUpdate);

            return new ExpiredOption(equityOption, securityType, title, exchange, lastUpdate);
        }

        public ExpiredOption(EquityOption equityOption, int securityType, string title, int exchange, DateTime lastUpdate)
        {
            EquityOption = equityOption;
            SecurityType = securityType;
            Title = title;
            Exchange = exchange;
            LastUpdate = lastUpdate;
        }
    }
}