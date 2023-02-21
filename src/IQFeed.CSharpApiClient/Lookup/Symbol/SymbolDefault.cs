namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public class SymbolDefault
    {
        public const string MarketSymbolsArchiveUrl = "http://www.dtniq.com/product/mktsymbols_v2.zip";
 
        public const string ExpiredOptionsArchiveUrl = "http://www.iqfeed.net/downloads/beta/IEOPTION.zip";
        
        /// <summary>
        /// We can't check the length of values on most of the messages in this group,
        /// as some description fields can contain commas (giving us a variable number of
        /// fields, so we need to look for this DataId in the correct field instead.
        /// </summary>
        public const string SymbolsDataId = "LS";
    }
}