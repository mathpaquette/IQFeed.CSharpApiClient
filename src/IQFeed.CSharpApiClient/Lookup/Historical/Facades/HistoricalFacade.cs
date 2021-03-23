using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Facades
{
    public class HistoricalFacade : BaseLookupFacade, IHistoricalFacade<IEnumerable<TickMessage>, IEnumerable<IntervalMessage>, IEnumerable<DailyWeeklyMonthlyMessage>>
    {
        private readonly HistoricalRequestFormatter _historicalRequestFormatter;
        private readonly IHistoricalMessageHandler _historicalMessageHandler;

        public HistoricalFacade(
            HistoricalRequestFormatter historicalRequestFormatter,
            LookupDispatcher lookupDispatcher,
            LookupRateLimiter lookupRateLimiter,
            ExceptionFactory exceptionFactory,
            IHistoricalMessageHandler historicalMessageHandler,
            HistoricalFileFacade historicalFileFacade,
            TimeSpan timeout) : base(lookupDispatcher, lookupRateLimiter, exceptionFactory, timeout)
        {
            _historicalMessageHandler = historicalMessageHandler;
            _historicalRequestFormatter = historicalRequestFormatter;
            File = historicalFileFacade;
        }

        public HistoricalFileFacade File { get; }

        [Obsolete("Raw property is deprecated. Please use File instead. Will be removed in next major version.")]
        public HistoricalFileFacade Raw => File;

        /// <summary>
        /// HTX - Retrieves up to [MaxDatapoints] number of ticks for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<TickMessage>> GetHistoryTickDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetTickMessagesWithRequestId);
        }

        /// <summary>
        /// HTD - Retrieves ticks for the previous [Days] days for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="days"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="beginFilterTime"></param>
        /// <param name="endFilterTime"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        /// <returns></returns>
        public Task<IEnumerable<TickMessage>> GetHistoryTickDaysAsync(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
            TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDays(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetTickMessagesWithRequestId);
        }

        /// <summary>
        /// HTT - Retrieves tick data between [BeginDate BeginTime] and [EndDate EndTime] for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="beginFilterTime"></param>
        /// <param name="endFilterTime"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<TickMessage>> GetHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryTickTimeframe(symbol, beginDate, endDate, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetTickMessagesWithRequestId);
        }

        /// <summary>
        /// HIX - Retrieves [maxDatapoints] number of Intervals of data for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        /// <param name="intervalType"></param>
        public Task<IEnumerable<IntervalMessage>> GetHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDatapoints(symbol, interval, maxDatapoints, dataDirection, requestId, datapointsPerSend, intervalType, labelAtBeginning);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessagesWithRequestId);
        }

        /// <summary>
        /// HID - Retrieves [Days] days of interval data for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="days"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="beginFilterTime"></param>
        /// <param name="endFilterTime"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        /// <param name="intervalType"></param>
        public Task<IEnumerable<IntervalMessage>> GetHistoryIntervalDaysAsync(string symbol, int interval, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDays(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType, labelAtBeginning);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessagesWithRequestId);
        }

        /// <summary>
        /// HIT - Retrieves interval data between [BeginDate BeginTime] and [EndDate EndTime] for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="interval"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="beginFilterTime"></param>
        /// <param name="endFilterTime"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        /// <param name="intervalType"></param>
        public Task<IEnumerable<IntervalMessage>> GetHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalTimeframe(symbol, interval, beginDate, endDate,
                maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType, labelAtBeginning);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessagesWithRequestId);
        }

        /// <summary>
        /// HDX - Retrieves up to [maxDatapoints] number of End-Of-Day Data for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<DailyWeeklyMonthlyMessage>> GetHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessagesWithRequestId);
        }

        /// <summary>
        /// HDT - Retrieves Daily data between [BeginDate] and [EndDate] for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<DailyWeeklyMonthlyMessage>> GetHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryDailyTimeframe(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessagesWithRequestId);
        }

        /// <summary>
        /// HWX - Retrieves up to [maxDatapoints] datapoints of composite weekly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<DailyWeeklyMonthlyMessage>> GetHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryWeeklyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessagesWithRequestId);
        }

        /// <summary>
        /// HMX - Retrieves up to [maxDatapoints] datapoints of composite monthly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<DailyWeeklyMonthlyMessage>> GetHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessagesWithRequestId);
        }

        public IEnumerable<TickMessage> GetHistoryTickDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryTickDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<TickMessage> GetHistoryTickDays(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
            TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null,
            int? datapointsPerSend = null)
        {
            return GetHistoryTickDaysAsync(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection,
                requestId, datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<TickMessage> GetHistoryTickTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryTickTimeframeAsync(symbol, beginDate, endDate, maxDatapoints, beginFilterTime,
                endFilterTime, dataDirection, requestId, datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<IntervalMessage> GetHistoryIntervalDatapoints(string symbol, int interval, int maxDatapoints,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalDatapointsAsync(symbol, interval, maxDatapoints, dataDirection, requestId,
                datapointsPerSend, intervalType, labelAtBeginning).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<IntervalMessage> GetHistoryIntervalDays(string symbol, int interval, int days, int? maxDatapoints = null,
            TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null,
            LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalDaysAsync(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime,
                    dataDirection, requestId, datapointsPerSend, intervalType, labelAtBeginning)
                .SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<IntervalMessage> GetHistoryIntervalTimeframe(string symbol, int interval, DateTime? beginDate, DateTime? endDate,
            int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null,
            HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
        {
            return GetHistoryIntervalTimeframeAsync(symbol, interval, beginDate, endDate, maxDatapoints,
                beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType,
                labelAtBeginning).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<DailyWeeklyMonthlyMessage> GetHistoryDailyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryDailyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<DailyWeeklyMonthlyMessage> GetHistoryDailyTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null,
            DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryDailyTimeframeAsync(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId,
                datapointsPerSend).SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<DailyWeeklyMonthlyMessage> GetHistoryWeeklyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryWeeklyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }

        public IEnumerable<DailyWeeklyMonthlyMessage> GetHistoryMonthlyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null,
            string requestId = null, int? datapointsPerSend = null)
        {
            return GetHistoryMonthlyDatapointsAsync(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend)
                .SynchronouslyAwaitTaskResult();
        }
    }
}