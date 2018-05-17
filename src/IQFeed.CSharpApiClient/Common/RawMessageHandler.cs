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
            var filename = Path.GetRandomFileName();
            var bw = new BinaryWriter(File.Open(filename, FileMode.OpenOrCreate));

            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<string>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                bw.Write(args.Message, 0, args.Count);

                var msgs = Encoding.ASCII.GetString(args.Message, 0, args.Count);       // TODO: should avoid string conversion
                if (msgs.EndsWith("!ENDMSG!,\r\n"))                                     // TODO: to be put somewhere else.
                    res.TrySetResult(filename);
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                bw.Close();
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
            }, TaskContinuationOptions.None);

            return await res.Task;
        }
    }
}