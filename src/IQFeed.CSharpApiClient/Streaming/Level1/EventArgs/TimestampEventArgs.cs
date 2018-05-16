using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class TimestampEventArgs : System.EventArgs
    {
        public TimestampMessage TimestampMessage { get; }

        public TimestampEventArgs(TimestampMessage timestampMessage)
        {
            TimestampMessage = timestampMessage;
        }
    }
}