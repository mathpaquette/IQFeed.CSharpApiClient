using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1Event : ILevel1EventCommon
    {
        event Action<IUpdateSummaryMessage> Summary;
        event Action<IUpdateSummaryMessage> Update;
    }
}