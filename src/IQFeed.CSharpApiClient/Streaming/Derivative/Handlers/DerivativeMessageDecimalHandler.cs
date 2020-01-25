using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageDecimalHandler : BaseDerivativeMessageHandler<decimal>
    {
        public DerivativeMessageDecimalHandler() : base(IntervalBarMessage.Parse, IntervalBarMessage.ParseWithRequestId) { }
    }
}