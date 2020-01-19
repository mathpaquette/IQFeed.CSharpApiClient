namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class AutoConnectMessage
    {
        public AutoConnectMessageType Type { get; private set; }

        public AutoConnectMessage(AutoConnectMessageType type)
        {
            Type = type;
        }
    }
}