using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public interface IChainsFacade
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
        Task<IEnumerable<string>> ReqChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);


        /// <summary>
        /// CFS - Request a Future Spread chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="monthCodes"></param>
        /// <param name="years"></param>
        /// <param name="nearMonths"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> ReqChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);

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
        Task<IEnumerable<string>> ReqChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        /// <summary>
        /// CEO - Request an Index or Equity Option Chain from IQFeed
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="optionSideFilter"></param>
        /// <param name="monthCodes"></param>
        /// <param name="nearMonths"></param>
        /// <param name="binaryOptionFilter"></param>
        /// <param name="optionFilter"></param>
        /// <param name="filterValue1"></param>
        /// <param name="filterValue2"></param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        Task<IEnumerable<EquityOptionMessage>> ReqChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, BinaryOptionFilterType binaryOptionFilter = BinaryOptionFilterType.Include, OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null);
    }
}