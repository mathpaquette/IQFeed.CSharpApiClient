using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Snapshot
    {
        Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol);
        Task<UpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol);
    }
}