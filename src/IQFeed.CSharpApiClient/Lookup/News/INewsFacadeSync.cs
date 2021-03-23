using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Lookup.News.Enums;
using IQFeed.CSharpApiClient.Lookup.News.Messages;

namespace IQFeed.CSharpApiClient.Lookup.News
{
    public interface INewsFacadeSync
    {
        IEnumerable<string> GetNewsConfiguration(FormatType formatType, string requestId = null);

        IEnumerable<NewsHeadlinesMessage> GetNewsHeadlines(string[] sources = null, string[] symbols = null, FormatType? formatType = null, int? limit = null, DateTime? date = null, string requestId = null);

        IEnumerable<string> GetNewsStory(string id, NewsFormatType? formatType = null, string deliverTo = null, string requestId = null);

        IEnumerable<string> GetNewsStoryCount(string[] symbols, FormatType? formatType = null, string[] sources = null, DateTime? fromDate = null, DateTime? toDate = null, string requestId = null);
    }
}