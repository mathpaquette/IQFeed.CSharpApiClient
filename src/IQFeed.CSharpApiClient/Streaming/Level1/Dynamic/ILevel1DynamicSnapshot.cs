using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public interface ILevel1DynamicSnapshot : ILevel1DynamicSnapshotSync
    {
        Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol);
        Task<IUpdateSummaryDynamicMessage> GetUpdateSummarySnapshotAsync(string symbol);
    }
}