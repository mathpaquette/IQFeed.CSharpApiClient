using IQFeed.CSharpApiClient.Common.Interfaces;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;

namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupClient: IClient
    {
        private readonly LookupDispatcher _lookupDispatcher;

        public LookupClient(LookupDispatcher lookupDispatcher, HistoricalFacade historical, NewsFacade news, SymbolFacade symbol, ChainsFacade chains)
        {
            _lookupDispatcher = lookupDispatcher;
            Historical = historical;
            News = news;
            Symbol = symbol;
            Chains = chains;
        }

        public HistoricalFacade Historical { get; }
        public NewsFacade News { get; }
        public SymbolFacade Symbol { get; }
        public ChainsFacade Chains { get; }

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