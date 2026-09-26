using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Common;

namespace IQFeed.CSharpApiClient.Tests.Lookup.Common
{
    /// <summary>
    /// Exposes <see cref="BaseLookupFacade"/>'s request path. Each message received is returned as text, the response ends with the first
    /// message carrying the end-of-message marker, and a message starting with the error character is reported as that error.
    /// </summary>
    public class BaseLookupFacadeTestClass : BaseLookupFacade
    {
        public BaseLookupFacadeTestClass(LookupDispatcher lookupDispatcher, LookupRateLimiter lookupRateLimiter, TimeSpan timeout)
            : base(lookupDispatcher, lookupRateLimiter, new ExceptionFactory(), timeout) { }

        public Task<IEnumerable<string>> GetLinesAsync(string request)
        {
            return GetMessagesAsync(request, (message, count) =>
            {
                var text = Encoding.ASCII.GetString(message, 0, count);
                if (text[0] == IQFeedDefault.PrototolErrorCharacter)
                    return new MessageContainer<string>(text.Split(IQFeedDefault.ProtocolDelimiterCharacter)[1], text);

                return new MessageContainer<string>(new[] { text }, text.Contains(IQFeedDefault.ProtocolEndOfMessageCharacters));
            });
        }
    }
}
