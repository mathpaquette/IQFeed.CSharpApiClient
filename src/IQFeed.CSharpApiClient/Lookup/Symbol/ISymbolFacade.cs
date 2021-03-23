using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;
using IQFeed.CSharpApiClient.Lookup.Symbol.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public interface ISymbolFacade : ISymbolFacadeDownload, ISymbolFacadeSync
    {
        /// <summary>
        /// SBF - Request the Symbols By Filter
        /// </summary>
        /// <param name="fieldToSearch">Field to perform search on (either Symbols or Descriptions)</param>
        /// <param name="searchString">Search value</param>
        /// <param name="filterType">Optional filter type (ListedMarket or SecurityType)</param>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Symbol By Filter messages</returns>
        Task<IEnumerable<SymbolByFilterMessage>> GetSymbolsByFilterAsync(FieldToSearch fieldToSearch, string searchString, FilterType? filterType, IEnumerable<int> filterValues, string requestId = null);

        /// <summary>
        /// SBS - Request the Symbols By SIC code
        /// </summary>
        /// <param name="sicCodePrefix">At least the first 2 digits of an existing SIC code</param>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Symbol By Sic Code messages</returns>
        Task<IEnumerable<SymbolBySicCodeMessage>> GetSymbolsBySicCodeAsync(string sicCodePrefix, string requestId = null);

        /// <summary>
        /// SBN - Request the Symbols By NAICS code
        /// </summary>
        /// <param name="naicsCodePrefix">At least the first 2 digits of an existing NAICS code</param>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Symbol By Naics Code messages</returns>
        Task<IEnumerable<SymbolByNaicsCodeMessage>> GetSymbolsByNaicsCodeAsync(string naicsCodePrefix, string requestId = null);

        /// <summary>
        /// SLM - Request a list of Listed Markets from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Listed market messages</returns>
        Task<IEnumerable<ListedMarketMessage>> GetListedMarketsAsync(string requestId = null);
        
        /// <summary>
        /// SST - Request a list of Security Types from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Security type messages</returns>
        Task<IEnumerable<SecurityTypeMessage>> GetSecurityTypesAsync(string requestId = null);

        /// <summary>
        /// STC - Request a list of Trade Conditions from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>Trade condition messages</returns>
        Task<IEnumerable<TradeConditionMessage>> GetTradeConditionsAsync(string requestId = null);

        /// <summary>
        /// SSC - Request a list of SIC Codes from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>SIC code info messages</returns>
        Task<IEnumerable<SicCodeInfoMessage>> GetSicCodesAsync(string requestId = null);

        /// <summary>
        /// SNC - Request a list of NAICS Codes from the feed.
        /// </summary>
        /// <param name="requestId">Optional request id</param>
        /// <returns>NAICS code info messages</returns>
        Task<IEnumerable<NaicsCodeInfoMessage>> GetNaicsCodesAsync(string requestId = null);
    }
}