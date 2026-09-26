using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Lookup.Common
{
    public class LookupMessageFileHandler : BaseLookupMessageHandler
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly LookupRateLimiter _lookupRateLimiter;
        private readonly ExceptionFactory _exceptionFactory;

        private readonly TimeSpan _timeout;
        private readonly byte[] _endOfMsgBytes;

        public LookupMessageFileHandler(
            LookupDispatcher lookupDispatcher, 
            LookupRateLimiter lookupRateLimiter, 
            ExceptionFactory exceptionFactory, 
            TimeSpan timeout)
        {
            _endOfMsgBytes = Encoding.ASCII.GetBytes(IQFeedDefault.ProtocolEndOfMessageCharacters + IQFeedDefault.ProtocolDelimiterCharacter + IQFeedDefault.ProtocolTerminatingCharacters);

            _lookupDispatcher = lookupDispatcher;
            _lookupRateLimiter = lookupRateLimiter;
            _exceptionFactory = exceptionFactory;
            _timeout = timeout;
        }

        // TODO(mathip): add support for requestId parsing.
        public async Task<string> GetFilenameAsync(string request)
        {
            // the timeout covers waiting for a free socket as well as the reply
            using (var ct = new CancellationTokenSource(_timeout))
            {
                var client = await _lookupDispatcher.TakeAsync(ct.Token).ConfigureAwait(false);
                return await GetFilenameAsync(client, request, ct.Token).ConfigureAwait(false);
            }
        }

        private async Task<string> GetFilenameAsync(SocketClient client, string request, CancellationToken timeout)
        {
            var filename = Path.GetRandomFileName();
            BinaryWriter binaryWriter = null;
            var writerGate = new object(); // the socket thread writes while a timeout or a disconnect may be closing the file
            // completed from timer, disconnect and socket threads, so callers' continuations must never run on them
            var res = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                // check for errors
                if (args.Message[0] == IQFeedDefault.PrototolErrorCharacter && args.Message[1] == IQFeedDefault.ProtocolDelimiterCharacter)
                {
                    // at this level, we might have true negative, further checks needed
                    var received = Encoding.ASCII.GetString(args.Message, 0, args.Count);
                    var messages = received.SplitFeedLine();
                    var errorMessage = ParseErrorMessage(messages);

                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        // error has been confirmed
                        res.TrySetException(_exceptionFactory.CreateNew(request, errorMessage, received));
                        return;
                    }
                }
                
                lock (writerGate)
                {
                    // null once the request is over: whatever still arrives belongs to nobody
                    if (binaryWriter == null)
                        return;

                    binaryWriter.Write(args.Message, 0, args.Count);
                }

                // check if the message end
                if (args.Message.EndsWith(args.Count, _endOfMsgBytes))
                    res.TrySetResult(filename);
            }

            using (timeout.Register(() => res.TrySetCanceled(), false))
            using (_lookupDispatcher.Disconnected.Register(() => res.TrySetException(new ObjectDisposedException(nameof(LookupDispatcher), "The lookup client is disconnected.")), false))
            {
                var sent = false;
                var replace = false;
                var completed = false;
                try
                {
                    lock (writerGate)
                        binaryWriter = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));

                    client.MessageReceived += SocketClientOnMessageReceived;

                    // a request that timed out or was disconnected before it was sent leaves the socket clean
                    bool slotTaken;
                    using (var timeoutOrDisconnect = CancellationTokenSource.CreateLinkedTokenSource(timeout, _lookupDispatcher.Disconnected))
                        slotTaken = await _lookupRateLimiter.TryWaitAsync(timeoutOrDisconnect.Token).ConfigureAwait(false);

                    if (slotTaken && !res.Task.IsCompleted)
                    {
                        try
                        {
                            client.Send(request);
                            sent = true;
                        }
                        catch (SocketException)
                        {
                            replace = true; // the socket is dead
                            throw;
                        }
                    }

                    var result = await res.Task.ConfigureAwait(false);
                    completed = true;
                    return result;
                }
                catch (TaskCanceledException) when (sent)
                {
                    // IQFeed may still send the rest of the abandoned response on this socket
                    replace = true;
                    throw;
                }
                finally
                {
                    client.MessageReceived -= SocketClientOnMessageReceived;
                    if (replace)
                        _lookupDispatcher.Replace(client);
                    else
                        _lookupDispatcher.Add(client);

                    lock (writerGate)
                    {
                        binaryWriter?.Close();
                        binaryWriter = null;
                    }

                    if (!completed && File.Exists(filename))
                        File.Delete(filename);
                }
            }
        }
    }
}