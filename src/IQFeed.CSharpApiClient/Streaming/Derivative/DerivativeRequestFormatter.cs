using System;
using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeRequestFormatter : RequestFormatter
    {
        public const string DerivativeTimeFormat = "hhmmss";
        public const string DerivativeDatetimeFormat = "yyyyMMdd HHmmss";

        public string ReqBarWatch(string symbol, int interval, DateTime? beginDate = null, int? maxDaysOfDatapoints = null, int? maxDatapoints = null, 
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, string requestId = null, DerivativeIntervalType? intervalType = null, int? updateInterval = null)
        {
            var request = $"BW,{symbol.ToUpper()},{interval},{beginDate?.ToString(DerivativeDatetimeFormat)},{maxDaysOfDatapoints},{maxDatapoints},{beginFilterTime?.ToString(DerivativeTimeFormat)},{endFilterTime?.ToString(DerivativeTimeFormat)},{requestId},{intervalType?.ToString().ToLower()},{updateInterval},{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqBarUnwatch(string symbol, string requestId)
        {
            return $"BR,{symbol.ToUpper()},{requestId},{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string ReqWatches()
        {
            return $"S,REQUEST WATCHES,{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        public string UnwatchAll()
        {
            return $"S,UNWATCH ALL,{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}