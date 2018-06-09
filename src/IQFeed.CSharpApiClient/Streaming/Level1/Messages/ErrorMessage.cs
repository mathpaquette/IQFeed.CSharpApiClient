namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class ErrorMessage
    {
        public ErrorMessage(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}