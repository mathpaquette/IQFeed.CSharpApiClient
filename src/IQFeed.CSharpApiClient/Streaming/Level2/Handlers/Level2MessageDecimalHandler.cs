using System;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public class Level2MessageDecimalHandler : BaseLevel2MessageHandler, ILevel2MessageHandler<decimal>
    {
        public event Action<UpdateSummaryMessage<decimal>> Summary;
        public event Action<UpdateSummaryMessage<decimal>> Update;

        protected override void ProcessSummaryMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg);
            Summary?.Invoke(updateSummaryMessage);
        }

        protected override void ProcessUpdateMessage(string msg)
        {
            var updateSummaryMessage = UpdateSummaryMessage.Parse(msg);
            Update?.Invoke(updateSummaryMessage);
        }
    }
}