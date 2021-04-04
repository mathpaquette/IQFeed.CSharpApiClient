using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary
{
    public enum MarketSummaryDynamicFieldset
    {
        RequestId,
        Symbol,
        Exchange,   // ExchangeId
        Type,       // SecurityType
        Last,
        TradeSize,
        TradedMarket,
        TradeDate,
        TradeTime,
        Open,
        High,
        Low,
        Close,
        Bid,
        BidMarket,
        BidSize,
        Ask,
        AskMarket,
        AskSize,
        Volume,
        PDayVolume,
        UpVolume,
        DownVolume,
        NeutralVolume,
        TradeCount,
        UpTrades,
        DownTrades,
        NeutralTrades,
        VWAP,
        MutualDiv,
        SevenDayYield,
        OpenInterest,
        Settlement,
        SettlementDate,
        ExpirationDate,
        Strike
    }
}
