using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDoubleHandler : BaseLevel1MessageHandler, ILevel1MessageHandler<double>
    {
        public event Action<UpdateSummaryMessage<double>> Summary;
        public event Action<UpdateSummaryMessage<double>> Update;
        public event Action<RegionalUpdateMessage<double>> Regional;

        protected override void ProcessSummaryMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseDouble(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseDouble(msg);
            Update?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessRegionalUpdateMessage(string msg)
        {
            var regionUpdateMessage = RegionalUpdateMessage.ParseDouble(msg);
            Regional?.Invoke(regionUpdateMessage);
        }
    }
}