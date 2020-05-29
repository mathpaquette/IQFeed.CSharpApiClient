using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;
using System.Linq;

namespace IQFeed.CSharpApiClient.Streaming.Common.Messages
{
    public class SystemMessage
    {
        public string Type { get; private set; }
        public string Message { get; private set; }

        public SystemMessage(string type, string message)
        {
            Type = type;
            Message = message;
        }
        
        public static SystemMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new SystemMessage(values[1], message);
        }

        public static void ProcessCurrentUpdateFieldnames(string message)
        {
            var fieldNames = message.SplitFeedMessage();
            UpdateSummaryMessage.PrepareDynamicFieldHandlers(fieldNames.Skip(3).ToArray());
        }

        public override bool Equals(object obj)
        {
            return obj is SystemMessage message &&
                   Type == message.Type &&
                   Message == message.Message;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 17;
                hashCode = hashCode * 29 + Type.GetHashCode();
                hashCode = hashCode * 29 + Message.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(Message)}: {Message}";
        }
    }
}