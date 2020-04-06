using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDoubleHandler : BaseLevel1MessageHandler, ILevel1MessageHandler<double>
    {
        public event Action<UpdateSummaryMessage<double>> Summary;
        public event Action<UpdateSummaryMessage<double>> Update;
        public event Action<RegionalUpdateMessage<double>> Regional;

        protected override void ProcessSummaryMessage(string msg, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg, dynamicFieldsetHandler);
            Summary?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessUpdateMessage(string msg, DynamicFieldsetHandler dynamicFieldsetHandler = null)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg, dynamicFieldsetHandler);
            Update?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessRegionalUpdateMessage(string msg)
        {
            var regionUpdateMessage = RegionalUpdateMessage.Parse(msg);
            Regional?.Invoke(regionUpdateMessage);
        }
    }
}