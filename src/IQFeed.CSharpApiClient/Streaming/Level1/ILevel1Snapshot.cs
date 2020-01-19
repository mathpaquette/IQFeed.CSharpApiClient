using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Snapshot<T>
    {
        Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol);
        Task<UpdateSummaryMessage<T>> GetUpdateSummarySnapshotAsync(string symbol);
    }
}