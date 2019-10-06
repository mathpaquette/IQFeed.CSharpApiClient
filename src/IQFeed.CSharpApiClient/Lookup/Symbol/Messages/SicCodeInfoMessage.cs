using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class SicCodeInfoMessage
    {
        public SicCodeInfoMessage(int sicCode, string description, string requestId = null)
        {
            RequestId = requestId;
            SicCode = sicCode;
            Description = description;
        }

        public string RequestId { get; }
        public int SicCode { get; }
        public string Description { get; }        

        public static SicCodeInfoMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new SicCodeInfoMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture), 
                values[1]);
        }

        public static SicCodeInfoMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new SicCodeInfoMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],                
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is SicCodeInfoMessage message &&
                   RequestId == message.RequestId &&
                   SicCode == message.SicCode &&
                   Description == message.Description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + SicCode.GetHashCode();
                hash = hash * 29 + Description.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(SicCode)}: {SicCode}, {nameof(Description)}: {Description}";
        }
    }
}