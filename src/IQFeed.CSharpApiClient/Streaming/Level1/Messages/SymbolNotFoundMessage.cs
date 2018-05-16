namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class SymbolNotFoundMessage
    {
        public SymbolNotFoundMessage(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }
    }
}