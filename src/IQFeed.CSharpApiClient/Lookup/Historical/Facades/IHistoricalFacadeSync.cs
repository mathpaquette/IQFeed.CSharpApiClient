using System;
using IQFeed.CSharpApiClient.Lookup.Historical.Enums;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Facades
{
    public interface IHistoricalFacadeSync<TTickMessages, TIntervalMessages, TDailyWeeklyMonthlyMessages>
    {
        TTickMessages GetHistoryTickDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TTickMessages GetHistoryTickDays(string symbol, Int16 days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TTickMessages GetHistoryTickTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TIntervalMessages GetHistoryIntervalDatapoints(string symbol, int interval, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null);
        TIntervalMessages GetHistoryIntervalDays(string symbol, int interval, Int16 days, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null);
        TIntervalMessages GetHistoryIntervalTimeframe(string symbol, int interval, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, TimeSpan? endFilterTime = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null, HistoricalIntervalType? intervalType = null, LabelAtBeginning? labelAtBeginning = null);
        TDailyWeeklyMonthlyMessages GetHistoryDailyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TDailyWeeklyMonthlyMessages GetHistoryDailyTimeframe(string symbol, DateTime? beginDate, DateTime? endDate, int? maxDatapoints = null, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TDailyWeeklyMonthlyMessages GetHistoryWeeklyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
        TDailyWeeklyMonthlyMessages GetHistoryMonthlyDatapoints(string symbol, int maxDatapoints, DataDirection? dataDirection = null, string requestId = null, int? datapointsPerSend = null);
    }
}