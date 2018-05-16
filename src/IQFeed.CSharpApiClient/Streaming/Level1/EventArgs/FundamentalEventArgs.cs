using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class FundamentalEventArgs : System.EventArgs
    {
        public FundamentalMessage FundamentalMessage { get; }

        public FundamentalEventArgs(FundamentalMessage fundamentalMessage)
        {
            FundamentalMessage = fundamentalMessage;
        }
    }
}