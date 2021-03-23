using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public interface ISymbolFacadeDownload
    {
        IEnumerable<MarketSymbol> GetAllMarketSymbols(string url = SymbolDefault.MarketSymbolsArchiveUrl, bool useCache = true, TimeSpan? expiration = null);
        IEnumerable<MarketSymbol> GetAllMarketSymbols(string downloadFolder, string url = SymbolDefault.MarketSymbolsArchiveUrl, bool useCache = true, TimeSpan? expiration = null);
        
        IEnumerable<ExpiredOption> GetAllExpiredOptions(string url = SymbolDefault.ExpiredOptionsArchiveUrl, bool useCache = true, TimeSpan? expiration = null);
        IEnumerable<ExpiredOption> GetAllExpiredOptions(string downloadFolder, string url = SymbolDefault.ExpiredOptionsArchiveUrl, bool useCache = true, TimeSpan? expiration = null);
    }
}