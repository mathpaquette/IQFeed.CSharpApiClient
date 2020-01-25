using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageFloatHandler : BaseLevel1MessageHandler<float>
    {
        public Level1MessageFloatHandler() : base(UpdateSummaryMessage.ParseFloat, RegionalUpdateMessage.ParseFloat) { }
    }
}