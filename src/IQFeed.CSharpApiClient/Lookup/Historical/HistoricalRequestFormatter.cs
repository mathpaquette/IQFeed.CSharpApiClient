using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalRequestFormatter : RequestFormatter
    {
        public const string HistoricalDataTimeFormat = "hhmmss";
        public const string HistoricalDataDateFormat = "yyyyMMdd";
        public const string HistoricalDataDatetimeFormat = "yyyyMMdd HHmmss";

        public string ReqHistoryTickDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection, string requestId = null, int? datapointsPerSend = null)
        {
            // HTX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF>
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HTX,{symbol.ToUpper()},{maxDatapoints},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryTickDays(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, 
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HTD,[Symbol],[Days],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF>
            days = days > Int16.MaxValue ? Int16.MaxValue : days;
            var beginFilterTimeFormat = beginFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var endFilterTimeFormat = endFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HTD,{symbol.ToUpper()},{days},{maxDatapoints},{beginFilterTimeFormat},{endFilterTimeFormat},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryTickTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, 
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HTT,[Symbol],[BeginDate BeginTime],[EndDate EndTime],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var beginDateFormat = beginDate?.ToString(HistoricalDataDatetimeFormat) ?? string.Empty;
            var endDateFormat = endDate?.ToString(HistoricalDataDatetimeFormat) ?? string.Empty;
            var beginFilterTimeFormat = beginFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var endFilterTimeFormat = endFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HTT,{symbol.ToUpper()},{beginDateFormat},{endDateFormat},{maxDatapoints},{beginFilterTimeFormat},{endFilterTimeFormat},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryIntervalDatapoints(string symbol, int interval, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            // HIX,[Symbol],[Interval],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType],[LabelAtBeginning]<CR><LF> 
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var intervalTypeFormat = intervalType?.ToString().ToLower();
            var labelAtBeginningFormat = labelAtBeginning.HasValue ? ((int)labelAtBeginning).ToString() : null;
            var request = $"HIX,{symbol.ToUpper()},{interval},{maxDatapoints},{dataDirectionFormat},{requestId},{datapointsPerSend},{intervalTypeFormat},{labelAtBeginningFormat}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryIntervalDays(string symbol, int interval, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            // HID,[Symbol],[Interval],[Days],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType],[LabelAtBeginning]<CR><LF> 
            days = days > Int16.MaxValue ? Int16.MaxValue : days;
            var beginFilterTimeFormat = beginFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var endFilterTimeFormat = endFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var intervalTypeFormat = intervalType?.ToString().ToLower();
            var labelAtBeginningFormat = labelAtBeginning.HasValue ? ((int)labelAtBeginning).ToString() : null;
            var request = $"HID,{symbol.ToUpper()},{interval},{days},{maxDatapoints},{beginFilterTimeFormat},{endFilterTimeFormat},{dataDirectionFormat},{requestId},{datapointsPerSend},{intervalTypeFormat},{labelAtBeginningFormat}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryIntervalTimeframe(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            // HIT,[Symbol],[Interval],[BeginDate BeginTime],[EndDate EndTime],[MaxDatapoints],[BeginFilterTime],[EndFilterTime],[DataDirection],[RequestID],[DatapointsPerSend],[IntervalType],[LabelAtBeginning]<CR><LF> 
            var beginDateFormat = beginDate?.ToString(HistoricalDataDatetimeFormat) ?? string.Empty;
            var endDateFormat = endDate?.ToString(HistoricalDataDatetimeFormat) ?? string.Empty;
            var beginFilterTimeFormat = beginFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var endFilterTimeFormat = endFilterTime?.ToString(HistoricalDataTimeFormat) ?? string.Empty;
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var intervalTypeFormat = intervalType?.ToString().ToLower();
            var labelAtBeginningFormat = labelAtBeginning.HasValue ? ((int) labelAtBeginning).ToString() : null;
            var request = $"HIT,{symbol.ToUpper()},{interval},{beginDateFormat},{endDateFormat},{maxDatapoints},{beginFilterTimeFormat},{endFilterTimeFormat},{dataDirectionFormat},{requestId},{datapointsPerSend},{intervalTypeFormat},{labelAtBeginningFormat}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryDailyDatapoints(string symbol, int maxDataPoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HDX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HDX,{symbol.ToUpper()},{maxDataPoints},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryDailyTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HDT,[Symbol],[BeginDate],[EndDate],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var beginDateFormat = beginDate?.ToString(HistoricalDataDateFormat) ?? string.Empty;
            var endDateFormat = endDate?.ToString(HistoricalDataDateFormat) ?? string.Empty;
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HDT,{symbol.ToUpper()},{beginDateFormat},{endDateFormat},{maxDatapoints},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryWeeklyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HWX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HWX,{symbol.ToUpper()},{maxDatapoints},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }

        public string ReqHistoryMonthlyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            // HMX,[Symbol],[MaxDatapoints],[DataDirection],[RequestID],[DatapointsPerSend]<CR><LF> 
            var dataDirectionFormat = dataDirection.HasValue ? ((int)dataDirection).ToString() : null;
            var request = $"HMX,{symbol.ToUpper()},{maxDatapoints},{dataDirectionFormat},{requestId},{datapointsPerSend}{IQFeedDefault.ProtocolTerminatingCharacters}";
            return request;
        }
    }
}