using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public enum DynamicFieldset
    {
        [FieldsetDescription("7 Day Yield", typeof(double))]
        SevenDayYield,

        [FieldsetDescription("Ask", typeof(double))]
        Ask,

        [FieldsetDescription("Ask Change", typeof(double))]
        AskChange,

        [FieldsetDescription("Ask Market Center", typeof(int))]
        AskMarketCenter,

        [FieldsetDescription("Ask Size", typeof(int))]
        AskSize,

        [FieldsetDescription("Ask Time", typeof(TimeSpan))]
        AskTime,

        [FieldsetDescription("Available Regions", typeof(string))]
        AvailableRegions,

        [FieldsetDescription("Average Maturity", typeof(double))]
        AverageMaturity,

        [FieldsetDescription("Bid", typeof(double))]
        Bid,

        [FieldsetDescription("Bid Change", typeof(double))]
        BidChange,

        [FieldsetDescription("Bid Market Center", typeof(int))]
        BidMarketCenter,

        [FieldsetDescription("Bid Size", typeof(int))]
        BidSize,

        [FieldsetDescription("Bid Time", typeof(TimeSpan))]
        BidTime,

        [FieldsetDescription("Change", typeof(double))]
        Change,

        [FieldsetDescription("Change From Open", typeof(double))]
        ChangeFromOpen,

        [FieldsetDescription("Close", typeof(double))]
        Close,

        [FieldsetDescription("Close Range 1", typeof(double))]
        CloseRange1,

        [FieldsetDescription("Close Range 2", typeof(double))]
        CloseRange2,

        [FieldsetDescription("Days to Expiration", typeof(string))]
        DaysToExpiration,

        [FieldsetDescription("Decimal Precision", typeof(string))]
        DecimalPrecision,

        [FieldsetDescription("Delay", typeof(int))]
        Delay,

        [FieldsetDescription("Exchange ID", typeof(string))]
        ExchangeID,

        [FieldsetDescription("Extended Trade", typeof(double))]
        ExtendedTrade,

        [FieldsetDescription("Extended Trade Date", typeof(DateTime))]
        ExtendedTradeDate,

        [FieldsetDescription("Extended Trade Market Center", typeof(int))]
        ExtendedTradeMarketCenter,

        [FieldsetDescription("Extended Trade Size", typeof(int))]
        ExtendedTradeSize,

        [FieldsetDescription("Extended Trade Time", typeof(TimeSpan))]
        ExtendedTradeTime,

        [FieldsetDescription("Extended Trading Change", typeof(double))]
        ExtendedTradingChange,

        [FieldsetDescription("Extended Trading Difference", typeof(double))]
        ExtendedTradingDifference,

        [FieldsetDescription("Financial Status Indicator", typeof(char))]
        FinancialStatusIndicator,

        [FieldsetDescription("Fraction Display Code", typeof(string))]
        FractionDisplayCode,

        [FieldsetDescription("High", typeof(double))]
        High,

        [FieldsetDescription("Last", typeof(double))]
        Last,

        [FieldsetDescription("Last Date", typeof(DateTime))]
        LastDate,

        [FieldsetDescription("Last Market Center", typeof(int))]
        LastMarketCenter,

        [FieldsetDescription("Last Size", typeof(int))]
        LastSize,

        [FieldsetDescription("Last Time", typeof(TimeSpan))]
        LastTime,

        [FieldsetDescription("Low", typeof(double))]
        Low,

        [FieldsetDescription("Market Capitalization", typeof(double))]
        MarketCapitalization,

        [FieldsetDescription("Market Open", typeof(int))]
        MarketOpen,

        [FieldsetDescription("Message Contents", typeof(string))]
        MessageContents,

        [FieldsetDescription("Most Recent Trade", typeof(double))]
        MostRecentTrade,

        [FieldsetDescription("Most Recent Trade Aggressor", typeof(int))]
        MostRecentTradeAggressor,

        [FieldsetDescription("Most Recent Trade Conditions", typeof(string))]
        MostRecentTradeConditions,

        [FieldsetDescription("Most Recent Trade Date", typeof(DateTime))]
        MostRecentTradeDate,

        [FieldsetDescription("Most Recent Trade Day Code", typeof(int))]
        MostRecentTradeDayCode,

        [FieldsetDescription("Most Recent Trade Market Center", typeof(int))]
        MostRecentTradeMarketCenter,

        [FieldsetDescription("Most Recent Trade Size", typeof(int))]
        MostRecentTradeSize,

        [FieldsetDescription("Most Recent Trade Time", typeof(TimeSpan))]
        MostRecentTradeTime,

        [FieldsetDescription("Net Asset Value", typeof(double))]
        NetAssetValue,

        [FieldsetDescription("Number of Trades Today", typeof(int))]
        NumberOfTradesToday,

        [FieldsetDescription("Open", typeof(double))]
        Open,

        [FieldsetDescription("Open Interest", typeof(int))]
        OpenInterest,

        [FieldsetDescription("Open Range 1", typeof(double))]
        OpenRange1,

        [FieldsetDescription("Open Range 2", typeof(double))]
        OpenRange2,

        [FieldsetDescription("Percent Change", typeof(double))]
        PercentChange,

        [FieldsetDescription("Percent Off Average Volume", typeof(double))]
        PercentOffAverageVolume,

        [FieldsetDescription("Previous Day Volume", typeof(int))]
        PreviousDayVolume,

        [FieldsetDescription("Price-Earnings Ratio", typeof(double))]
        PriceEarningsRatio,

        [FieldsetDescription("Range", typeof(double))]
        Range,

        [FieldsetDescription("Restricted Code", typeof(string))]
        RestrictedCode,

        [FieldsetDescription("Settle", typeof(double))]
        Settle,

        [FieldsetDescription("Settlement Date", typeof(DateTime))]
        SettlementDate,

        [FieldsetDescription("Spread", typeof(double))]
        Spread,

        [FieldsetDescription("Symbol", typeof(string))]
        Symbol,

        [FieldsetDescription("Tick", typeof(int))]
        Tick,

        [FieldsetDescription("TickID", typeof(int))]
        TickID,

        [FieldsetDescription("Total Volume", typeof(int))]
        TotalVolume,

        [FieldsetDescription("Type", typeof(string))]
        Type,

        [FieldsetDescription("Volatility", typeof(double))]
        Volatility,

        [FieldsetDescription("VWAP", typeof(double))]
        VWAP,
    }
}