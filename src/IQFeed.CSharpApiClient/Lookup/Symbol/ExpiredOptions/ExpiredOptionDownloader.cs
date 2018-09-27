using IQFeed.CSharpApiClient.Lookup.Symbol.Common;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions
{
    public class ExpiredOptionDownloader : FileDownloaderBase
    {
        public ExpiredOptionDownloader() : base(new FtpFileModificationStrategy()) { }
    }
}