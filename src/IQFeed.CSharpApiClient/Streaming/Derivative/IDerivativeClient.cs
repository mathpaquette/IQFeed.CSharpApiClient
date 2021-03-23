using System;
using IQFeed.CSharpApiClient.Common.Interfaces;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public interface IDerivativeClient: IClient, IDerivativeEvent
    {
        void SetClientName(string name);

        void ReqBarWatch(string symbol, int interval, DateTime? beginDate = null, int? maxDaysOfDatapoints = null, int? maxDatapoints = null, TimeSpan? beginFilterTime = null, 
            TimeSpan? endFilterTime = null, string requestId = null, DerivativeIntervalType? intervalType = null, int? updateInterval = null);

        void ReqBarUnwatch(string symbol, string requestId);
        void ReqWatches();
        void UnwatchAll();
    }
}