using System;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Common
{
    public interface IFileModificationStrategy
    {
        DateTime GetLastModificationTimestamp(string url);
    }
}