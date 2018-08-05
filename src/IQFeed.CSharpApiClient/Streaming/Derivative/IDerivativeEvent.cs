using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public interface IDerivativeEvent
    {
        event Action<ErrorMessage> Error;
        event Action<IntervalBarMessage> IntervalBar;
        event Action<SymbolNotFoundMessage> SymbolNotFound;
        event Action<SystemMessage> System;
    }
}