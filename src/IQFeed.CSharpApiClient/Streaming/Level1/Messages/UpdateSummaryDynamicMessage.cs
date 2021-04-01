using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryDynamicMessage : IUpdateSummaryMessage
    {
        public string Symbol => throw new Exception(GetErrorMessage(nameof(Symbol)));
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

        #region Dynamic Fields

        private readonly string DynamicFieldNotAvailableErrorMessage = $"Dynamic Field is not available. Please use {nameof(ILevel1Client)}.{nameof(ILevel1Client.SelectUpdateFieldName)} to enable!";

        public double SevenDayYield => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double AskChange => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int AskMarketCenter => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public TimeSpan AskTime => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string AvailableRegions => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double AverageMaturity => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double BidChange => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int BidMarketCenter => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public TimeSpan BidTime => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Change => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double ChangeFromOpen => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double CloseRange1 => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double CloseRange2 => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string DaysToExpiration => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string DecimalPrecision => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int Delay => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string ExchangeID => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double ExtendedTrade => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public DateTime ExtendedTradeDate => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int ExtendedTradeMarketCenter => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int ExtendedTradeSize => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public TimeSpan ExtendedTradeTime => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double ExtendedTradingChange => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double ExtendedTradingDifference => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string FinancialStatusIndicator => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string FractionDisplayCode => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Last => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public DateTime LastDate => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int LastMarketCenter => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int LastSize => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public TimeSpan LastTime => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double MarketCapitalization => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int MarketOpen => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public DateTime MostRecentTradeDate => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double NetAssetValue => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int NumberOfTradesToday => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int OpenInterest => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double OpenRange1 => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double OpenRange2 => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double PercentChange => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double PercentOffAverageVolume => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int PreviousDayVolume => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double PriceEarningsRatio => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Range => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string RestrictedCode => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Settle => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public DateTime SettlementDate => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Spread => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int Tick => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public int TickID => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public string Type => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double Volatility => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);
        public double VWAP => throw new NotImplementedException(DynamicFieldNotAvailableErrorMessage);

        #endregion Dynamic Fields

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