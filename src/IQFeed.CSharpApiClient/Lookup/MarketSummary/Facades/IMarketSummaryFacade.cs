using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades
{
    public interface IMarketSummaryFacade
    {
        Task<IEnumerable<MarketSummaryMessage>> GetEndOfDaySummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null);
        Task<IEnumerable<MarketSummaryMessage>> GetEndOfDayFundamentalSummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null);
        Task<IEnumerable<MarketSummaryMessage>> Get5MinuteSnapshotSummaryAsync(SecurityType securityType, int listedMarketGroupId, string requestId = null);
    }
}
