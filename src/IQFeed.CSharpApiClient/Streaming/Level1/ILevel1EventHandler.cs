using System;
using IQFeed.CSharpApiClient.Streaming.Level1.EventArgs;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public interface ILevel1EventHandler
    {
        event EventHandler<FundamentalEventArgs> Fundamental;
        event EventHandler<UpdateSummaryEventArgs> Summary;
        event EventHandler<SystemEventArgs> System;
        event EventHandler<SymbolNotFoundEventArgs> SymbolNotFound;
        event EventHandler<ErrorEventArgs> Error;
        event EventHandler<TimestampEventArgs> Timestamp;
        event EventHandler<UpdateSummaryEventArgs> Update;
        event EventHandler<RegionalUpdateEventArgs> Regional;
        event EventHandler<NewsEventArgs> News;
    }
}