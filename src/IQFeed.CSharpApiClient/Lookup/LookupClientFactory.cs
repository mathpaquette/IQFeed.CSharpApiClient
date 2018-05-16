using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;

namespace IQFeed.CSharpApiClient.Lookup
{
    public static class LookupClientFactory
    {
        public static LookupClient CreateNew(string host = IQFeedDefault.Hostname, int port = IQFeedDefault.HistoricalPort, int numberOfClients = 1)
        {
            // Common
            var requestFormatter = new RequestFormatter();
            var lookupDispatcher = new LookupDispatcher(host, port, IQFeedDefault.ProtocolVersion, numberOfClients, requestFormatter);
            var rawMessageHandler = new RawMessageHandler(lookupDispatcher, 60 * 1000);

            // Historical
            var historicalDataRequestFormatter = new HistoricalRequestFormatter();
            var historicalRawFace = new HistoricalRawFacade(historicalDataRequestFormatter, rawMessageHandler);
            var historicalFacade = new HistoricalFacade(
                historicalDataRequestFormatter,
                lookupDispatcher, 
                new HistoricalMessageHandler(),
                historicalRawFace
            );

            // News
            var newsFacade = new NewsFacade();

            // Symbol
            var symbolFacade = new SymbolFacade();

            // Chains
            var chainsFacade = new ChainsFacade();

            return new LookupClient(lookupDispatcher, historicalFacade, newsFacade, symbolFacade, chainsFacade);
        }

        public static LookupClient CreateNew(int numberOfClients)
        {
            return CreateNew(IQFeedDefault.Hostname, IQFeedDefault.HistoricalPort, numberOfClients);
        }
    }
}