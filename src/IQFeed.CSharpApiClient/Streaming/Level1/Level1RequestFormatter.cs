using System;
using System.Text;
using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1RequestFormatter : RequestFormatter
    {
        public string ReqWatch(string symbol)
        {
            return $"w{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqTradesOnlyWatch(string symbol)
        {
            return $"t{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqUnwatch(string symbol)
        {
            return $"r{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqForcedRefresh(string symbol)
        {
            return $"f{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqTimestamp()
        {
            return $"T{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqTimestamps(bool on)
        {
            var timestamps = on ? "TIMESTAMPSON" : "TIMESTAMPSOFF";
            return $"S,{timestamps}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqRegionalWatch(string symbol)
        {
            return $"S,REGON,{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqRegionalUnwatch(string symbol)
        {
            return $"S,REGOFF,{symbol.ToUpper()}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqNews(bool on)
        {
            var news = on ? "NEWSON" : "NEWSOFF";
            return $"S,{news}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqStats()
        {
            return $"S,REQUEST STATS{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqFundamentalFieldnames()
        {
            return $"S,REQUEST FUNDAMENTAL FIELDNAMES{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqUpdateFieldnames()
        {
            return $"S,REQUEST ALL UPDATE FIELDNAMES{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqCurrentUpdateFieldNames()
        {
            return $"S,REQUEST CURRENT UPDATE FIELDNAMES{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string SelectUpdateFieldName(string[] fieldNames)
        {
            var sb = new StringBuilder("S,SELECT UPDATE FIELDS");
            foreach (var fieldName in fieldNames)
            {
                sb.Append($"{IQFeedDefault.ProtocolDelimiterCharacter}{fieldName.ToUpper()}");
            }
            sb.Append(IQFeedDefault.ProtocolTerminatingCharacters);
            return sb.ToString();
        }

        public string SetLogLevels(LoggingLevel[] logLevels)
        {
            var sb = new StringBuilder("S,SET LOG LEVELS");
            foreach (var logLevel in logLevels)
            {
                sb.Append($"{IQFeedDefault.ProtocolDelimiterCharacter}{logLevel.ToString()}");
            }
            sb.Append(IQFeedDefault.ProtocolTerminatingCharacters);
            return sb.ToString();
        }

        public string ReqWatchList()
        {
            return $"S,REQUEST WATCHES{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqUnwatchAll()
        {
            return $"S,UNWATCH ALL{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerConnect()
        {
            return $"S,CONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqServerDisconnect()
        {
            return $"S,DISCONNECT{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}