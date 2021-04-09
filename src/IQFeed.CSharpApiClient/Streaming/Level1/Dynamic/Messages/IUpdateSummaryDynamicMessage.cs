using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Dynamic.Messages
{
    public interface IUpdateSummaryDynamicMessage
    {
        double SevenDayYield { get; }
        double Ask { get; }
        double AskChange { get; }
        int AskMarketCenter { get; }
        int AskSize { get; }
        TimeSpan AskTime { get; }
        string AvailableRegions { get; }
        double AverageMaturity { get; }
        double Bid { get; }
        double BidChange { get; }
        int BidMarketCenter { get; }
        int BidSize { get; }
        TimeSpan BidTime { get; }
        double Change { get; }
        double ChangeFromOpen { get; }
        double Close { get; }
        double CloseRange1 { get; }
        double CloseRange2 { get; }
        string DaysToExpiration { get; }
        string DecimalPrecision { get; }
        int Delay { get; }
        string ExchangeID { get; }
        double ExtendedTrade { get; }
        DateTime ExtendedTradeDate { get; }
        int ExtendedTradeMarketCenter { get; }
        int ExtendedTradeSize { get; }
        TimeSpan ExtendedTradeTime { get; }
        double ExtendedTradingChange { get; }
        double ExtendedTradingDifference { get; }
        string FinancialStatusIndicator { get; }
        string FractionDisplayCode { get; }
        double High { get; }
        double Last { get; }
        DateTime LastDate { get; }
        int LastMarketCenter { get; }
        int LastSize { get; }
        TimeSpan LastTime { get; }
        double Low { get; }
        double MarketCapitalization { get; }
        int MarketOpen { get; }
        string MessageContents { get; }
        double MostRecentTrade { get; }
        int MostRecentTradeAggressor { get; }
        string MostRecentTradeConditions { get; }
        DateTime MostRecentTradeDate { get; }
        int MostRecentTradeDayCode { get; }
        int MostRecentTradeMarketCenter { get; }
        int MostRecentTradeSize { get; }
        TimeSpan MostRecentTradeTime { get; }
        double NetAssetValue { get; }
        int NumberOfTradesToday { get; }
        double Open { get; }
        int OpenInterest { get; }
        double OpenRange1 { get; }
        double OpenRange2 { get; }
        double PercentChange { get; }
        double PercentOffAverageVolume { get; }
        int PreviousDayVolume { get; }
        double PriceEarningsRatio { get; }
        double Range { get; }
        string RestrictedCode { get; }
        double Settle { get; }
        DateTime SettlementDate { get; }
        double Spread { get; }
        string Symbol { get; }
        int Tick { get; }
        int TickID { get; }
        int TotalVolume { get; }
        string Type { get; }
        double Volatility { get; }
        double VWAP { get; }
    }
}