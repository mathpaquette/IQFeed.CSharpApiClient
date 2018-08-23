using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages 
{
    public interface IHistoricalMessage 
    {
        DateTime Timestamp { get; }
    }
}