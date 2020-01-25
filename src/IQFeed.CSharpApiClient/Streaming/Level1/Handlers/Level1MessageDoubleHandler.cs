using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Handlers
{
    public class Level1MessageDoubleHandler : BaseLevel1MessageHandler<double>
    {
        public Level1MessageDoubleHandler() : base(UpdateSummaryMessage.ParseDouble, RegionalUpdateMessage.ParseDouble) { }
    }
}