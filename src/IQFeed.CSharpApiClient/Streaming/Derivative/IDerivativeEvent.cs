using System;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Derivative.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public interface IDerivativeEvent<T>
    {
        event Action<ErrorMessage> Error;
        event Action<IntervalBarMessage<T>> IntervalBar;
        event Action<SymbolNotFoundMessage> SymbolNotFound;
        event Action<SystemMessage> System;
    }
}