using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Handlers
{
    public class HistoricalMessageDecimalHandler : BaseLookupMessageHandler, IHistoricalMessageHandler<decimal>
    {
        public MessageContainer<TickMessage<decimal>> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseDecimal, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage<decimal>> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseDecimalWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<IntervalMessage<decimal>> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseDecimal, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage<decimal>> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseDecimalWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<decimal>> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseDecimal, ParseErrorMessage, message, count);
        }
        public MessageContainer<DailyWeeklyMonthlyMessage<decimal>> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseDecimalWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}