using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1Snapshot : ILevel1Snapshot
    {
        private readonly SocketClient _socketClient;
        private readonly Level1RequestFormatter _level1RequestFormatter;
        private readonly Level1MessageHandler _level1MessageHandler;
        private readonly int _timeoutMs;

        public Level1Snapshot(SocketClient socketClient, Level1RequestFormatter level1RequestFormatter, Level1MessageHandler level1MessageHandler, int timeoutMs)
        {
            _socketClient = socketClient;
            _level1RequestFormatter = level1RequestFormatter;
            _level1MessageHandler = level1MessageHandler;
            _timeoutMs = timeoutMs;
        }

        public Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol)
        {
            return GetFundamentalMessageAsync(symbol);
        }

        public Task<UpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return GetUpdateSummaryMessageAsync(symbol);
        }

        private async Task<FundamentalMessage> GetFundamentalMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<FundamentalMessage>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void Level1ClientOnFundamental(FundamentalMessage fundamentalMessage)
            {
                if (fundamentalMessage.Symbol == symbol)
                    res.TrySetResult(fundamentalMessage);
            }

            _level1MessageHandler.Fundamental += Level1ClientOnFundamental;
            ReqWatch(symbol);

            await res.Task.ContinueWith(x =>
            {
                _level1MessageHandler.Fundamental -= Level1ClientOnFundamental;
                ReqUnwatch(symbol);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }

        private async Task<UpdateSummaryMessage> GetUpdateSummaryMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<UpdateSummaryMessage>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void Level1ClientOnUpdate(UpdateSummaryMessage updateSummaryMessage)
            {
                if (updateSummaryMessage.Symbol == symbol)
                    res.TrySetResult(updateSummaryMessage);
            }

            _level1MessageHandler.Summary += Level1ClientOnUpdate;
            _level1MessageHandler.Update += Level1ClientOnUpdate;
            ReqWatch(symbol);

            await res.Task.ContinueWith(x =>
            {
                _level1MessageHandler.Summary -= Level1ClientOnUpdate;
                _level1MessageHandler.Update -= Level1ClientOnUpdate;
                ReqUnwatch(symbol);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }

        private void ReqWatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqWatch(symbol);
            _socketClient.Send(request);
        }

        private void ReqUnwatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }
    }
}