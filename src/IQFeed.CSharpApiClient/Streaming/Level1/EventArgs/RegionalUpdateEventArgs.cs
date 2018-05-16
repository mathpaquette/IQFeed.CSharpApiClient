using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class RegionalUpdateEventArgs : System.EventArgs
    {
        public RegionalUpdateMessage RegionalUpdateMessage { get; }

        public RegionalUpdateEventArgs(RegionalUpdateMessage regionalUpdateMessage)
        {
            RegionalUpdateMessage = regionalUpdateMessage;
        }
    }
}