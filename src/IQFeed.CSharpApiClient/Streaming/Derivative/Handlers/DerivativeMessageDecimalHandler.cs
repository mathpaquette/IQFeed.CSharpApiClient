using System;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageDecimalHandler : BaseDerivativeMessageHandler, IDerivativeMessageHandler<decimal>
    {
        public event Action<IntervalBarMessage<decimal>> IntervalBar;

        protected override bool HasIntervalBar(string msg)
        {
            if (IntervalBarMessage.TryParse(msg, out IntervalBarMessage<decimal> intervalBarMessage))
            {
                IntervalBar?.Invoke(intervalBarMessage);
                return true;
            }

            return false;
        }
    }
}