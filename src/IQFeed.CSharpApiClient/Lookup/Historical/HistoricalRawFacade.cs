using System;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalRawFacade : IHistoricalFacade<string, string, string>
    {
        private readonly HistoricalRequestFormatter _historicalRequestFormatter;
        private readonly RawMessageHandler _rawMessageHandler;

        public HistoricalRawFacade(
            HistoricalRequestFormatter historicalRequestFormatter, 
            RawMessageHandler rawMessageHandler)
        {
            _rawMessageHandler = rawMessageHandler;
            _historicalRequestFormatter = historicalRequestFormatter;
        }

        public async Task<string> ReqHistoryTickDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null,
            int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryTickDaysAsync(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
            TimeSpan? endFilterTime = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDays(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryTickTimeframe(symbol, beginDate, endDate, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDatapoints(symbol, interval, maxDatapoints, dataDirection, requestId, datapointsPerSend, intervalType);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryIntervalDaysAsync(string symbol, int interval, int days, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDays(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate,
            int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalTimeframe(symbol, interval, beginDate, endDate,
                maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryDailyTimeframe(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryWeeklyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }

        public async Task<string> ReqHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await _rawMessageHandler.GetStringAsync(request);
        }
    }
}