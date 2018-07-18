using System;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public enum DynamicFieldset
    {
        [FieldsetDescription("7 Day Yield", typeof(float))]
        SevenDayYield,

        [FieldsetDescription("Ask", typeof(float))]
        Ask,

        [FieldsetDescription("Ask Change", typeof(float))]
        AskChange,

        [FieldsetDescription("Ask Market Center", typeof(int))]
        AskMarketCenter,

        [FieldsetDescription("Ask Size", typeof(int))]
        AskSize,

        [FieldsetDescription("Ask Time", typeof(DateTime))]
        AskTime,

        [FieldsetDescription("Available Regions", typeof(string))]
        AvailableRegions,

        [FieldsetDescription("Average Maturity", typeof(float))]
        AverageMaturity,

        [FieldsetDescription("Bid", typeof(float))]
        Bid,

        [FieldsetDescription("Bid Change", typeof(float))]
        BidChange,

        [FieldsetDescription("Bid Market Center", typeof(int))]
        BidMarketCenter,

        [FieldsetDescription("Bid Size", typeof(int))]
        BidSize,

        [FieldsetDescription("Bid Time", typeof(DateTime))]
        BidTime,

        [FieldsetDescription("Change", typeof(float))]
        Change,

        [FieldsetDescription("Change From Open", typeof(float))]
        ChangeFromOpen,

        [FieldsetDescription("Close", typeof(float))]
        Close,

        [FieldsetDescription("Close Range 1", typeof(float))]
        CloseRange1,

        [FieldsetDescription("Close Range 2", typeof(float))]
        CloseRange2,

        [FieldsetDescription("Days to Expiration", typeof(string))]
        DaysToExpiration,

        [FieldsetDescription("Decimal Precision", typeof(string))]
        DecimalPrecision,

        [FieldsetDescription("Delay", typeof(int))]
        Delay,

        [FieldsetDescription("Exchange ID", typeof(string))]
        ExchangeID,

        [FieldsetDescription("Extended Trade", typeof(float))]
        ExtendedTrade,

        [FieldsetDescription("Extended Trade Date", typeof(DateTime))]
        ExtendedTradeDate,

        [FieldsetDescription("Extended Trade Market Center", typeof(int))]
        ExtendedTradeMarketCenter,

        [FieldsetDescription("Extended Trade Size", typeof(int))]
        ExtendedTradeSize,

        [FieldsetDescription("Extended Trade Time", typeof(DateTime))]
        ExtendedTradeTime,

        [FieldsetDescription("Extended Trading Change", typeof(float))]
        ExtendedTradingChange,

        [FieldsetDescription("Extended Trading Difference", typeof(float))]
        ExtendedTradingDifference,

        [FieldsetDescription("Financial Status Indicator", typeof(char))]
        FinancialStatusIndicator,

        [FieldsetDescription("Fraction Display Code", typeof(string))]
        FractionDisplayCode,

        [FieldsetDescription("High", typeof(float))]
        High,

        [FieldsetDescription("Last", typeof(float))]
        Last,

        [FieldsetDescription("Last Date", typeof(DateTime))]
        LastDate,

        [FieldsetDescription("Last Market Center", typeof(int))]
        LastMarketCenter,

        [FieldsetDescription("Last Size", typeof(int))]
        LastSize,

        [FieldsetDescription("Last Time", typeof(DateTime))]
        LastTime,

        [FieldsetDescription("Last Trade Date", typeof(DateTime))]
        LastTradeDate,

        [FieldsetDescription("Low", typeof(float))]
        Low,

        [FieldsetDescription("Market Capitalization", typeof(float))]
        MarketCapitalization,

        [FieldsetDescription("Market Open", typeof(int))]
        MarketOpen,

        [FieldsetDescription("Message Contents", typeof(string))]
        MessageContents,

        [FieldsetDescription("Most Recent Trade", typeof(float))]
        MostRecentTrade,

        [FieldsetDescription("Most Recent Trade Conditions", typeof(string))]
        MostRecentTradeConditions,

        [FieldsetDescription("Most Recent Trade Date", typeof(DateTime))]
        MostRecentTradeDate,

        [FieldsetDescription("Most Recent Trade Market Center", typeof(int))]
        MostRecentTradeMarketCenter,

        [FieldsetDescription("Most Recent Trade Size", typeof(int))]
        MostRecentTradeSize,

        [FieldsetDescription("Most Recent Trade Time", typeof(DateTime))]
        MostRecentTradeTime,

        [FieldsetDescription("Net Asset Value", typeof(float))]
        NetAssetValue,

        [FieldsetDescription("Number of Trades Today", typeof(int))]
        NumberOfTradesToday,

        [FieldsetDescription("Open", typeof(float))]
        Open,

        [FieldsetDescription("Open Interest", typeof(int))]
        OpenInterest,

        [FieldsetDescription("Open Range 1", typeof(float))]
        OpenRange1,

        [FieldsetDescription("Open Range 2", typeof(float))]
        OpenRange2,

        [FieldsetDescription("Percent Change", typeof(float))]
        PercentChange,

        [FieldsetDescription("Percent Off Average Volume", typeof(float))]
        PercentOffAverageVolume,

        [FieldsetDescription("Previous Day Volume", typeof(int))]
        PreviousDayVolume,

        [FieldsetDescription("Price-Earnings Ratio", typeof(float))]
        PriceEarningsRatio,

        [FieldsetDescription("Range", typeof(float))]
        Range,

        [FieldsetDescription("Restricted Code", typeof(string))]
        RestrictedCode,

        [FieldsetDescription("Settle", typeof(float))]
        Settle,

        [FieldsetDescription("Settlement Date", typeof(DateTime))]
        SettlementDate,

        [FieldsetDescription("Spread", typeof(float))]
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

        [FieldsetDescription("Volatility", typeof(float))]
        Volatility,

        [FieldsetDescription("VWAP", typeof(float))]
        VWAP,
    }
}