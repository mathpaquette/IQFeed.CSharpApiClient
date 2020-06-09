using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1SnapshotSync
    {
        FundamentalMessage GetFundamentalSnapshot(string symbol);
        IUpdateSummaryMessage GetUpdateSummarySnapshot(string symbol);
    }
}