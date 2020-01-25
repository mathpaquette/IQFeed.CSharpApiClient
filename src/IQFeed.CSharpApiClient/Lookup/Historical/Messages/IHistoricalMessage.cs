using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages 
{
    public interface IHistoricalMessage 
    {
        string RequestId { get; }
        DateTime Timestamp { get; }
    }
}