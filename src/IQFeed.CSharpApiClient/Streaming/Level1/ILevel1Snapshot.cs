using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Snapshot : ILevel1SnapshotSync
    {
        Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol);
        Task<IUpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol);
    }
}