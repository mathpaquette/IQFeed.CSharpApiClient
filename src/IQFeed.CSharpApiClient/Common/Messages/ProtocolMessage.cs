namespace IQFeed.CSharpApiClient.Common.Messages
{
    public class ProtocolMessage
    {
        public string Version { get; }

        public ProtocolMessage(string version)
        {
            Version = version;
        }
    }
}