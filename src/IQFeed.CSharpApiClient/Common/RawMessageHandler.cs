using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient.Common
{
    public class RawMessageHandler
    {
        private readonly LookupDispatcher _lookupDispatcher;
        private readonly int _timeoutMs;

        public RawMessageHandler(LookupDispatcher lookupDispatcher, int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            _lookupDispatcher = lookupDispatcher;
        }

        public async Task<string> GetFilenameAsync(string request)
        {
            var client = await _lookupDispatcher.TakeAsync();
            var count = 0;
            var filename = Path.GetRandomFileName();
            var binaryWriter = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));

            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<string>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var msg = Encoding.ASCII.GetString(args.Message, 0, args.Count);       // TODO: should avoid string conversion
                if (count == 0 && msg[0] == 'E')
                    res.TrySetException(new Exception(msg));

                binaryWriter.Write(args.Message, 0, args.Count);

                if (msg.EndsWith("!ENDMSG!,\r\n"))                                     // TODO: to be put somewhere else.
                    res.TrySetResult(filename);

                count++;
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                binaryWriter.Close();
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
            }, TaskContinuationOptions.None);

            return await res.Task;
        }
    }
}