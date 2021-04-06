using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Chains.Equities;
using IQFeed.CSharpApiClient.Lookup.Chains.Futures;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
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

        public async Task<IEnumerable<FutureMessage>> GetChainFutureMessagesAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFuture(symbol, monthCodes, years, nearMonths, requestId);
            return await (string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _chainsMessageHandler.GetFutureMessages) : GetMessagesAsync(request, _chainsMessageHandler.GetFutureMessagesWithRequestId)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Future>> GetChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var messages = await GetChainFutureMessagesAsync(symbol, monthCodes, years, nearMonths, requestId);
            return messages.First().Chains;
        }

        public async Task<IEnumerable<FutureSpreadMessage>> GetChainFutureSpreadMessagesAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureSpreads(symbol, monthCodes, years, nearMonths, requestId);
            return await (string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _chainsMessageHandler.GetFutureSpreadMessages) : GetMessagesAsync(request, _chainsMessageHandler.GetFutureSpreadMessagesWithRequestId)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<FutureSpread>> GetChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var messages = await GetChainFutureSpreadMessagesAsync(symbol, monthCodes, years, nearMonths, requestId);
            return messages.First().Chains;
        }

        public async Task<IEnumerable<FutureOptionMessage>> GetChainFutureOptionMessagesAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureOption(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId);
            return await (string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _chainsMessageHandler.GetFutureOptionMessages) : GetMessagesAsync(request, _chainsMessageHandler.GetFutureOptionMessagesWithRequestId)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<FutureOption>> GetChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var messages = await GetChainFutureOptionMessagesAsync(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId);
            return messages.First().Chains;
        }

        // Protocol Update to 6.1 - Added "includeNonStandardOptions" - default to true to maintain backwards compatibility - IQ Default is false
        public async Task<IEnumerable<EquityOptionMessage>> GetChainIndexEquityOptionMessagesAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null,
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null, bool includeNonStandardOptions = true)
        {
            var request = _chainsRequestFormatter.ReqChainIndexEquityOption(
                symbol, optionSideFilter, monthCodes, nearMonths, optionFilter, filterValue1, filterValue2, requestId, includeNonStandardOptions);
            return await (string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _chainsMessageHandler.GetEquityOptionMessages) : GetMessagesAsync(request, _chainsMessageHandler.GetEquityOptionMessagesWithRequestId)).ConfigureAwait(false);
        }

        // Protocol Update to 6.1 - Added "includeNonStandardOptions" - default to true to maintain backwards compatibility - IQ Default is false
        public async Task<IEnumerable<EquityOption>> GetChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, 
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null, bool includeNonStandardOptions = true)
        {
            var messages = await GetChainIndexEquityOptionMessagesAsync(symbol, optionSideFilter, monthCodes, nearMonths, optionFilter, filterValue1, filterValue2, requestId, includeNonStandardOptions);
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