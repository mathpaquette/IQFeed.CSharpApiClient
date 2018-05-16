using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class SystemEventArgs : System.EventArgs
    {
        public SystemMessage SystemMessage { get; }

        public SystemEventArgs(SystemMessage systemMessage)
        {
            SystemMessage = systemMessage;
        }
    }
}