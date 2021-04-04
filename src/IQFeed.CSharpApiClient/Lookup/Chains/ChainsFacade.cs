using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsFacade : BaseLookupFacade, IChainsFacade
    {
        private readonly ChainsRequestFormatter _chainsRequestFormatter;
        private readonly ChainsMessageHandler _chainsMessageHandler;

        public ChainsFacade(
            ChainsRequestFormatter chainsRequestFormatter,
            ChainsMessageHandler chainsMessageHandler,
            LookupDispatcher lookupDispatcher,
            LookupRateLimiter lookupRateLimiter,
            ExceptionFactory exceptionFactory,
            TimeSpan timeout) : base(lookupDispatcher, lookupRateLimiter, exceptionFactory, timeout)
        {
            _chainsMessageHandler = chainsMessageHandler;
            _chainsRequestFormatter = chainsRequestFormatter;
        }

        public async Task<IEnumerable<Future>> GetChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            if (!string.IsNullOrEmpty(requestId))
                throw new NotSupportedException("RequestId parsing isn't supported for Chains!");

            var request = _chainsRequestFormatter.ReqChainFuture(symbol, monthCodes, years, nearMonths, requestId);
            var messages = await GetMessagesAsync(request, _chainsMessageHandler.GetFutureMessages).ConfigureAwait(false);
            return messages.First().Chains;
        }

        public async Task<IEnumerable<FutureSpread>> GetChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            if (!string.IsNullOrEmpty(requestId))
                throw new NotSupportedException("RequestId parsing isn't supported for Chains!");

            var request = _chainsRequestFormatter.ReqChainFutureSpreads(symbol, monthCodes, years, nearMonths, requestId);
            var messages = await GetMessagesAsync(request, _chainsMessageHandler.GetFutureSpreadMessages).ConfigureAwait(false);
            return messages.First().Chains;
        }

        public async Task<IEnumerable<FutureOption>> GetChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            if (!string.IsNullOrEmpty(requestId))
                throw new NotSupportedException("RequestId parsing isn't supported for Chains!");

            var request = _chainsRequestFormatter.ReqChainFutureOption(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId);
            var messages = await GetMessagesAsync(request, _chainsMessageHandler.GetFutureOptionMessages).ConfigureAwait(false);
            return messages.First().Chains;
        }

        // Protocol Update to 6.1 - Added "includeNonStandardOptions" - default to true to maintain backwards compatibility - IQ Default is false
        public async Task<IEnumerable<EquityOption>> GetChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, 
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null, bool includeNonStandardOptions = true)
        {
            if (!string.IsNullOrEmpty(requestId))
                throw new NotSupportedException("RequestId parsing isn't supported for Chains!");

            var request = _chainsRequestFormatter.ReqChainIndexEquityOption(
                symbol, optionSideFilter, monthCodes, nearMonths, optionFilter, filterValue1, filterValue2, requestId, includeNonStandardOptions);
            var messages = await GetMessagesAsync(request, _chainsMessageHandler.GetEquityOptionMessages).ConfigureAwait(false);
            return messages.First().Chains;
        }

        public IEnumerable<Future> GetChainFuture(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            return GetChainFutureAsync(symbol, monthCodes, years, nearMonths, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<FutureSpread> GetChainFutureSpreads(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            return GetChainFutureSpreadsAsync(symbol, monthCodes, years, nearMonths, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<FutureOption> GetChainFutureOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            return GetChainFutureOptionAsync(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<EquityOption> GetChainIndexEquityOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes,
            int? nearMonths = null, OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null,
            string requestId = null)
        {
            return GetChainIndexEquityOptionAsync(symbol, optionSideFilter, monthCodes, nearMonths, optionFilter, filterValue1, filterValue2, requestId).SynchronouslyAwaitTaskResult();
        }
    }
}