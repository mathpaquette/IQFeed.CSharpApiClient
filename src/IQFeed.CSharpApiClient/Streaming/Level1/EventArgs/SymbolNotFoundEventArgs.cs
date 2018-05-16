using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1.EventArgs
{
    public class SymbolNotFoundEventArgs : System.EventArgs
    {
        public SymbolNotFoundEventArgs(SymbolNotFoundMessage symbolNotFoundMessage)
        {
            SymbolNotFoundMessage = symbolNotFoundMessage;
        }

        public SymbolNotFoundMessage SymbolNotFoundMessage { get; }
    }
}