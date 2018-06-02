namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class ClientAppMessage
    {
        public ClientAppMessageType Type { get; }

        public ClientAppMessage(ClientAppMessageType type)
        {
            Type = type;
        }
    }
}