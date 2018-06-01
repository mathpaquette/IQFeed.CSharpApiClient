using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Common
{
    public class RawMessageHandler
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly int _timeoutMs;
        private readonly byte[] _endOfMsgBytes;

        public RawMessageHandler(LookupDispatcher lookupDispatcher, int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            _lookupDispatcher = lookupDispatcher;
            _endOfMsgBytes = Encoding.ASCII.GetBytes(IQFeedDefault.ProtocolEndMessage);
        }

        public async Task<string> GetFilenameAsync(string request)
        {
            var client = await _lookupDispatcher.TakeAsync();
            var filename = Path.GetRandomFileName();
            var binaryWriter = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));
            var msgCount = 0;

            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<string>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                // check for errors
                if (msgCount == 0 && args.Message[0] == 'E')
                {
                    var errorMsg = Encoding.ASCII.GetString(args.Message, 0, args.Count);
                    res.TrySetException(new Exception(errorMsg));
                    return;
                }
                
                binaryWriter.Write(args.Message, 0, args.Count);

                // check if the message end
                if (args.Message.EndsWith(args.Count, _endOfMsgBytes))
                    res.TrySetResult(filename);

                msgCount++;
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                binaryWriter.Close();
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
            }, TaskContinuationOptions.None).ConfigureAwait(false);

            return await res.Task.ConfigureAwait(false);
        }
    }
}