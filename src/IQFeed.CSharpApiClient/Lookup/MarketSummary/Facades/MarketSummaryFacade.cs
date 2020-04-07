﻿using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Facades
{
    public class MarketSummaryFacade<T> : BaseLookupFacade, IMarketSummaryFacade<T> where T : struct
    {
        private readonly MarketSummaryRequestFormatter _marketSummaryRequestFormatter;

        public MarketSummaryFacade(
            MarketSummaryRequestFormatter marketSummaryRequestFormatter,
            LookupDispatcher lookupDispatcher,
            ExceptionFactory exceptionFactory,
            TimeSpan timeout) : base(lookupDispatcher, exceptionFactory, timeout)
        {
            _marketSummaryRequestFormatter = marketSummaryRequestFormatter;
        }

        public Task<IEnumerable<MarketSummaryMessage<T>>> GetEndOfDaySummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.ReqEndOfDaySummary(securityType, listedMarketGroupId, date, requestId);
            var marketSummaryHandler = new MarketSummaryHandler<T>();
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessages) : GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessagesWithRequestId);
        }

        public Task<IEnumerable<MarketSummaryMessage<T>>> GetEndOfDayFundamentalSummaryAsync(SecurityType securityType, int listedMarketGroupId, DateTime date, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.ReqFundamentalSummary(securityType, listedMarketGroupId, date, requestId);
            var marketSummaryHandler = new MarketSummaryHandler<T>();
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessages) : GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessagesWithRequestId);
        }

        public Task<IEnumerable<MarketSummaryMessage<T>>> Get5MinuteSnapshotSummaryAsync(SecurityType securityType, int listedMarketGroupId, string requestId = null)
        {
            var request = _marketSummaryRequestFormatter.Req5MinuteSnapshotSummary(securityType, listedMarketGroupId, requestId);
            var marketSummaryHandler = new MarketSummaryHandler<T>();
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessages) : GetMessagesAsync(request, marketSummaryHandler.GetMarketSummaryMessagesWithRequestId);
        }
    }
}