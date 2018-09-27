using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolFacade : ISymbolFacade
    {
        private readonly MarketSymbolDownloader _marketSymbolDownloader;
        private readonly MarketSymbolReader _marketSymbolReader;
        private readonly ExpiredOptionDownloader _expiredOptionDownloader;
        private readonly ExpiredOptionReader _expiredOptionReader;

        public SymbolFacade(
            MarketSymbolDownloader marketSymbolDownloader, 
            MarketSymbolReader marketSymbolReader,
            ExpiredOptionDownloader expiredOptionDownloader,
            ExpiredOptionReader expiredOptionReader)
        {
            _expiredOptionReader = expiredOptionReader;
            _expiredOptionDownloader = expiredOptionDownloader;
            _marketSymbolReader = marketSymbolReader;
            _marketSymbolDownloader = marketSymbolDownloader;
        }

        public IEnumerable<MarketSymbol> GetAllMarketSymbols(string url = SymbolDefault.MarketSymbolsArchiveUrl, string downloadPath = null, bool useCache = true)
        {
            var filename = _marketSymbolDownloader.GetFile(url, downloadPath, useCache);
            return _marketSymbolReader.GetMarketSymbols(filename);
        }

        public IEnumerable<ExpiredOption> GetAllExpiredOptions(string url = SymbolDefault.ExpiredOptionsArchiveUrl, string downloadPath = null, bool useCache = true, bool header = false)
        {
            var filename = _expiredOptionDownloader.GetFile(url, downloadPath, useCache);
            return _expiredOptionReader.GetExpiredOptions(filename, header);
        }
    }
}