using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level2.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public class Level2Snapshot : ILevel2Snapshot
    {
        private readonly SocketClient _socketClient;
        private readonly Level2RequestFormatter _level2RequestFormatter;
        private readonly Level2MessageHandler _level2MessageHandler;
        private readonly int _timeoutMs;

        public Level2Snapshot(SocketClient socketClient, Level2RequestFormatter level2RequestFormatter, Level2MessageHandler level2MessageHandler, int timeoutMs)
        {
            _socketClient = socketClient;
            _level2RequestFormatter = level2RequestFormatter;
            _level2MessageHandler = level2MessageHandler;
            _timeoutMs = timeoutMs;
        }

        public Task<UpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return GetUpdateSummaryMessageAsync(symbol);
        }

        private async Task<UpdateSummaryMessage> GetUpdateSummaryMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<UpdateSummaryMessage>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void Level2ClientOnUpdate(UpdateSummaryMessage updateSummaryMessage)
            {
                if (updateSummaryMessage.Symbol == symbol)
                    res.TrySetResult(updateSummaryMessage);
            }

            _level2MessageHandler.Summary += Level2ClientOnUpdate;
            _level2MessageHandler.Update += Level2ClientOnUpdate;
            ReqWatch(symbol);

            await res.Task.ContinueWith(x =>
            {
                _level2MessageHandler.Summary -= Level2ClientOnUpdate;
                _level2MessageHandler.Update -= Level2ClientOnUpdate;
                ReqUnwatch(symbol);
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