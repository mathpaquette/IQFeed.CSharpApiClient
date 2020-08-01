using System;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Downloader
{
    public interface ILocalCacheStrategy
    {
        bool HasExpired(string path, TimeSpan expiration);
    }
}