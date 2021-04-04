using System.Net.PeerToPeer.Collaboration;

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

        // Protocol Update to 6.1 - Added "includeNonStandardOptions" - IQ Default is false
        public string ReqChainIndexEquityOption(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, 
        OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null, bool includeNonStandardOptions = false)
        {
            var wireFormatIncludeNonStandardOptions = includeNonStandardOptions ? 1 : 0;
            var request = $"CEO,{symbol},{optionSideFilter.ToString().ToLower()},{monthCodes},{nearMonths},{(int)optionFilter}," +
                          $"{filterValue1},{filterValue2},{requestId},{wireFormatIncludeNonStandardOptions}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }
    }
}