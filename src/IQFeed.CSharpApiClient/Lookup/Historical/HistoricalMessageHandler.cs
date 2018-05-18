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
            return ProcessMessages(GetTickMessage, message, count);
        }

        public HistoricalMessageContainer<IntervalMessage> GetIntervalMessages(byte[] message, int count)
        {
            return ProcessMessages(GetIntervalMessage, message, count);
        }

        public HistoricalMessageContainer<DailyWeeklyMonthlyMessage> GetDailyWeeklyMonthlyMessages(byte[] message, int count)
        {
            return ProcessMessages(GetDailyWeeklyMonthlyMessage, message, count);
        }

        private HistoricalMessageContainer<T> ProcessMessages<T>(Func<string, T> converter, byte[] message, int count)
        {
            var messages = Encoding.ASCII.GetString(message, 0, count).Split(_lineSplitDelimiter, StringSplitOptions.RemoveEmptyEntries);

            var convertedMessages = new List<T>();
            var endMsg = false;
            var lastMsgIdx = messages.Length - 1;

            // check for errors
            if (messages[0][0] == 'E')
                return new HistoricalMessageContainer<T>(convertedMessages, true, messages[0].Substring(2));

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

        private TickMessage GetTickMessage(string msg)
        {
            var values = msg.Split(',');
            return new TickMessage(
                DateTime.Parse(values[0]),
                float.Parse(values[1]),
                int.Parse(values[2]),
                int.Parse(values[3]),
                float.Parse(values[4]),
                float.Parse(values[5]),
                int.Parse(values[6]),
                char.Parse(values[7]),
                int.Parse(values[8]),
                values[9]);
        }

        private IntervalMessage GetIntervalMessage(string msg)
        {
            var values = msg.Split(',');
            return new IntervalMessage(
                DateTime.Parse(values[0]), 
                float.Parse(values[1]),
                float.Parse(values[2]),
                float.Parse(values[3]),
                float.Parse(values[4]),
                int.Parse(values[5]),
                int.Parse(values[6]));
        }

        private DailyWeeklyMonthlyMessage GetDailyWeeklyMonthlyMessage(string msg)
        {
            var values = msg.Split(',');
            return new DailyWeeklyMonthlyMessage(
                DateTime.Parse(values[0]),
                float.Parse(values[1]),
                float.Parse(values[2]),
                float.Parse(values[3]),
                float.Parse(values[4]),
                int.Parse(values[5]),
                int.Parse(values[6]));
        }
    }
}