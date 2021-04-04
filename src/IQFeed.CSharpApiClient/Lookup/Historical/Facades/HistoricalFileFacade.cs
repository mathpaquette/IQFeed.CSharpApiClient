using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Facades
{
    public class HistoricalFileFacade : IHistoricalFacade<string, string, string>
    {
        private readonly HistoricalRequestFormatter _historicalRequestFormatter;
        private readonly LookupMessageFileHandler _lookupMessageFileHandler;

        public HistoricalFileFacade(
            HistoricalRequestFormatter historicalRequestFormatter,
            LookupMessageFileHandler lookupMessageFileHandler)
        {
            _lookupMessageFileHandler = lookupMessageFileHandler;
            _historicalRequestFormatter = historicalRequestFormatter;
        }

        public Task<string> GetHistoryTickDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null,
            int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryTickDaysAsync(string symbol, Int16 days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
            TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDays(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryTickTimeframe(symbol, beginDate, endDate, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDatapoints(symbol, interval, maxDatapoints, dataDirection, requestId, datapointsPerSend, intervalType);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryIntervalDaysAsync(string symbol, int interval, Int16 days, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDays(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate,
            int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalTimeframe(symbol, interval, beginDate, endDate,
                maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryDailyTimeframe(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryWeeklyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public Task<string> GetHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return _lookupMessageFileHandler.GetFilenameAsync(request);
        }

        public string GetHistoryTickDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryTickDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryTickDays(string symbol, Int16 days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
            TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null,
            int? datapointsPerSend = null)
        {
            return GetHistoryTickDaysAsync(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection,
                requestId, datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryTickTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryTickTimeframeAsync(symbol, beginDate, endDate, maxDatapoints, beginFilterTime,
                endFilterTime, dataDirection, requestId, datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryIntervalDatapoints(string symbol, int interval, int maxDatapoints,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalDatapointsAsync(symbol, interval, maxDatapoints, dataDirection, requestId,
                datapointsPerSend, intervalType, labelAtBeginning).SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryIntervalDays(string symbol, int interval, Int16 days, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null,
            LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalDaysAsync(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime,
                    dataDirection, requestId, datapointsPerSend, intervalType, labelAtBeginning)
                .SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryIntervalTimeframe(string symbol, int interval, DateTime? beginDate, DateTime? endDate,
            int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalTimeframeAsync(symbol, interval, beginDate, endDate, maxDatapoints,
                beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType,
                labelAtBeginning).SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryDailyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryDailyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryDailyTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryDailyTimeframeAsync(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId,
                datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryWeeklyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryWeeklyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public string GetHistoryMonthlyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryMonthlyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }
    }
}