using System;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageHandler : BaseLevel1MessageHandler, ILevel1MessageHandler
    {
        public event Action<IUpdateSummaryMessage> Summary;
        public event Action<IUpdateSummaryMessage> Update;

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