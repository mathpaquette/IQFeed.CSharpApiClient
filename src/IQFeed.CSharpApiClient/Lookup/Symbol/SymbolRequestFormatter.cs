using System.Collections.Generic;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol.Enums;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolRequestFormatter : RequestFormatter
    {
        public string ReqSymbolsByFilter(FieldToSearch fieldToSearch, string searchString, FilterType? filterType, IEnumerable<int> filterValues, string requestId = null)
        {
            // SBF,[Field To Search],[Search String],[Filter Type],[Filter Value],[RequestID]<CR><LF>
            var fieldToSearchFormat = ((char)fieldToSearch).ToString();
            var filterTypeFormat = filterType.HasValue ? ((char)filterType).ToString() : string.Empty;
            var filterValuesFormat = filterType.HasValue ? string.Join(" ", filterValues) : string.Empty;
            var request = $"SBF,{fieldToSearchFormat},{searchString},{filterTypeFormat},{filterValuesFormat}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqListedMarkets(string requestId = null)
        {
            // SLM,[RequestID]<CR><LF> 
            var request = $"SLM,{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqSecurityTypes(string requestId = null)
        {
            // SST,[RequestID]<CR><LF> 
            var request = $"SST,{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqTradeConditions(string requestId = null)
        {
            // STC,[RequestID]<CR><LF> 
            var request = $"STC,{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }
    }
}