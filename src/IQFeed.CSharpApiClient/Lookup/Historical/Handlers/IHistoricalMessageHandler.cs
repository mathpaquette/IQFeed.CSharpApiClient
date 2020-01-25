using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Handlers
{
    public interface IHistoricalMessageHandler<T>
    {
        MessageContainer<TickMessage<T>> GetTickMessages(byte[] message, int count);
        MessageContainer<TickMessage<T>> GetTickMessagesWithRequestId(byte[] message, int count);
        MessageContainer<IntervalMessage<T>> GetIntervalMessages(byte[] message, int count);
        MessageContainer<IntervalMessage<T>> GetIntervalMessagesWithRequestId(byte[] message, int count);
        MessageContainer<DailyWeeklyMonthlyMessage<T>> GetDailyWeeklyMonthlyMessages(byte[] message, int count);
        MessageContainer<DailyWeeklyMonthlyMessage<T>> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count);
    }
}