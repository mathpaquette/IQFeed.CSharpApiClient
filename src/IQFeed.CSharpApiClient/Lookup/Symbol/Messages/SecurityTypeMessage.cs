using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SecurityTypeMessage
    {
        public SecurityTypeMessage(int securityTypeId, string shortName, string longName, string requestId = null)
        {
            RequestId = requestId;
            SecurityTypeId = securityTypeId;
            ShortName = shortName;
            LongName = longName;
        }

        public string RequestId { get; private set; }
        public int SecurityTypeId { get; private set; }
        public string ShortName { get; private set; }
        public string LongName { get; private set; }

        public static SecurityTypeMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new SecurityTypeMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture), 
                values[1], 
                values[2]);
        }

        public static SecurityTypeMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new SecurityTypeMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                values[3],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is SecurityTypeMessage message &&
                   RequestId == message.RequestId &&
                   SecurityTypeId == message.SecurityTypeId &&
                   ShortName == message.ShortName &&
                   LongName == message.LongName;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + SecurityTypeId.GetHashCode();
                hash = hash * 29 + ShortName.GetHashCode();
                hash = hash * 29 + LongName.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(SecurityTypeId)}: {SecurityTypeId}, {nameof(ShortName)}: {ShortName}, {nameof(LongName)}: {LongName}";
        }
    }
}