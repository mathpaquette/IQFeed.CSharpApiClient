using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Handlers
{
    public class Level2MessageDoubleHandler : BaseLevel2MessageHandler<double>
    {
        public Level2MessageDoubleHandler() : base(UpdateSummaryMessage.ParseDouble) { }
    }
}