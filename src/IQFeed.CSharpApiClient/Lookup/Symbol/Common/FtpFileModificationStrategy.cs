using System;
using System.Net;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Common
{
    public class FtpFileModificationStrategy : IFileModificationStrategy
    {
        public DateTime GetLastModificationTimestamp(string url)
        {
            try
            {
                var request = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
                request.Proxy = null;
                request.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    return response.LastModified;
                }

            }
            catch (Exception) { }
            return DateTime.MinValue;
        }
    }
}