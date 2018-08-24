using System;

namespace IQFeed.CSharpApiClient.Common.Messages
{
    [Serializable]
    public class ProtocolMessage
    {
        public string Version { get; }
        private ProtocolMessage()
        {
            //empty constructor for serialization.
        }

        public ProtocolMessage(string version)
        {
            Version = version;
        }
    }
}