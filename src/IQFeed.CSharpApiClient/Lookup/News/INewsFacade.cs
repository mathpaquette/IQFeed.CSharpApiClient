using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.News.Enums;
using IQFeed.CSharpApiClient.Lookup.News.Messages;

namespace IQFeed.CSharpApiClient.Lookup.News
{
    public interface INewsFacade : INewsFacadeSync
    {
        Task<IEnumerable<string>> GetNewsConfigurationAsync(FormatType formatType, string requestId = null);
        
        Task<IEnumerable<NewsHeadlinesMessage>> GetNewsHeadlinesAsync(string[] sources = null, string[] symbols = null, FormatType? formatType = null, int? limit = null, DateTime? date = null, string requestId = null);

        Task<IEnumerable<string>> GetNewsStoryAsync(string id, NewsFormatType? formatType = null, string deliverTo = null, string requestId = null);

        Task<IEnumerable<string>> GetNewsStoryCountAsync(string[] symbols, FormatType? formatType = null, string[] sources = null, DateTime? fromDate = null, DateTime? toDate = null, string requestId = null);
    }
}