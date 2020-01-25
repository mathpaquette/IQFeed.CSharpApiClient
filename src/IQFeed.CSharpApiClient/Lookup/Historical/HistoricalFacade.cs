using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;
using IQFeed.CSharpApiClient.Lookup.Historical.Handlers;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalFacade<T> : BaseLookupFacade, IHistoricalFacade<IEnumerable<TickMessage<T>>, IEnumerable<IntervalMessage<T>>, IEnumerable<DailyWeeklyMonthlyMessage<T>>>
    {
        private readonly HistoricalRequestFormatter _historicalRequestFormatter;
        private readonly IHistoricalMessageHandler<T> _historicalMessageHandler;

        public HistoricalFacade(
            HistoricalRequestFormatter historicalRequestFormatter,
            LookupDispatcher lookupDispatcher,
            ExceptionFactory exceptionFactory,
            IHistoricalMessageHandler<T> historicalMessageHandler,
            HistoricalRawFacade historicalRawFacade,
            int timeoutMs) : base(lookupDispatcher, exceptionFactory, timeoutMs)
        {
            _historicalMessageHandler = historicalMessageHandler;
            _historicalRequestFormatter = historicalRequestFormatter;
            Raw = historicalRawFacade;
        }

        public HistoricalRawFacade Raw { get; }

        /// <summary>
        /// HTX - Retrieves up to [MaxDatapoints] number of ticks for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public Task<IEnumerable<TickMessage<T>>> ReqHistoryTickDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
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
        public Task<IEnumerable<TickMessage<T>>> ReqHistoryTickDaysAsync(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null,
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
        public Task<IEnumerable<TickMessage<T>>> ReqHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
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
        public Task<IEnumerable<IntervalMessage<T>>> ReqHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null)
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
        public Task<IEnumerable<IntervalMessage<T>>> ReqHistoryIntervalDaysAsync(string symbol, int interval, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
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
        public Task<IEnumerable<IntervalMessage<T>>> ReqHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
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
        public Task<IEnumerable<DailyWeeklyMonthlyMessage<T>>> ReqHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
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
        public Task<IEnumerable<DailyWeeklyMonthlyMessage<T>>> ReqHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
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
        public Task<IEnumerable<DailyWeeklyMonthlyMessage<T>>> ReqHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
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
        public Task<IEnumerable<DailyWeeklyMonthlyMessage<T>>> ReqHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return string.IsNullOrEmpty(requestId) ? GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages) : GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessagesWithRequestId);
        }
    }
}