using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolFacade
    {
        private string _marketSymbolsFile;

        private readonly MarketSymbolDownloader _marketSymbolDownloader;
        private readonly MarketSymbolReader _marketSymbolReader;

        public SymbolFacade(MarketSymbolDownloader marketSymbolDownloader, MarketSymbolReader marketSymbolReader)
        {
            _marketSymbolsFile = null;

            _marketSymbolReader = marketSymbolReader;
            _marketSymbolDownloader = marketSymbolDownloader;
        }

        public IEnumerable<MarketSymbol> GetAllMarketSymbols(string downloadPath = null, string marketSymbolsArchiveUrl = IQFeedDefault.MarketSymbolsArchiveUrl)
        {
            var fileName = _marketSymbolsFile ?? (_marketSymbolsFile = _marketSymbolDownloader.GetMarketSymbolsFile(downloadPath, marketSymbolsArchiveUrl));
            return _marketSymbolReader.GetMarketSymbols(fileName);
        }
    }
}