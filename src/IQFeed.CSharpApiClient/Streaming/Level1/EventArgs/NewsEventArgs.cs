using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class NewsEventArgs : System.EventArgs
    {
        public NewsMessage NewsMessage { get; }

        public NewsEventArgs(NewsMessage newsMessage)
        {
            NewsMessage = newsMessage;
        }
    }
}