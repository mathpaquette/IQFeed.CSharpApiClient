using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class UpdateSummaryEventArgs : System.EventArgs
    {
        public UpdateSummaryMessage UpdateSummaryMessage { get; }

        public UpdateSummaryEventArgs(UpdateSummaryMessage updateSummaryMessage)
        {
            UpdateSummaryMessage = updateSummaryMessage;
        }
    }
}