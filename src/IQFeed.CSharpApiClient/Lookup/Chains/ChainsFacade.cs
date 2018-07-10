using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsFacade : BaseLookupFacade, IChainsFacade
    {
        private readonly ChainsRequestFormatter _chainsRequestFormatter;
        private readonly ChainsMessageHandler _chainsMessageHandler;

        public ChainsFacade(ChainsRequestFormatter chainsRequestFormatter, ChainsMessageHandler chainsMessageHandler, LookupDispatcher lookupDispatcher, ErrorMessageHandler errorMessageHandler, int timeoutMs) : base(lookupDispatcher, errorMessageHandler, timeoutMs)
        {
            _chainsMessageHandler = chainsMessageHandler;
            _chainsRequestFormatter = chainsRequestFormatter;
        }

        public Task<IEnumerable<FutureMessage>> ReqChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFuture(symbol, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureMessages);
        }

        public Task<IEnumerable<FutureSpreadMessage>> ReqChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureSpreads(symbol, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureSpreadMessages);
        }

        public Task<IEnumerable<FutureOptionMessage>> ReqChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureOption(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureOptionMessages);
        }

        public Task<IEnumerable<EquityOptionMessage>> ReqChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, BinaryOptionFilterType binaryOptionFilter = BinaryOptionFilterType.Include,
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainIndexEquityOption(symbol, optionSideFilter, monthCodes, nearMonths, binaryOptionFilter, optionFilter, filterValue1, filterValue2, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetEquityOptionMessages);
        }
    }
}