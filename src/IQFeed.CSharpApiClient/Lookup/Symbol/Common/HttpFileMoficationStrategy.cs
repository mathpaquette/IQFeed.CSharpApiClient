using System;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Common
{
    public class HttpFileMoficationStrategy : IFileModificationStrategy
    {
        public DateTime GetLastModificationTimestamp(string url)
        {
            try
            {
                var webRequest = System.Net.WebRequest.Create(url);
                webRequest.Method = "HEAD";
                using (var resp = webRequest.GetResponse())
                {

                    if (DateTime.TryParse(resp.Headers.Get("Last-Modified"), out var lastModified))
                        return lastModified;
                }
            }
            catch (Exception) { }
            return DateTime.MinValue;
        }
    }
}