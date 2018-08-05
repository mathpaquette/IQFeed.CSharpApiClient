using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public class AdminRequestFormatter : RequestFormatter
    {
        public string RegisterClientApp(string productId, string productVersion)
        {
            return $"S,REGISTER CLIENT APP,{productId},{productVersion}{IQFeedDefault.ProtocolTerminatingCharacters}".ToUpper();
        }

        public string RemoveClientApp(string productId, string productVersion)
        {
            return $"S,REMOVE CLIENT APP,{productId},{productVersion}{IQFeedDefault.ProtocolTerminatingCharacters}".ToUpper();
        }

        public string SetLoginId(string userLoginId)
        {
            return $"S,SET LOGINID,{userLoginId}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SetPassword(string userPassword)
        {
            return $"S,SET PASSWORD,{userPassword}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SetSaveLoginInfo(bool on)
        {
            var value = on ? "On" : "Off";
            return $"S,SET SAVE LOGIN INFO,{value}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SetAutoconnect(bool on)
        {
            var value = on ? "On" : "Off";
            return $"S,SET AUTOCONNECT,{value}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerConnect()
        {
            return $"S,CONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerDisconnect()
        {
            return $"S,DISCONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SetClientStats(bool on)
        {
            var value = on ? "ON" : "OFF";
            return $"S,CLIENTSTATS {value}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}