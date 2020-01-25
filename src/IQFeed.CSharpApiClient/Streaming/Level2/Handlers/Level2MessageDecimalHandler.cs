using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public class Level2MessageDecimalHandler : BaseLevel2MessageHandler<decimal>
    {
        public Level2MessageDecimalHandler() : base(UpdateSummaryMessage.Parse) { }
    }
}