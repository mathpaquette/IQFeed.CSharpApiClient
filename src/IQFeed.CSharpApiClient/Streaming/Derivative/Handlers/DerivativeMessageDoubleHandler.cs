using System;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative.Handlers
{
    public class DerivativeMessageDoubleHandler : BaseDerivativeMessageHandler, IDerivativeMessageHandler<double>
    {
        public event Action<IntervalBarMessage<double>> IntervalBar;

        protected override bool HasIntervalBar(string msg)
        {
            if (IntervalBarMessage.TryParse(msg, out IntervalBarMessage<double> intervalBarMessage))
            {
                IntervalBar?.Invoke(intervalBarMessage);
                return true;
            }

            return false;
        }
    }
}