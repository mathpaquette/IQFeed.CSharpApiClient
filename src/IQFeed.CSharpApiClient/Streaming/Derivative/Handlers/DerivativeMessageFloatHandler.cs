using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageFloatHandler : BaseDerivativeMessageHandler<float>
    {
        public DerivativeMessageFloatHandler() : base(IntervalBarMessage.ParseFloat, IntervalBarMessage.ParseFloatWithRequestId) { }
    }
}