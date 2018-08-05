using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Common
{
    public class RequestFormatter : IRequestFormatter
    {
        public string SetClientName(string name)
        {
            return $"S,SET CLIENT NAME,{name}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SetProtocol(string version)
        {
            return $"S,SET PROTOCOL,{version}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}