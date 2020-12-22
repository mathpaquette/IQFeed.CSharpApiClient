using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.News.Enums;
using IQFeed.CSharpApiClient.Lookup.News.Messages;

namespace IQFeed.CSharpApiClient.Lookup.News
{
    public class NewsFacade : BaseLookupFacade, INewsFacade
    {
        private readonly NewsRequestFormatter _newsRequestFormatter;
        private readonly NewsMessageHandler _newsMessageHandler;

        public NewsFacade(
            NewsRequestFormatter newsRequestFormatter,
            LookupDispatcher lookupDispatcher,
            LookupRateLimiter lookupRateLimiter,
            ExceptionFactory exceptionFactory,
            NewsMessageHandler newsMessageHandler,
            TimeSpan timeout) : base(lookupDispatcher, lookupRateLimiter, exceptionFactory, timeout)
        {
            _newsMessageHandler = newsMessageHandler;
            _newsRequestFormatter = newsRequestFormatter;
        }

        public Task<IEnumerable<string>> GetNewsConfigurationAsync(FormatType formatType, string requestId = null)
        {
            var request = _newsRequestFormatter.ReqNewsConfiguration(formatType, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _newsMessageHandler.GetString)
                : GetMessagesAsync(request, _newsMessageHandler.GetStringWithRequestId);
        }

        public Task<IEnumerable<NewsHeadlinesMessage>> GetNewsHeadlinesAsync(string[] sources = null, string[] symbols = null, FormatType? formatType = null, int? limit = null, DateTime? date = null, string requestId = null)
        {
            if (formatType == FormatType.XML)
                throw new NotSupportedException("XML format is not supported.");

            var request = _newsRequestFormatter.ReqNewsHeadlines(sources, symbols, FormatType.Text, limit, date, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _newsMessageHandler.GetNewsHeadlines)
                : GetMessagesAsync(request, _newsMessageHandler.GetNewsHeadlinesWithRequestId);
        }

        public Task<IEnumerable<string>> GetNewsStoryAsync(string id, NewsFormatType? formatType = null, string deliverTo = null, string requestId = null)
        {
            var request = _newsRequestFormatter.ReqNewsStory(id, formatType, deliverTo, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _newsMessageHandler.GetString)
                : GetMessagesAsync(request, _newsMessageHandler.GetStringWithRequestId);
        }

        public Task<IEnumerable<string>> GetNewsStoryCountAsync(string[] symbols, FormatType? formatType = null, string[] sources = null,
            DateTime? fromDate = null, DateTime? toDate = null, string requestId = null)
        {
            var request = _newsRequestFormatter.ReqNewsStoryCount(symbols, formatType, sources, fromDate, toDate, requestId);
            return string.IsNullOrEmpty(requestId)
                ? GetMessagesAsync(request, _newsMessageHandler.GetString)
                : GetMessagesAsync(request, _newsMessageHandler.GetStringWithRequestId);
        }

        public IEnumerable<string> GetNewsConfiguration(FormatType formatType, string requestId = null)
        {
            return GetNewsConfigurationAsync(formatType, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<NewsHeadlinesMessage> GetNewsHeadlines(string[] sources = null, string[] symbols = null, FormatType? formatType = null, int? limit = null, DateTime? date = null, string requestId = null)
        {
            return GetNewsHeadlinesAsync(sources, symbols, formatType, limit, date, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<string> GetNewsStory(string id, NewsFormatType? formatType = null, string deliverTo = null, string requestId = null)
        {
            return GetNewsStoryAsync(id, formatType, deliverTo, requestId).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<string> GetNewsStoryCount(string[] symbols, FormatType? formatType = null, string[] sources = null, DateTime? fromDate = null, DateTime? toDate = null, string requestId = null)
        {
            return GetNewsStoryCountAsync(symbols, formatType, sources, fromDate, toDate, requestId).SynchronouslyAwaitTaskResult();
        }
    }
}