using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolFacade : BaseLookupFacade, ISymbolFacade
    {
        private readonly SymbolRequestFormatter _symbolRequestFormatter;
        private readonly SymbolMessageHandler _symbolMessageHandler;

        private readonly MarketSymbolDownloader _marketSymbolDownloader;
        private readonly MarketSymbolReader _marketSymbolReader;
        private readonly ExpiredOptionDownloader _expiredOptionDownloader;
        private readonly ExpiredOptionReader _expiredOptionReader;

        public SymbolFacade(
            SymbolRequestFormatter symbolRequestFormatter,
            LookupDispatcher lookupDispatcher,
            ErrorMessageHandler errorMessageHandler,
            SymbolMessageHandler symbolMessageHandler,
            MarketSymbolDownloader marketSymbolDownloader, 
            MarketSymbolReader marketSymbolReader,
            ExpiredOptionDownloader expiredOptionDownloader,
            ExpiredOptionReader expiredOptionReader,
            int timeoutMs) : base(lookupDispatcher, errorMessageHandler, timeoutMs)
        {
            _symbolRequestFormatter = symbolRequestFormatter;
            _symbolMessageHandler = symbolMessageHandler;
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

        /// <summary>
        /// SBF - Request the Symbols By Filter
        /// </summary>
        /// <param name="fieldToSearch">Field to perform search on (either Symbols or Descriptions)</param>
        /// <param name="searchString">Search value</param>
        /// <param name="filterType">Optional filter type (ListedMarket or SecurityType)</param>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Symbol By Filter messages</returns>
        public Task<IEnumerable<SymbolByFilterMessage>> ReqSymbolsByFilterAsync(FieldToSearch fieldToSearch, string searchString, FilterType? filterType, IEnumerable<int> filterValues, string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqSymbolsByFilter(fieldToSearch, searchString, filterType, filterValues, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByFilterMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByFilterMessagesWithRequestId);
        }

        public Task<IEnumerable<ListedMarketMessage>> ReqListedMarketsAsync(string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqListedMarkets(requestId);
            return string.IsNullOrEmpty(requestId) 
                ? GetMessagesAsync(request, _symbolMessageHandler.GetListedMarketMessages) 
                : GetMessagesAsync(request, _symbolMessageHandler.GetListedMarketMessagesWithRequestId);
        }

        public Task<IEnumerable<SecurityTypeMessage>> ReqSecurityTypesAsync(string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqSecurityTypes(requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSecurityTypeMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSecurityTypeMessagesWithRequestId);
        }

        public Task<IEnumerable<TradeConditionMessage>> ReqTradeConditionsAsync(string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqTradeConditions(requestId);
            return string.IsNullOrEmpty(requestId) 
                ? GetMessagesAsync(request, _symbolMessageHandler.GetTradeConditionMessages) 
                : GetMessagesAsync(request, _symbolMessageHandler.GetTradeConditionMessagesWithRequestId);
        }
    }
}