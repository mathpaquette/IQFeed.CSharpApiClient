using System;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageFloatHandler : BaseDerivativeMessageHandler, IDerivativeMessageHandler<float>
    {
        public event Action<IntervalBarMessage<float>> IntervalBar;

        protected override bool HasIntervalBar(string msg)
        {
            if (IntervalBarMessage.TryParse(msg, out IntervalBarMessage<float> intervalBarMessage))
            {
                IntervalBar?.Invoke(intervalBarMessage);
                return true;
            }

            return false;
        }
    }
}