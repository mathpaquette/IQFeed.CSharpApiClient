using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class NameLevelQueryResponseMessage
    {
        public NameLevelQueryResponseMessage(string mmID, string description)
        {
            MMID = mmID;
            Description = description;
        }

        public string MMID { get; }
        public string Description { get; }

        public override string ToString()
        {
            return $"{MMID} {Description}";
        }

        public static NameLevelQueryResponseMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var mmID = values[1];
            var description = values[2];

            return new NameLevelQueryResponseMessage(mmID, description);
        }

        public override bool Equals(object obj)
        {
            return obj is NameLevelQueryResponseMessage message &&
                   MMID == message.MMID &&
                   Description == message.Description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + MMID.GetHashCode();
                hash = hash * 29 + Description.GetHashCode();
                return hash;
            }
        }
    }
}
