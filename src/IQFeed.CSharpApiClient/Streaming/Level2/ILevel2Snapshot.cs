using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public interface ILevel2Snapshot
    {
        Task<IEnumerable<UpdateSummaryMessage>> GetSummarySnapshotAsync(string symbol);
    }
}