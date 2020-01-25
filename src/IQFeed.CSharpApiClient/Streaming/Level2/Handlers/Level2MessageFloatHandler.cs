using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public class Level2MessageFloatHandler : BaseLevel2MessageHandler<float>
    {
        public Level2MessageFloatHandler() : base(UpdateSummaryMessage.ParseFloat) { }
    }
}