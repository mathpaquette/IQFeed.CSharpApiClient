using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalMessageHandler : BaseLookupMessageHandler
    {
        public MessageContainer<TickMessage> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.Parse, message, count, false);
        }

        public MessageContainer<TickMessage> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.Parse, message, count, false);
        }

        public MessageContainer<IntervalMessage> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseWithRequestId, message, count, true);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.Parse, message, count, false);
        }
        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseWithRequestId, message, count, true);
        }
    }
}