using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public interface ISymbolFacade<TListedMarketMessages, TTradeConditionMessages>
    {
        IEnumerable<MarketSymbol> GetAllMarketSymbols(string url = SymbolDefault.MarketSymbolsArchiveUrl, string downloadPath = null, bool useCache = true);
        IEnumerable<ExpiredOption> GetAllExpiredOptions(string url = SymbolDefault.ExpiredOptionsArchiveUrl, string downloadPath = null, bool useCache = true, bool header = false);

        /// <summary>
        /// SLM - Request a list of Listed Markets from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Listed market messages</returns>
        Task<TListedMarketMessages> ReqListedMarketsAsync(string requestId = null);

        /// <summary>
        /// STC - Request a list of Trade Conditions from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Trade condition messages</returns>
        Task<TTradeConditionMessages> ReqTradeConditionssAsync(string requestId = null);
    }
}