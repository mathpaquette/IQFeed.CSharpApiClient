using System;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common.Exceptions;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Common.Messages;
using IQFeed.CSharpApiClient.Streaming.Level1.Handlers;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1Snapshot : ILevel1Snapshot
    {
        private readonly SocketClient _socketClient;
        private readonly Level1RequestFormatter _level1RequestFormatter;
        private readonly ILevel1MessageHandler _level1MessageHandler;
        private readonly TimeSpan _timeout;

        public Level1Snapshot(
            SocketClient socketClient, 
            Level1RequestFormatter level1RequestFormatter, 
            ILevel1MessageHandler level1MessageHandler,
            TimeSpan timeout)
        {
            _timeout = timeout;
            _socketClient = socketClient;
            _level1RequestFormatter = level1RequestFormatter;
            _level1MessageHandler = level1MessageHandler;
        }

        public Task<FundamentalMessage> GetFundamentalSnapshotAsync(string symbol)
        {
            return GetFundamentalMessageAsync(symbol);
        }

        public Task<IUpdateSummaryMessage> GetUpdateSummarySnapshotAsync(string symbol)
        {
            return GetUpdateSummaryMessageAsync(symbol);
        }

        public FundamentalMessage GetFundamentalSnapshot(string symbol)
        {
            return GetFundamentalSnapshotAsync(symbol).SynchronouslyAwaitTaskResult();
        }

        public IUpdateSummaryMessage GetUpdateSummarySnapshot(string symbol)
        {
            return GetUpdateSummarySnapshotAsync(symbol).SynchronouslyAwaitTaskResult();
        }

        private async Task<FundamentalMessage> GetFundamentalMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeout);
            var res = new TaskCompletionSource<FundamentalMessage>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            var reqWatchRequest = CreateReqWatchRequest(symbol);

            void Level1ClientOnFundamental(FundamentalMessage fundamentalMessage)
            {
                if (fundamentalMessage.Symbol == symbol)
                    res.TrySetResult(fundamentalMessage);
            }

            void Level1ClientOnSymbolNotFound(SymbolNotFoundMessage symbolNotFoundMessage)
            {
                if (symbolNotFoundMessage.Symbol == symbol)
                    res.TrySetException(new SymbolNotFoundIQFeedException(reqWatchRequest, symbol));
            }

            _level1MessageHandler.Fundamental += Level1ClientOnFundamental;
            _level1MessageHandler.SymbolNotFound += Level1ClientOnSymbolNotFound;
            SendReqWatchRequest(reqWatchRequest);

            await res.Task.ContinueWith(x =>
            {
                _level1MessageHandler.Fundamental -= Level1ClientOnFundamental;
                _level1MessageHandler.SymbolNotFound -= Level1ClientOnSymbolNotFound;
                ReqUnwatch(symbol);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }

        private async Task<IUpdateSummaryMessage> GetUpdateSummaryMessageAsync(string symbol)
        {
            var ct = new CancellationTokenSource(_timeout);
            var res = new TaskCompletionSource<IUpdateSummaryMessage>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            var reqWatchRequest = CreateReqWatchRequest(symbol);

            void Level1ClientOnUpdate(IUpdateSummaryMessage updateSummaryMessage)
            {
                if (updateSummaryMessage.Symbol == symbol)
                    res.TrySetResult(updateSummaryMessage);
            }

            void Level1ClientOnSymbolNotFound(SymbolNotFoundMessage symbolNotFoundMessage)
            {
                if (symbolNotFoundMessage.Symbol == symbol)
                    res.TrySetException(new SymbolNotFoundIQFeedException(reqWatchRequest, symbol));
            }

            _level1MessageHandler.Summary += Level1ClientOnUpdate;
            _level1MessageHandler.Update += Level1ClientOnUpdate;
            _level1MessageHandler.SymbolNotFound += Level1ClientOnSymbolNotFound;
            SendReqWatchRequest(reqWatchRequest);

            await res.Task.ContinueWith(x =>
            {
                _level1MessageHandler.Summary -= Level1ClientOnUpdate;
                _level1MessageHandler.Update -= Level1ClientOnUpdate;
                _level1MessageHandler.SymbolNotFound -= Level1ClientOnSymbolNotFound;
                ReqUnwatch(symbol);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }

        private string CreateReqWatchRequest(string symbol)
        {
            return _level1RequestFormatter.ReqWatch(symbol);            
        }

        private void SendReqWatchRequest(string request)
        {
            _socketClient.Send(request);
        }

        private void ReqUnwatch(string symbol)
        {
            var request = _level1RequestFormatter.ReqUnwatch(symbol);
            _socketClient.Send(request);
        }
    }
}