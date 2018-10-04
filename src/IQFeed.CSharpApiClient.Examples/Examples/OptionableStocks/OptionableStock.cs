using System.Collections.Generic;
using System.Linq;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Examples.Examples.OptionableStocks
{
    public class OptionableStock
    {
        public OptionableStock(MarketSymbol marketSymbol, IEnumerable<EquityOption> options)
        {
            MarketSymbol = marketSymbol;
            Options = options;
        }

        public MarketSymbol MarketSymbol { get; }
        public IEnumerable<EquityOption> Options { get; }

        public static IEnumerable<OptionableStock> GetOptionableStocks(IEnumerable<MarketSymbol> marketSymbols)
        {
            var options = new List<MarketSymbol>();
            var equities = new List<MarketSymbol>();
            var indexes = new List<MarketSymbol>();

            foreach (var marketSymbol in marketSymbols)
            {
                switch (marketSymbol.SecurityType)
                {
                    case MarketSymbolSecurityType.IEOPTION when !string.IsNullOrEmpty(marketSymbol.Description):
                        options.Add(marketSymbol);
                        break;

                    case MarketSymbolSecurityType.EQUITY:
                        equities.Add(marketSymbol);
                        break;

                    case MarketSymbolSecurityType.INDEX:
                        indexes.Add(marketSymbol);
                        break;
                }
            }

            var equityOptionsBySymbol = options.Select(x => EquityOption.Parse(x.Symbol)).GroupBy(x => x.EquitySymbol);
            var equitiesBySymbol = equities.Concat(indexes).ToDictionary(x => x.Symbol);

            foreach (var equityOptions in equityOptionsBySymbol)
            {
                if (equitiesBySymbol.TryGetValue(equityOptions.Key, out var equity))
                {
                    yield return new OptionableStock(equity, equityOptions);
                }
            }
        }
    }
}