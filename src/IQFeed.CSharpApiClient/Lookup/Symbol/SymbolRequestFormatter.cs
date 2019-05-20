using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolRequestFormatter : RequestFormatter
    {
        public string ReqListedMarkets(string requestId = null)
        {
            // SLM,[RequestID]<CR><LF> 
            var request = $"SLM,{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
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