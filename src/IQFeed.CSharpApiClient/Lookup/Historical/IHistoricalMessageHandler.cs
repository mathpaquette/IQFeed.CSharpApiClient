using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public interface IHistoricalMessageHandler
    {
        MessageContainer<TickMessage> GetTickMessages(byte[] message, int count);
        MessageContainer<TickMessage> GetTickMessagesWithRequestId(byte[] message, int count);
        MessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count);
        MessageContainer<IntervalMessage> GetIntervalMessagesWithRequestId(byte[] message, int count);
        MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count);
        MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count);
    }
}