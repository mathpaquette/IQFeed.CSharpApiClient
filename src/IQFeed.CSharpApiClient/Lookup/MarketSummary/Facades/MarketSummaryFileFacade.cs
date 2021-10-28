using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades
{
    public class MarketSummaryFileFacade : IMarketSummaryFacade<string, string, string>
    {
        private readonly MarketSummaryRequestFormatter _marketSummaryRequestFormatter;
        private readonly LookupMessageFileHandler _lookupMessageFileHandler;

        public MarketSummaryFileFacade(
            MarketSummaryRequestFormatter marketSummaryRequestFormatter,
            LookupMessageFileHandler lookupMessageFileHandler)
        {
            _lookupMessageFileHandler = lookupMessageFileHandler;
            _marketSummaryRequestFormatter = marketSummaryRequestFormatter;
        }

        public Task<string> GetEndOfDaySummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.ReqEndOfDaySummary(securityType, listedMarketGroupId, date, requestId);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetEndOfDayFundamentalSummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.ReqFundamentalSummary(securityType, listedMarketGroupId, date, requestId);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> Get5MinuteSnapshotSummaryAsync(SecurityType securityType, int listedMarketGroupId, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.Req5MinuteSnapshotSummary(securityType, listedMarketGroupId, requestId);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }
    }
}