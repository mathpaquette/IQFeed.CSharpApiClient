using System;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public interface IHistoricalFacade<TTickMessages, TIntervalMessages, TDailyWeeklyMonthlyMessages>
    {
        /// <summary>
        /// HTX - Retrieves up to [MaxDatapoints] number of ticks for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        Task<TTickMessages> ReqHistoryTickDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

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
        Task<TTickMessages> ReqHistoryTickDaysAsync(string symbol, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, 
            TimeSpan? endFilterTime = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

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
        Task<TTickMessages> ReqHistoryTickTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime =  null, TimeSpan? endFilterTime = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

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
        Task<TIntervalMessages> ReqHistoryIntervalDatapointsAsync(string symbol, int interval, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null);

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
        Task<TIntervalMessages> ReqHistoryIntervalDaysAsync(string symbol, int interval, int days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null);

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
        Task<TIntervalMessages> ReqHistoryIntervalTimeframeAsync(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null,
            int? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null);

        /// <summary>
        /// HDX - Retrieves up to [maxDatapoints] number of End-Of-Day Data for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        Task<TDailyWeeklyMonthlyMessages> ReqHistoryDailyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

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
        Task<TDailyWeeklyMonthlyMessages> ReqHistoryDailyTimeframeAsync(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

        /// <summary>
        /// HWX - Retrieves up to [maxDatapoints] datapoints of composite weekly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        Task<TDailyWeeklyMonthlyMessages> ReqHistoryWeeklyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);

        /// <summary>
        /// HMX - Retrieves up to [maxDatapoints] datapoints of composite monthly datapoints for the specified [Symbol].
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="maxDatapoints"></param>
        /// <param name="dataDirection"></param>
        /// <param name="requestId"></param>
        /// <param name="datapointsPerSend"></param>
        Task<TDailyWeeklyMonthlyMessages> ReqHistoryMonthlyDatapointsAsync(string symbol, int maxDatapoints, int? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
    }
}