using IQFeed.CSharpApiClient.Lookup.Symbol.Common;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols
{
    public class MarketSymbolDownloader : FileDownloaderBase
    {
        public MarketSymbolDownloader() : base(new HttpFileMoficationStrategy()) { }
    }
}