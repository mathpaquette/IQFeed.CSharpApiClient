using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level2.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2Snapshot<T> : ILevel2Snapshot<T>
    {
        private readonly SocketClient _socketClient;
        private readonly Level2RequestFormatter _level2RequestFormatter;
        private readonly ILevel2MessageHandler<T> _level2MessageHandler;
        private readonly int _timeoutMs;

        public Level2Snapshot(
            SocketClient socketClient, 
            Level2RequestFormatter level2RequestFormatter,
            ILevel2MessageHandler<T> level2MessageHandler, 
            int timeoutMs)
        {
            _socketClient = socketClient;
            _level2RequestFormatter = level2RequestFormatter;
            _level2MessageHandler = level2MessageHandler;
            _timeoutMs = timeoutMs;
        }

        public Task<IEnumerable<UpdateSummaryMessage<T>>> GetSummarySnapshotAsync(string symbol)
        {
            return GetSummaryMessageAsync(symbol);
        }

        private async Task<IEnumerable<UpdateSummaryMessage<T>>> GetSummaryMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<IEnumerable<UpdateSummaryMessage<T>>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            var summaryMessages = new List<UpdateSummaryMessage<T>>();

            void Level2ClientOnSummary(UpdateSummaryMessage<T> updateSummaryMessage)
            {
                if (updateSummaryMessage.Symbol == symbol)
                {
                    summaryMessages.Add(updateSummaryMessage);
                }
            }

            void Level2ClientOnTimestamp(TimestampMessage timestampMessage)
            {
                // summary messages are received sequentially in a batch meaning that they won't interfere with other messages
                // we can use this assumption and complete the receive process when at least one summary message has been received.
                if (summaryMessages.Count > 0)
                {
                    res.TrySetResult(summaryMessages);
                }
            }

            _level2MessageHandler.Summary += Level2ClientOnSummary;
            _level2MessageHandler.Timestamp += Level2ClientOnTimestamp;
            ReqWatch(symbol);

            await res.Task.ContinueWith(x =>
            {
                ReqUnwatch(symbol);
                _level2MessageHandler.Timestamp -= Level2ClientOnTimestamp;
                _level2MessageHandler.Summary -= Level2ClientOnSummary;
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }

        private void ReqWatch(string symbol)
        {
            var request = _level2RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        private void ReqUnwatch(string symbol)
        {
            var request = _level2RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }
    }
}