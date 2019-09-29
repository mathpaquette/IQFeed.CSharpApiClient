using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class NiacCodeInfoMessage : INiacCodeInfoMessage
    {
        public NiacCodeInfoMessage(int niacCode, string description, string requestId = null)
        {
            RequestId = requestId;
            NiacCode = niacCode;
            Description = description;
        }

        public string RequestId { get; }
        public int NiacCode { get; }
        public string Description { get; }        

        public static NiacCodeInfoMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new NiacCodeInfoMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture), 
                values[1]);
        }

        public static NiacCodeInfoMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new NiacCodeInfoMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],                
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is NiacCodeInfoMessage message &&
                   RequestId == message.RequestId &&
                   NiacCode == message.NiacCode &&
                   Description == message.Description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + NiacCode.GetHashCode();
                hash = hash * 29 + Description.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{Description}({NiacCode})";
        }
    }
}