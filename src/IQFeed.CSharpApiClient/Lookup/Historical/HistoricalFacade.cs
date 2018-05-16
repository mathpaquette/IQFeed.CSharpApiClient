using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalFacade : IHistoricalFacade<IEnumerable<TickMessage>, IEnumerable<IntervalMessage>, IEnumerable<DailyWeeklyMonthlyMessage>>
    {
        public const int DefaultTimeoutMs = 3 * 60000;

        private readonly HistoricalRequestFormatter _historicalRequestFormatter;
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly HistoricalMessageHandler _historicalMessageHandler;
        private readonly int _timeoutMs;

        public HistoricalFacade(
            HistoricalRequestFormatter historicalRequestFormatter,
            LookupDispatcher lookupDispatcher,
            HistoricalMessageHandler historicalMessageHandler,
            HistoricalRawFacade historicalRawFacade,
            int timeoutMs = DefaultTimeoutMs)
        {
            _timeoutMs = timeoutMs;
            _historicalMessageHandler = historicalMessageHandler;
            _lookupDispatcher = lookupDispatcher;
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
        public async Task<IEnumerable<TickMessage>> ReqHistoryTickDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<TickMessage>> ReqHistoryTickDaysAsync(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, 
            TimeSpan? endFilterTime = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryTickDays(symbol, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<TickMessage>> ReqHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime =  null, TimeSpan? endFilterTime = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryTickTimeframe(symbol, beginDate, endDate, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetTickMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<IntervalMessage>> ReqHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDatapoints(symbol, interval, maxDatapoints, dataDirection, requestId, datapointsPerSend, intervalType);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<IntervalMessage>> ReqHistoryIntervalDaysAsync(string symbol, int interval, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalDays(symbol, interval, days, maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<IntervalMessage>> ReqHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryIntervalTimeframe(symbol, interval, beginDate, endDate,
                maxDatapoints, beginFilterTime, endFilterTime, dataDirection, requestId, datapointsPerSend, intervalType);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetIntervalMessages).ConfigureAwait(false);
        }

        /// <summary>
        /// HDX - Retrieves up to [maxDatapoints] number of End-Of-Day Data for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public async Task<IEnumerable<DailyWeeklyMonthlyMessage>> ReqHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryDailyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages).ConfigureAwait(false);
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
        public async Task<IEnumerable<DailyWeeklyMonthlyMessage>> ReqHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            if (!beginDate.HasValue && !endDate.HasValue)
                throw new ArgumentException("Begin date or End date must have value.");

            var request = _historicalRequestFormatter.ReqHistoryDailyTimeframe(symbol, beginDate, endDate, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages).ConfigureAwait(false);
        }

        /// <summary>
        /// HWX - Retrieves up to [maxDatapoints] datapoints of composite weekly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public async Task<IEnumerable<DailyWeeklyMonthlyMessage>> ReqHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryWeeklyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages).ConfigureAwait(false);
        }

        /// <summary>
        /// HMX - Retrieves up to [maxDatapoints] datapoints of composite monthly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        public async Task<IEnumerable<DailyWeeklyMonthlyMessage>> ReqHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null)
        {
            var request = _historicalRequestFormatter.ReqHistoryMonthlyDatapoints(symbol, maxDatapoints, dataDirection, requestId, datapointsPerSend);
            return await GetMessagesAsync(request, _historicalMessageHandler.GetDailyWeeklyMonthlyMessages).ConfigureAwait(false);
        }

        private async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, HistoricalMessageContainer<T>> historicalDataMessageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var messages = new List<T>();
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<IEnumerable<T>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var container = historicalDataMessageHandler(args.Message, args.Count);
                messages.AddRange(container.Messages);

                if (container.End)
                    res.TrySetResult(messages);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
            }, TaskContinuationOptions.None);

            return await res.Task;
        }
    }
}