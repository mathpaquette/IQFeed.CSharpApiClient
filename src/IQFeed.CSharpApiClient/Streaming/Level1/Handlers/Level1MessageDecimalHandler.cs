using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDecimalHandler : BaseLevel1MessageHandler, ILevel1MessageHandler<decimal>
    {
        public event Action<UpdateSummaryMessage<decimal>> Summary;
        public event Action<UpdateSummaryMessage<decimal>> Update;
        public event Action<RegionalUpdateMessage<decimal>> Regional;

        protected override void ProcessSummaryMessage(string msg, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseDecimal(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessUpdateMessage(string msg, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseDecimal(msg);
            Update?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessRegionalUpdateMessage(string msg)
        {
            var regionUpdateMessage = RegionalUpdateMessage.ParseDecimal(msg);
            Regional?.Invoke(regionUpdateMessage);
        }
    }
}