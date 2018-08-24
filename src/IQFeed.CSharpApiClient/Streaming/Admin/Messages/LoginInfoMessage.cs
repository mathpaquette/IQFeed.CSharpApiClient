using System;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class LoginInfoMessage
    {
        public LoginInfoMessageType Type { get; }
        private LoginInfoMessage()
        {
            //empty constructor for serialization.
        }
        public LoginInfoMessage(LoginInfoMessageType type)
        {
            Type = type;
        }
    }
}