using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Common;
using IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary
{
    public class MarketSummaryHandler<T> : BaseLookupMessageHandler where T : struct
    {

        public MarketSummaryHandler()
        {
            FieldNames = new List<string>();
            switch (typeof(T).Name)
            {
                case "Double":
                    FieldConvertors = MarketSummaryMessage<double>.GetFieldsetDescriptorsDouble();
                    break;

                case "Decimal":
                    FieldConvertors = MarketSummaryMessage<decimal>.GetFieldsetDescriptorsDecimal();
                    break;

                case "Single":
                    FieldConvertors = MarketSummaryMessage<float>.GetFieldsetDescriptorsFloat();
                    break;
            }
        }

        public List<string> FieldNames { get; private set; }

        public IDictionary<string, Func<string, object>> FieldConvertors { get; private set; }

        public MessageContainer<MarketSummaryMessage<T>> GetMarketSummaryMessages(byte[] rawMessage, int count)
        {
            return ProcessMessages<MarketSummaryMessage<T>>((message) => MarketSummaryMessage<T>.Parse(message, this), ParseErrorMessage, rawMessage, count);
        }

        public MessageContainer<MarketSummaryMessage<T>> GetMarketSummaryMessagesWithRequestId(byte[] rawMessage, int count)
        {
            return ProcessMessages<MarketSummaryMessage<T>>((message) => MarketSummaryMessage<T>.ParseWithRequestId(message, this), ParseErrorMessageWithRequestId, rawMessage, count);
        }


    }
}
