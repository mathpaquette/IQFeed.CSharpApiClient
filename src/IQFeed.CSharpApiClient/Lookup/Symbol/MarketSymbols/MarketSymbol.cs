namespace IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols
{
    public class MarketSymbol
    {
        public string Symbol { get; private set; }
        public string Description { get; private set; }
        public string Exchange { get; private set; }
        public string ListedMarket { get; private set; }
        public string SecurityType { get; private set; }
        public string Sic { get; private set; }
        public string Frontmonth { get; private set; }
        public string Naics { get; private set; }

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

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(Description)}: {Description}, {nameof(Exchange)}: {Exchange}, {nameof(ListedMarket)}: {ListedMarket}, {nameof(SecurityType)}: {SecurityType}, {nameof(Sic)}: {Sic}, {nameof(Frontmonth)}: {Frontmonth}, {nameof(Naics)}: {Naics}";
        }
    }
}