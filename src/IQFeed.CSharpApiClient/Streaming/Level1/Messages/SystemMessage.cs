namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class SystemMessage
    {
        public string Type { get; }
        public string Message { get; }

        public SystemMessage(string type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}