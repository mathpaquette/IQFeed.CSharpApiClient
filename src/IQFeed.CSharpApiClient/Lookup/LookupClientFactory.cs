using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical;
using IQFeed.CSharpApiClient.Lookup.Historical.Facades;
using IQFeed.CSharpApiClient.Lookup.MarketSummary;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using IQFeed.CSharpApiClient.Lookup.Symbol.Downloader;
using IQFeed.CSharpApiClient.Lookup.Symbol.ExpiredOptions;
using IQFeed.CSharpApiClient.Lookup.Symbol.MarketSymbols;

namespace IQFeed.CSharpApiClient.Lookup
{
    public static class LookupClientFactory
    {
        public static LookupClient CreateNew(
            string host,
            int port,
            int numberOfClients,
            TimeSpan timeout,
            int bufferSize,
            int requestsPerSecond)
        {
            // Common
            var requestFormatter = new RequestFormatter();
            var lookupDispatcher = new LookupDispatcher(host, port, bufferSize, IQFeedDefault.ProtocolVersion, numberOfClients, requestFormatter);
            var lookupRateLimiter = new LookupRateLimiter(requestsPerSecond);
            var exceptionFactory = new ExceptionFactory();
            var lookupMessageFileHandler = new LookupMessageFileHandler(lookupDispatcher, lookupRateLimiter, exceptionFactory, timeout);
            var historicalMessageHandler = new HistoricalMessageHandler();

            // Historical
            var historicalDataRequestFormatter = new HistoricalRequestFormatter();
            var historicalFileFacade = new HistoricalFileFacade(historicalDataRequestFormatter, lookupMessageFileHandler);
            var historicalFacade = new HistoricalFacade(
                historicalDataRequestFormatter,
                lookupDispatcher,
                lookupRateLimiter,
                exceptionFactory,
                historicalMessageHandler,
                historicalFileFacade,
                timeout
            );

            // News
            var newsFacade = new NewsFacade(
                new NewsRequestFormatter(),
                lookupDispatcher,
                lookupRateLimiter,
                exceptionFactory,
                new NewsMessageHandler(),
                timeout);

            // Symbol
            var symbolFacade = new SymbolFacade(
                new SymbolRequestFormatter(),
                lookupDispatcher,
                lookupRateLimiter,
                exceptionFactory,
                new SymbolMessageHandler(),
                new MarketSymbolReader(),
                new ExpiredOptionReader(),
                new FileDownloader(new LocalCacheStrategy()),
                timeout);

            // Chains
            var chainsFacade = new ChainsFacade(
                new ChainsRequestFormatter(),
                new ChainsMessageHandler(),
                lookupDispatcher,
                lookupRateLimiter,
                exceptionFactory,
                timeout);

            // MarketSummary
            var marketSummaryFacade = new MarketSummaryFacade(new MarketSummaryRequestFormatter(), lookupDispatcher, lookupRateLimiter, exceptionFactory, timeout);

            return new LookupClient(lookupDispatcher, historicalFacade, newsFacade, symbolFacade, chainsFacade, marketSummaryFacade);
        }

        public static LookupClient CreateNew()
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                1,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                LookupDefault.RequestsPerSecond);
        }

        public static LookupClient CreateNew(int numberOfClients)
        {
            return CreateNew(
                IQFeedDefault.Hostname,
                IQFeedDefault.LookupPort,
                numberOfClients,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                LookupDefault.RequestsPerSecond);
        }

        public static LookupClient CreateNew(string host, int port)
        {
            return CreateNew(
                host,
                port,
                1,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                LookupDefault.RequestsPerSecond);
        }

        public static LookupClient CreateNew(string host, int port, int numberOfClients)
        {
            return CreateNew(
                host,
                port,
                numberOfClients,
                LookupDefault.Timeout,
                LookupDefault.BufferSize,
                LookupDefault.RequestsPerSecond);
        }

        public static LookupClient CreateNew(string host, int port, int numberOfClients, TimeSpan timeout)
        {
            return CreateNew(
                host,
                port,
                numberOfClients,
                timeout,
                LookupDefault.BufferSize,
                LookupDefault.RequestsPerSecond);
        }
    }
}