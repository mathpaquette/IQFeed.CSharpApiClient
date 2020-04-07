using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.Historical.Facades;
using IQFeed.CSharpApiClient.Lookup.Historical.Handlers;
using IQFeed.CSharpApiClient.Lookup.MarketSummary;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup
{
    public static class LookupClientFactory
    {
        public static LookupClient<T> CreateNew<T>(
            string host,
            int port,
            int numberOfClients,
            TimeSpan timeout,
            int bufferSize,
            IHistoricalMessageHandler<T> historicalMessageHandler) where T : struct
        {
            // Common
            var requestFormatter = new RequestFormatter();
            var lookupDispatcher = new LookupDispatcher(host, port, bufferSize, IQFeedDefault.ProtocolVersion, numberOfClients, requestFormatter);
            var exceptionFactory = new ExceptionFactory();
            var lookupMessageFileHandler = new LookupMessageFileHandler(lookupDispatcher, exceptionFactory, timeout);

            // Historical
            var historicalDataRequestFormatter = new HistoricalRequestFormatter();
            var historicalFileFacade = new HistoricalFileFacade(historicalDataRequestFormatter, lookupMessageFileHandler);
            var historicalFacade = new HistoricalFacade<T>(
                historicalDataRequestFormatter,
                lookupDispatcher,
                exceptionFactory,
                historicalMessageHandler,
                historicalFileFacade,
                timeout
            );

            // News
            var newsFacade = new NewsFacade();

            // Symbol
            var symbolFacade = new SymbolFacade(
                new SymbolRequestFormatter(),
                lookupDispatcher,
                exceptionFactory,
                new SymbolMessageHandler(),
                new MarketSymbolDownloader(),
                new MarketSymbolReader(),
                new ExpiredOptionDownloader(),
                new ExpiredOptionReader(),
                timeout);

            // Chains
            var chainsFacade = new ChainsFacade(new ChainsRequestFormatter(), new ChainsMessageHandler(), lookupDispatcher, exceptionFactory, timeout);

            // MarketSummary
            var marketSummaryFacade = new MarketSummaryFacade<T>(new MarketSummaryRequestFormatter(), lookupDispatcher, exceptionFactory, timeout);

            return new LookupClient<T>(lookupDispatcher, historicalFacade, newsFacade, symbolFacade, chainsFacade, marketSummaryFacade);
        }

        public static LookupClient<double> CreateNew()
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                1,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                new HistoricalMessageDoubleHandler());
        }

        public static LookupClient<double> CreateNew(string host, int port)
        {
            return CreateNew(
                host,
                port,
                1,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                new HistoricalMessageDoubleHandler());
        }

        public static LookupClient<double> CreateNew(string host, int port, int numberOfClients, TimeSpan timeout)
        {
            return CreateNew(
                host,
                port,
                numberOfClients,
                timeout,
                LookupDefault.BufferSize,
                new HistoricalMessageDoubleHandler());
        }

        public static LookupClient<double> CreateNew(int numberOfClients)
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                numberOfClients,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                new HistoricalMessageDoubleHandler());
        }
    }
}