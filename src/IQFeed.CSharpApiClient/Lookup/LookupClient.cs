using System;
using IQFeed.CSharpApiClient.Common.Interfaces;
using IQFeed.CSharpApiClient.Lookup.Chains;
using IQFeed.CSharpApiClient.Lookup.Historical.Facades;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades;
using IQFeed.CSharpApiClient.Lookup.News;
using IQFeed.CSharpApiClient.Lookup.Symbol;

namespace IQFeed.CSharpApiClient.Lookup
{
    public class LookupClient: IClient, IDisposable
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly LookupRateLimiter _lookupRateLimiter;
        private bool _disposed;

        public LookupClient(
            LookupDispatcher lookupDispatcher,
            HistoricalFacade historical,
            NewsFacade news,
            SymbolFacade symbol,
            ChainsFacade chains,
            MarketSummaryFacade marketSummary)
            : this(lookupDispatcher, historical, news, symbol, chains, marketSummary, null)
        {
        }

        /// <summary>
        /// Creates a client that owns <paramref name="lookupRateLimiter"/>: <see cref="Dispose()"/> disposes it, which stops its release loop.
        /// Do not share the rate limiter with another client, which would stop working when this one is disposed.
        /// </summary>
        public LookupClient(
            LookupDispatcher lookupDispatcher,
            HistoricalFacade historical,
            NewsFacade news,
            SymbolFacade symbol,
            ChainsFacade chains,
            MarketSummaryFacade marketSummary,
            LookupRateLimiter lookupRateLimiter)
        {
            _lookupDispatcher = lookupDispatcher;
            _lookupRateLimiter = lookupRateLimiter;
            Historical = historical;
            News = news;
            Symbol = symbol;
            Chains = chains;
            MarketSummary = marketSummary;
        }

        public HistoricalFacade Historical { get; }
        public NewsFacade News { get; }
        public SymbolFacade Symbol { get; }
        public ChainsFacade Chains { get; }
        public MarketSummaryFacade MarketSummary { get; }

        public void Connect()
        {
            _lookupDispatcher.ConnectAll();
        }

        public void Disconnect()
        {
            _lookupDispatcher.DisconnectAll();
        }

        /// <summary>
        /// Disconnects the client and disposes the rate limiter it owns, which <see cref="Disconnect"/> alone leaves running.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                Disconnect();
                _lookupRateLimiter?.Dispose();
            }

            _disposed = true;
        }
    }
}