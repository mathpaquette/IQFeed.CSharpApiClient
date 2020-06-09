using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalMessageHandler : BaseLookupMessageHandler, IHistoricalMessageHandler
    {
        public MessageContainer<TickMessage> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}