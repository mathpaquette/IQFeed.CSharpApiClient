using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageFloatHandler : BaseLevel1MessageHandler, ILevel1MessageHandler<float>
    {
        public event Action<UpdateSummaryMessage<float>> Summary;
        public event Action<UpdateSummaryMessage<float>> Update;
        public event Action<RegionalUpdateMessage<float>> Regional;

        protected override void ProcessSummaryMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseFloat(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.ParseFloat(msg);
            Update?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessRegionalUpdateMessage(string msg)
        {
            var regionUpdateMessage = RegionalUpdateMessage.ParseFloat(msg);
            Regional?.Invoke(regionUpdateMessage);
        }
    }
}