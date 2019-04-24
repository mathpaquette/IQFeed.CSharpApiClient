using System.Text;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2RequestFormatter : RequestFormatter
    {
        public string ReqWatch(string symbol)
        {
            return $"w{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqUnwatch(string symbol)
        {
            return $"r{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqTimestamp()
        {
            return $"T{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerConnect()
        {
            return $"S,CONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerDisconnect()
        {
            return $"S,DISCONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqMMID(string mmID)
        {
            return $"m{mmID.ToUpper()}{ IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}