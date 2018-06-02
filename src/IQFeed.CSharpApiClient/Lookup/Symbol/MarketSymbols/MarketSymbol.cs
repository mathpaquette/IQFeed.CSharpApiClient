namespace IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols
{
    public class MarketSymbol
    {
        public string Symbol { get; }
        public string Description { get; }
        public string Exchange { get; }
        public string ListedMarket { get; }
        public string SecurityType { get; }
        public string Sic { get; }
        public string Frontmonth { get; }
        public string Naics { get; }

        public MarketSymbol(string symbol, string description, string exchange, string listedMarket, string securityType, string sic, string frontmonth, string naics)
        {
            Symbol = symbol;
            Description = description;
            Exchange = exchange;
            ListedMarket = listedMarket;
            SecurityType = securityType;
            Sic = sic;
            Frontmonth = frontmonth;
            Naics = naics;
        }
    }
}