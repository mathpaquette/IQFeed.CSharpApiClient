using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageDoubleHandler : BaseDerivativeMessageHandler<double>
    {
        public DerivativeMessageDoubleHandler() : base(IntervalBarMessage.ParseDouble, IntervalBarMessage.ParseDoubleWithRequestId) { }
    }
}