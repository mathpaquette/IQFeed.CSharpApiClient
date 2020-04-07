using IQFeed.CSharpApiClient.Common.Interfaces;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.Historical.Facades;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;

namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupClient<T> : IClient where T : struct
    {
        private readonly LookupDispatcher _lookupDispatcher;

        public LookupClient(
            LookupDispatcher lookupDispatcher,
            HistoricalFacade<T> historical,
            NewsFacade news,
            SymbolFacade symbol,
            ChainsFacade chains,
            MarketSummaryFacade<T> marketSummary)
        {
            _lookupDispatcher = lookupDispatcher;
            Historical = historical;
            News = news;
            Symbol = symbol;
            Chains = chains;
            MarketSummary = marketSummary;
        }

        public HistoricalFacade<T> Historical { get; }
        public NewsFacade News { get; }
        public SymbolFacade Symbol { get; }
        public ChainsFacade Chains { get; }
        public MarketSummaryFacade<T> MarketSummary { get; }

        public void Connect()
        {
            _lookupDispatcher.ConnectAll();
        }

        public void Disconnect()
        {
            _lookupDispatcher.DisconnectAll();
        }
    }
}