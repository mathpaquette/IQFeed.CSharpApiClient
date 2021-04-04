using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2RequestFormatter : RequestFormatter
    {
        public string ReqWatch(string symbol)
        {
            return $"w{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqWatchMarketByPrice(string symbol)
        {
            return $"WPL,{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqWatchMarketByOrder(string symbol)
        {
            return $"WOR,{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqMarketMakerNameById(string mmid)
        {
            return $"m{mmid.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqUnwatch(string symbol)
        {
            return $"r{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerConnect()
        {
            return $"c{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerDisconnect()
        {
            return $"x{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}