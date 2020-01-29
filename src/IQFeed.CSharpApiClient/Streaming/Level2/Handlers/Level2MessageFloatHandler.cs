using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public class Level2MessageFloatHandler : BaseLevel2MessageHandler, ILevel2MessageHandler<float>
    {
        public event Action<UpdateSummaryMessage<float>> Summary;
        public event Action<UpdateSummaryMessage<float>> Update;

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
    }
}