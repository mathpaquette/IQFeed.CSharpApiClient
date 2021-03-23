using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalMessageHandler : BaseLookupMessageHandler, IHistoricalMessageHandler
    {
        public static readonly TryParseDelegate<string, TickMessage, bool> TryParseTick = TickMessage.TryParse;
        public static readonly TryParseDelegate<string, IntervalMessage, bool> TryParseInterval = IntervalMessage.TryParse;
        public static readonly TryParseDelegate<string, DailyWeeklyMonthlyMessage, bool> TryParseDaily = DailyWeeklyMonthlyMessage.TryParse;

        // tick
        public MessageContainer<TickMessage> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TryParseTick, ParseErrorMessage, message, count);
        }

        public MessageContainer<TickMessage> GetTickMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        // interval
        public MessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(TryParseInterval, ParseErrorMessage, message, count);
        }

        public MessageContainer<IntervalMessage> GetIntervalMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }

        // daily
        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(TryParseDaily, ParseErrorMessage, message, count);
        }

        public MessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessagesWithRequestId(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.ParseWithRequestId, ParseErrorMessageWithRequestId, message, count);
        }
    }
}