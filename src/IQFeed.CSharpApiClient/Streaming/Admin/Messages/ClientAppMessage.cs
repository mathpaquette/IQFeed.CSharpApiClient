using System;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class ClientAppMessage
    {
        public ClientAppMessageType Type { get; }
        private ClientAppMessage()
        {
            //empty constructor for serialization.
        }
        public ClientAppMessage(ClientAppMessageType type)
        {
            Type = type;
        }
    }
}