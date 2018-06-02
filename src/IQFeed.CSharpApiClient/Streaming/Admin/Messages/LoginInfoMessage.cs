namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class LoginInfoMessage
    {
        public LoginInfoMessageType Type { get; }

        public LoginInfoMessage(LoginInfoMessageType type)
        {
            Type = type;
        }
    }
}