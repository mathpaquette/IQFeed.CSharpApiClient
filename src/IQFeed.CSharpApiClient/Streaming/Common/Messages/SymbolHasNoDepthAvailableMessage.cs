using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    public class SymbolHasNoDepthAvailableMessage
{
        public SymbolHasNoDepthAvailableMessage(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; private set; }

        public static SymbolHasNoDepthAvailableMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            return new SymbolHasNoDepthAvailableMessage(values[1]);
        }

        public override bool Equals(object obj)
        {
            return obj is SymbolHasNoDepthAvailableMessage message &&
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