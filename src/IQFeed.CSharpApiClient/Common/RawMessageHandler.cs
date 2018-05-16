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

        public async Task<string> GetStringAsync(string request)
        {
            var client = await _lookupDispatcher.TakeAsync();

            var sb = new StringBuilder();
            var ct = new CancellationTokenSource(_timeoutMs);
            var res = new TaskCompletionSource<string>();
            ct.Token.Register(() => res.TrySetCanceled(), false);

            void SocketClientOnMessageReceived(object sender, SocketMessageEventArgs args)
            {
                var msgs = Encoding.ASCII.GetString(args.Message, 0, args.Count);
                sb.Append(msgs);

                if (msgs.EndsWith("!ENDMSG!,\r\n"))     // TODO: to be put somewhere else.
                    res.TrySetResult(sb.ToString());
            }

            client.MessageReceived += SocketClientOnMessageReceived;
            client.Send(request);

            await res.Task.ContinueWith(x =>
            {
                client.MessageReceived -= SocketClientOnMessageReceived;
                _lookupDispatcher.Add(client);
            }, TaskContinuationOptions.None);

            return await res.Task;
        }
    }
}