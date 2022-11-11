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
        public string Description { get; }
        public int Exchange { get; }
        public DateTime LastUpdate { get; }

        public static ExpiredOption Parse(string line)
        {
            // Symbol,SecurityType,Description,ExpirationDate,Exchange,LastUpdate
            // CIEN1808R23.5,2,CIEN JUN 2018 P 23.50,20180608,14,20180612
            var values = line.Split(',');

            var equityOption = EquityOption.Parse(values[0]);
            int.TryParse(values[1], out var securityType);
            var title = values[2].Trim();
            // skip values[3] ExpirationDate already part of EquityOption
            int.TryParse(values[4], out var exchange);
            DateTime.TryParseExact(values[5], LastUpdateDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var lastUpdate);

            return new ExpiredOption(equityOption, securityType, title, exchange, lastUpdate);
        }

        public ExpiredOption(EquityOption equityOption, int securityType, string description, int exchange, DateTime lastUpdate)
        {
            EquityOption = equityOption;
            SecurityType = securityType;
            Description = description;
            Exchange = exchange;
            LastUpdate = lastUpdate;
        }
    }
}