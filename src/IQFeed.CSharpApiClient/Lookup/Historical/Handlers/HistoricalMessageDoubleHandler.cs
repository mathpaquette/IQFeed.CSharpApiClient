using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical.Handlers
{
    public class HistoricalMessageDoubleHandler : BaseLookupMessageHandler, IHistoricalMessageHandler<double>
    {
        public MessageContainer<TickMessage<double>> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage<double>> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<IntervalMessage<double>> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage<double>> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<double>> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.Parse, ParseErrorMessage, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage<double>> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}