using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1SnapshotSync<T>
    {
        FundamentalMessage GetFundamentalSnapshot(string symbol);
        UpdateSummaryMessage<T> GetUpdateSummarySnapshot(string symbol);
    }
}