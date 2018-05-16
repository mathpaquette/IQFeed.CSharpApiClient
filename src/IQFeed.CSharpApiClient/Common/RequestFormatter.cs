namespace IQFeed.CSharpApiClient.Common
{
    public class RequestFormatter : IRequestFormatter
    {
        public string SetProtocol(string protocol)
        {
            return $"S,SET PROTOCOL,{protocol}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}