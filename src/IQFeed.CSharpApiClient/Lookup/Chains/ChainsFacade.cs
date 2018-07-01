﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup.Chains.Messages;
using IQFeed.CSharpApiClient.Lookup.Chains.Options;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup.Chains
{
    public class ChainsFacade : IChainsFacade
    {
        private readonly ChainsRequestFormatter _chainsRequestFormatter;
        private readonly ChainsMessageHandler _chainsMessageHandler;
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly int _timeoutMs;

        public ChainsFacade(ChainsRequestFormatter chainsRequestFormatter, ChainsMessageHandler chainsMessageHandler, LookupDispatcher lookupDispatcher, int timeoutMs)
        {
            _chainsMessageHandler = chainsMessageHandler;
            _timeoutMs = timeoutMs;
            _lookupDispatcher = lookupDispatcher;
            _chainsRequestFormatter = chainsRequestFormatter;
        }

        public Task<IEnumerable<FutureMessage>> ReqChainFutureAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFuture(symbol, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureMessages);
        }

        public Task<IEnumerable<FutureSpreadMessage>> ReqChainFutureSpreadsAsync(string symbol, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureSpreads(symbol, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureSpreadMessages);
        }

        public Task<IEnumerable<FutureOptionMessage>> ReqChainFutureOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, string years, int? nearMonths = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainFutureOption(symbol, optionSideFilter, monthCodes, years, nearMonths, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetFutureOptionMessages);
        }

        public Task<IEnumerable<EquityOptionMessage>> ReqChainIndexEquityOptionAsync(string symbol, OptionSideFilterType optionSideFilter, string monthCodes, int? nearMonths = null, BinaryOptionFilterType binaryOptionFilter = BinaryOptionFilterType.Include,
            OptionFilterType optionFilter = OptionFilterType.None, int? filterValue1 = null, int? filterValue2 = null, string requestId = null)
        {
            var request = _chainsRequestFormatter.ReqChainIndexEquityOption(symbol, optionSideFilter, monthCodes, nearMonths, binaryOptionFilter, optionFilter, filterValue1, filterValue2, requestId);
            return GetMessagesAsync(request, _chainsMessageHandler.GetEquityOptionMessages);
        }

        // TODO: combine this method with Historical
        private async Task<IEnumerable<T>> GetMessagesAsync<T>(string request, Func<byte[], int, ChainsMessageContainer<T>> chainsMessageHandler)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var messages = new List<T>();
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<IEnumerable<T>>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var container = chainsMessageHandler(args.Message, args.Count);

                if (messages.Count == 0 && container.Error != null)
                {
                    // TODO: should throw specific exception here
                    res.TrySetException(new Exception(container.Error));
                    return;
                }

                messages.AddRange(container.Messages);

                if (container.End)
                    res.TrySetResult(messages);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
                ct.Dispose();
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }
    }
}