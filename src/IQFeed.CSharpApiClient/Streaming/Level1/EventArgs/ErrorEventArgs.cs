using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class ErrorEventArgs : System.EventArgs
    {
        public ErrorEventArgs(ErrorMessage errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public ErrorMessage ErrorMessage { get; }
    }
}