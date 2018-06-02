using System.Collections.Generic;
using System.Linq;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols
{
    public static class MarketSymbolExtensions
    {
        public static IEnumerable<MarketSymbol> AreEquitiesOrIndexes(this IEnumerable<MarketSymbol> marketSymbols)
        {
            return marketSymbols.Where(x => x.SecurityType == MarketSymbolSecurityType.EQUITY || x.SecurityType == MarketSymbolSecurityType.INDEX);
        }
    }
}