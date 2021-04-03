using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public interface ILevel1DynamicSnapshotSync
    {
        FundamentalMessage GetFundamentalSnapshot(string symbol);
        IUpdateSummaryDynamicMessage GetUpdateSummarySnapshot(string symbol);
    }
}