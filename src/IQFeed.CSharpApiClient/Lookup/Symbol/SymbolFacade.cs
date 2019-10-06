using System;
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

        public Task<IEnumerable<SymbolByFilterMessage>> ReqSymbolsByFilterAsync(FieldToSearch fieldToSearch, string searchString, FilterType? filterType, IEnumerable<int> filterValues, string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqSymbolsByFilter(fieldToSearch, searchString, filterType, filterValues, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByFilterMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByFilterMessagesWithRequestId);
        }

        public Task<IEnumerable<SymbolBySicCodeMessage>> ReqSymbolsBySicCodeAsync(string sicCodePrefix, string requestId = null)
        {
            if(sicCodePrefix == null) throw new ArgumentNullException(nameof(sicCodePrefix));
            if(sicCodePrefix.Length < 2) throw new ArgumentException("Value should have at least 2 characters!", nameof(sicCodePrefix));
            var request = _symbolRequestFormatter.ReqSymbolsBySicCode(sicCodePrefix, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSymbolBySicCodeMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSymbolBySicCodeMessagesWithRequestId);
        }

        public Task<IEnumerable<SymbolByNaicsCodeMessage>> ReqSymbolsByNaicsCodeAsync(string naicsCodePrefix, string requestId = null)
        {
            if(naicsCodePrefix == null) throw new ArgumentNullException(nameof(naicsCodePrefix));
            if(naicsCodePrefix.Length < 2) throw new ArgumentException("Value should have at least 2 characters!", nameof(naicsCodePrefix));
            var request = _symbolRequestFormatter.ReqSymbolsByNaicsCode(naicsCodePrefix, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByNaicsCodeMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSymbolByNaicsCodeMessagesWithRequestId);
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

        public Task<IEnumerable<SicCodeInfoMessage>> ReqSicCodesAsync(string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqSicCodes(requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetSicCodeInfoMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetSicCodeInfoMessagesWithRequestId);
        }

        public Task<IEnumerable<NaicsCodeInfoMessage>> ReqNaicsCodesAsync(string requestId = null)
        {
            var request = _symbolRequestFormatter.ReqNaicsCodes(requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _symbolMessageHandler.GetNaicsCodeInfoMessages)
                : GetMessagesAsync(request, _symbolMessageHandler.GetNaicsCodeInfoMessagesWithRequestId);
        }
    }
}