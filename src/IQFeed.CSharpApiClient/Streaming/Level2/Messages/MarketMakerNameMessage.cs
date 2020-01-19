using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level2.Messages
{
    public class MarketMakerNameMessage
    {
        public MarketMakerNameMessage(string mmid, string description)
        {
            MMID = mmid;
            Description = description;
        }

        public string MMID { get; private set; }
        public string Description { get; private set; }

        public static MarketMakerNameMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var mmid = values[1];
            var description = values[2];

            return new MarketMakerNameMessage(mmid, description);
        }

        public override bool Equals(object obj)
        {
            return obj is MarketMakerNameMessage message &&
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

        public override string ToString()
        {
            return $"{nameof(MMID)}: {MMID}, {nameof(Description)}: {Description}";
        }
    }
}
