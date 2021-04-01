﻿using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public interface IUpdateSummaryDynamicMessage : IUpdateSummaryMessage
    {
        double SevenDayYield { get; }
        double AskChange { get; }
        int AskMarketCenter { get; }
        TimeSpan AskTime { get; }
        string AvailableRegions { get; }
        double AverageMaturity { get; }
        double BidChange { get; }
        int BidMarketCenter { get; }
        TimeSpan BidTime { get; }
        double Change { get; }
        double ChangeFromOpen { get; }
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
        double Last { get; }
        DateTime LastDate { get; }
        int LastMarketCenter { get; }
        int LastSize { get; }
        TimeSpan LastTime { get; }
        double MarketCapitalization { get; }
        int MarketOpen { get; }
        DateTime MostRecentTradeDate { get; }
        double NetAssetValue { get; }
        int NumberOfTradesToday { get; }
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
        int Tick { get; }
        int TickID { get; }
        string Type { get; }
        double Volatility { get; }
        double VWAP { get; }
    }
}