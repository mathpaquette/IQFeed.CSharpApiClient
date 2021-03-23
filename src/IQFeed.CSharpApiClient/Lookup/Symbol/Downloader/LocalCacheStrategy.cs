using System;
using System.IO;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Downloader
{
    public class LocalCacheStrategy : ILocalCacheStrategy
    {
        public bool HasExpired(string path, TimeSpan expiration)
        {
            if (!File.Exists(path))
                return true;
            
            return DateTime.Now > File.GetCreationTime(path) + expiration;
        }
    }
}