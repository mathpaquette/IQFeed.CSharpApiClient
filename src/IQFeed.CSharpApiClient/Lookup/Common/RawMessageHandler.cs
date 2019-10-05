﻿using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public class RawMessageHandler : BaseLookupMessageHandler
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly ErrorMessageHandler _errorMessageHandler;

        private readonly int _timeoutMs;
        private readonly byte[] _endOfMsgBytes;

        public RawMessageHandler(LookupDispatcher lookupDispatcher, ErrorMessageHandler errorMessageHandler, int timeoutMs)
        {
            _endOfMsgBytes = Encoding.ASCII.GetBytes(IQFeedDefault.ProtocolEndOfMessageCharacters + IQFeedDefault.ProtocolDelimiterCharacter + IQFeedDefault.ProtocolTerminatingCharacters);

            _lookupDispatcher = lookupDispatcher;
            _errorMessageHandler = errorMessageHandler;
            _timeoutMs = timeoutMs;
        }

        // TODO(mathip): add support for requestId parsing.
        public async Task<string> GetFilenameAsync(string request)
        {
            var client = await _lookupDispatcher.TakeAsync();
            var filename = Path.GetRandomFileName();
            var binaryWriter = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));

            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<string>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                // check for errors
                if (args.Message[0] == IQFeedDefault.PrototolErrorCharacter && args.Message[1] == IQFeedDefault.ProtocolDelimiterCharacter)
                {
                    // at this level, we might have true negative, further checks needed
                    var messages = Encoding.ASCII.GetString(args.Message, 0, args.Count).SplitFeedLine();
                    var errorMessage = ParseErrorMessage(messages);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        // error has been confirmed
                        res.TrySetException(_errorMessageHandler.GetException(errorMessage));
                        return;
                    }
                }
                
                binaryWriter.Write(args.Message, 0, args.Count);

                // check if the message end
                if (args.Message.EndsWith(args.Count, _endOfMsgBytes))
                    res.TrySetResult(filename);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                binaryWriter.Close();
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
                ct.Dispose();
                if (res.Task.IsFaulted)
                    File.Delete(filename);
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }
    }
}