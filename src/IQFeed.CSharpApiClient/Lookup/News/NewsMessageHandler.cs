using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.News.Messages;

namespace IQFeed.CSharpApiClient.Lookup.News
{
    public class NewsMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<string> GetString(byte[] message, int count)
        {
            return ProcessMessages(s => s, ParseErrorMessage, message, count);
        }

        public MessageContainer<string> GetStringWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(s => s, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<NewsHeadlinesMessage> GetNewsHeadlines(byte[] message, int count)
        {
            return ProcessMessages(NewsHeadlinesMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<NewsHeadlinesMessage> GetNewsHeadlinesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(NewsHeadlinesMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}