using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades
{
    public interface IMarketSummaryFacade<TEndOfDayMessages, TEndOfDayFundamentalMessages, T5MinuteSnapshotMessages>
    {
        Task<TEndOfDayMessages> GetEndOfDaySummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null);
        Task<TEndOfDayFundamentalMessages> GetEndOfDayFundamentalSummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null);
        Task<T5MinuteSnapshotMessages> Get5MinuteSnapshotSummaryAsync(SecurityType securityType, int listedMarketGroupId, string requestId = null);
    }
}
