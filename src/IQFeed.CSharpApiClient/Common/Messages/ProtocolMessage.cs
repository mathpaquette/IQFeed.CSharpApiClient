namespace IQFeed.CSharpApiClient.Common.Messages
{
    public class ProtocolMessage
    {
        public string Version { get; private set; }

        public ProtocolMessage(string version)
        {
            Version = version;
        }
    }
}