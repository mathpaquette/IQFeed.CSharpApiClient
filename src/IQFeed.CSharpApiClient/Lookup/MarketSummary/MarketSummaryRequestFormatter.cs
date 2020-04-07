using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Symbol;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary
{
    public class MarketSummaryRequestFormatter : RequestFormatter
    {
        public const string SourceDataDateFormat = "yyyyMMdd";

        public string ReqEndOfDaySummary(SecurityType securityType, int groupId, DateTime date, string requestId = null)
        {
            // EDS,[SecurityType],[GroupID],[Date],[RequestID]<CR><LF>
            var wireDate = date.ToString(SourceDataDateFormat);
            var request = $"EDS,{((int)securityType)},{groupId},{wireDate},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqFundamentalSummary(SecurityType securityType, int groupId, DateTime date, string requestId = null)
        {
            // FDS,[SecurityType],[GroupID],[Date],[RequestID]<CR><LF>
            var wireDate = date.ToString(SourceDataDateFormat);
            var request = $"FDS,{((int)securityType)},{groupId},{wireDate},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string Req5MinuteSnapshotSummary(SecurityType securityType, int groupId, string requestId = null)
        {
            // 5MS,[SecurityType],[GroupID],[Date],[RequestID]<CR><LF>
            var request = $"5MS,{((int)securityType)},{groupId},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }
    }
}