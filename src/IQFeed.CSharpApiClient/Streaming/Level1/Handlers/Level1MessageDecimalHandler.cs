using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDecimalHandler : BaseLevel1MessageHandler<decimal>
    {
        public Level1MessageDecimalHandler() : base(UpdateSummaryMessage.Parse, RegionalUpdateMessage.Parse) { }
    }
}