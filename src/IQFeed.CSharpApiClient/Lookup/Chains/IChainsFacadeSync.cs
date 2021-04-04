using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public interface IChainsFacadeSync
    {
        IEnumerable<Future> GetChainFuture(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        IEnumerable<FutureSpread> GetChainFutureSpreads(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        IEnumerable<FutureOption> GetChainFutureOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null);

        IEnumerable<EquityOption> GetChainIndexEquityOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null);
    }
}