using IQFeed.CSharpApiClient.Lookup.Chains.Options;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsRequestFormatter
    {
        public string ReqChainFuture(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = $"CFU,{symbol.ToUpper()},{monthCodes.ToUpper()},{years},{nearMonths},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqChainFutureSpreads(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = $"CFS,{symbol.ToUpper()},{monthCodes},{years},{nearMonths},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqChainFutureOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = $"CFO,{symbol.ToUpper()},{optionSideFilter.ToString().ToLower()},{monthCodes},{years},{nearMonths},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqChainIndexEquityOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, BinaryOptionFilterType binaryOptionFilter = BinaryOptionFilterType.Include,
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null)
        {
            var request = $"CEO,{symbol},{optionSideFilter.ToString().ToLower()},{monthCodes},{nearMonths},{(int)binaryOptionFilter},{(int)optionFilter},{filterValue1},{filterValue2},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }
    }
}