using System;
using System.Collections.Generic;
using System.Text;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Lookup.Historical
{
    public class HistoricalMessageHandler
    {
        private readonly string[] _lineSplitDelimiter = { ",\r\n" };

        public HistoricalMessageContainer<TickMessage> GetTickMessages(byte[] message, int count)
        {
            return ProcessMessages(TickMessage.Parse, message, count);
        }

        public HistoricalMessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(IntervalMessage.Parse, message, count);
        }

        public HistoricalMessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(DailyWeeklyMonthlyMessage.Parse, message, count);
        }

        private HistoricalMessageContainer<T> ProcessMessages<T>(Func<string, T> converter, byte[] message, int count)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).Split(_lineSplitDelimiter, StringSplitOptions.RemoveEmptyEntries);

            var convertedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;

            // check for errors
            if (messages[0][0] == 'E')
                return new HistoricalMessageContainer<T>(convertedMessages, true, messages[0]);

            for (var i = 0; i < messages.Length; i++)
            {
                // check for last message
                if (i == lastMsgIdx && messages[i] == IQFeedDefault.ProtocolEndOfMessageCharacters)
                {
                    endMsg = true;
                    break;
                }

                convertedMessages.Add(converter(messages[i]));
            }

            return new HistoricalMessageContainer<T>(convertedMessages, endMsg);
        }
    }
}