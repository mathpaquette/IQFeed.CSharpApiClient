using System;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class AutoConnectMessage
    {
        public AutoConnectMessageType Type { get; }

        private AutoConnectMessage()
        {
            //empty constructor for serialization.
        }
        public AutoConnectMessage(AutoConnectMessageType type)
        {
            Type = type;
        }
    }
}