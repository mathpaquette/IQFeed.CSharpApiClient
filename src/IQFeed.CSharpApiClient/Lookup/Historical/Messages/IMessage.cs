using System;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Messages 
{
    public interface IMessage 
    {
        DateTime Timestamp { get; }
    }
}