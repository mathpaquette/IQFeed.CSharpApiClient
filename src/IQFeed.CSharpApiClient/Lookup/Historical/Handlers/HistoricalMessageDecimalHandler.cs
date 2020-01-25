using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Handlers
{
    public class HistoricalMessageDecimalHandler : BaseLookupMessageHandler, IHistoricalMessageHandler<decimal>
    {
        public MessageContainer<TickMessage<decimal>> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage<decimal>> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<IntervalMessage<decimal>> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage<decimal>> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<decimal>> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.Parse, ParseErrorMessage, message, count);
        }
        public MessageContainer<DailyWeeklyMonthlyMessage<decimal>> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}