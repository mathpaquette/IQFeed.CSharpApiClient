using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic
{
    public interface ILevel1DynamicEvent : ILevel1EventCommon
    {
        event Action<IUpdateSummaryDynamicMessage> Summary;
        event Action<IUpdateSummaryDynamicMessage> Update;
    }
}