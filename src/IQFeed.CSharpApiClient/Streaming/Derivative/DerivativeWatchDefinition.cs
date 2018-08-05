using System.Collections.Generic;
using System.Text.RegularExpressions;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Derivative
{
    public class DerivativeWatchDefinition
    {
        private const string DerivativeWatchesPattern = "^S,WATCHES,(.*)$";
        private static readonly Regex DerivativeWatchesRegex = new Regex(DerivativeWatchesPattern);

        public DerivativeWatchDefinition(string symbol, string interval, string requestId)
        {
            Symbol = symbol;
            Interval = interval;
            RequestId = requestId;
        }

        public string Symbol { get; }
        public string Interval { get; }
        public string RequestId { get; }

        public static IEnumerable<DerivativeWatchDefinition> Parse(string message)
        {
            var match = DerivativeWatchesRegex.Match(message);
            if (match.Success)
            {
                var watches = match.Groups[1].Value.SplitFeedMessage();
                var offset = 0;

                while (offset < watches.Length)
                {
                    yield return new DerivativeWatchDefinition(watches[offset], watches[offset + 1], watches[offset + 2]);
                    offset += 3;
                }
            }
        }
    }
}