using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryDynamicMessage : IUpdateSummaryMessage
    {
        // There is always a Symbol field, so it's reasonable to hard-wire it so that Snapshot layer can use it
        public string Symbol => DynamicFields?.Symbol;
        public double MostRecentTrade => throw new Exception(GetErrorMessage(nameof(MostRecentTrade)));
        public int MostRecentTradeSize => throw new Exception(GetErrorMessage(nameof(MostRecentTradeSize)));
        public TimeSpan MostRecentTradeTime => throw new Exception(GetErrorMessage(nameof(MostRecentTradeTime)));
        public int MostRecentTradeMarketCenter => throw new Exception(GetErrorMessage(nameof(MostRecentTradeMarketCenter)));
        public int TotalVolume => throw new Exception(GetErrorMessage(nameof(TotalVolume)));
        public double Bid => throw new Exception(GetErrorMessage(nameof(Bid)));
        public int BidSize => throw new Exception(GetErrorMessage(nameof(BidSize)));
        public double Ask => throw new Exception(GetErrorMessage(nameof(Ask)));
        public int AskSize => throw new Exception(GetErrorMessage(nameof(AskSize)));
        public double Open => throw new Exception(GetErrorMessage(nameof(Open)));
        public double High => throw new Exception(GetErrorMessage(nameof(High)));
        public double Low => throw new Exception(GetErrorMessage(nameof(Low)));
        public double Close => throw new Exception(GetErrorMessage(nameof(Close)));
        public string MessageContents => throw new Exception(GetErrorMessage(nameof(MessageContents)));
        public string MostRecentTradeConditions => throw new Exception(GetErrorMessage(nameof(MostRecentTradeConditions)));
        public Level1DynamicFields DynamicFields { get; private set; }

        public UpdateSummaryDynamicMessage(Level1DynamicFields dynamicFields)
        {
            DynamicFields = dynamicFields;
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryDynamicMessage message &&
                   DynamicFields == message.DynamicFields;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + DynamicFields.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return DynamicFields.ToString();
        }

        private static string GetErrorMessage(string propertyName) => $"Use {propertyName} from DynamicFields with Level1MessageDynamicHandler.";
    }
}