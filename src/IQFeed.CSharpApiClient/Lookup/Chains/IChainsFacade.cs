using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public interface IChainsFacade : IChainsFacadeSync
    {
        /// <summary>
        /// CFU - Request a Future Chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="monthCodes"></param>
        /// <param name="years"></param>
        /// <param name="nearMonths"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<IEnumerable<Future>> GetChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        /// <summary>
        /// CFS - Request a Future Spread chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="monthCodes"></param>
        /// <param name="years"></param>
        /// <param name="nearMonths"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<IEnumerable<FutureSpread>> GetChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        /// <summary>
        /// CFO - Request a Future Option Chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="optionSideFilter"></param>
        /// <param name="monthCodes"></param>
        /// <param name="years"></param>
        /// <param name="nearMonths"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<IEnumerable<FutureOption>> GetChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        /// <summary>
        /// CEO - Request an Index or Equity Option Chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="optionSideFilter"></param>
        /// <param name="monthCodes"></param>
        /// <param name="nearMonths"></param>
        /// <param name="optionFilter"></param>
        /// <param name="filterValue1"></param>
        /// <param name="filterValue2"></param>
        /// <param name="requestId"></param>
        /// <param name="includeNonStandardOptions"></param>
        /// <returns></returns>
        Task<IEnumerable<EquityOption>> GetChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, 
            OptionFilterType optionFilter = OptionFilterType.None, 
            int? filterValue1 = null, int? filterValue2 = null, string requestId = null, bool includeNonStandardOptions = true);
    }
}