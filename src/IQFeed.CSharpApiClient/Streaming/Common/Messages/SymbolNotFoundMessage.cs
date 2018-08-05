using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    public class SymbolNotFoundMessage
    {
        public SymbolNotFoundMessage(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; }

        public static SymbolNotFoundMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            return new SymbolNotFoundMessage(values[1]);
        }

        public override bool Equals(object obj)
        {
            return obj is SymbolNotFoundMessage message &&
                   Symbol == message.Symbol;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return -1758840423 + Symbol.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}";
        }
    }
}