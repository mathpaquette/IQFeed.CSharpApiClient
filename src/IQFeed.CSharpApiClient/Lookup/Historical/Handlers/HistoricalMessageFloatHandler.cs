using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Handlers
{
    public class HistoricalMessageFloatHandler : BaseLookupMessageHandler, IHistoricalMessageHandler<float>
    {
        public MessageContainer<TickMessage<float>> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseFloat, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage<float>> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseFloatWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<IntervalMessage<float>> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseFloat, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage<float>> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseFloatWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<float>> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseFloat, ParseErrorMessage, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<float>> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseFloatWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}