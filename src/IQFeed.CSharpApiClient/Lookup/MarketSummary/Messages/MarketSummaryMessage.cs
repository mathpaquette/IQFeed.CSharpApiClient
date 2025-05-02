using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.Symbol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Lookup.MarketSummary.Messages
{
    /// <summary>
    /// Market Summaries must be treated as dynamic fields, and parsed into what we think we know exists.
    /// This doesn't currently entirely conform to the way Dynamic Fields are handled elsewhere
    /// but this was written to align with an alternate implementation of DynamicFields
    /// before there was an official version.
    /// As of now, it is also assumed that the protocol version is 6.2 to handle the addition constant field "LM"
    /// </summary>
    public class MarketSummaryMessage
    {
        public const string MarketSummaryDataId = "LM";


        public MarketSummaryMessage(string[] values, MarketSummaryHandler marketSummaryHandler)
        {
            for (var index = 0; index < values.Length; index++)
            {
                switch (marketSummaryHandler.FieldNames[index])
                {
                    case MarketSummaryDynamicFieldset.RequestId:
                        RequestId = (string)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.RequestId].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.LM:
                        // don't need this. Ignore it!
                        break;

                    case MarketSummaryDynamicFieldset.Symbol:
                        Symbol = (string)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Symbol].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Exchange:
                        ExchangeId = (int)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Exchange].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Type:
                        SecurityType = (SecurityType)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Type].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Last:
                        Last = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Last].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.TradeSize:
                        TradeSize = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.TradeSize].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.TradedMarket:
                        TradedMarket = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.TradedMarket].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.TradeDate:
                        TradeDate = (DateTime?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.TradeDate].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.TradeTime:
                        TradeTime = (TimeSpan?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.TradeTime].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Open:
                        Open = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Open].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.High:
                        High = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.High].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Low:
                        Low = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Low].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Close:
                        Close = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Close].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Bid:
                        Bid = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Bid].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.BidMarket:
                        BidMarket = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.BidMarket].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.BidSize:
                        BidSize = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.BidSize].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Ask:
                        Ask = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Ask].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.AskMarket:
                        AskMarket = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.AskMarket].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.AskSize:
                        AskSize = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.AskSize].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Volume:
                        Volume = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Volume].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.PDayVolume:
                        PDayVolume = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.PDayVolume].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.UpVolume:
                        UpVolume = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.UpVolume].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.DownVolume:
                        DownVolume = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.DownVolume].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.NeutralVolume:
                        NeutralVolume = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.NeutralVolume].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.TradeCount:
                        TradeCount = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.TradeCount].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.UpTrades:
                        UpTrades = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.UpTrades].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.DownTrades:
                        DownTrades = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.DownTrades].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.NeutralTrades:
                        NeutralTrades = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.NeutralTrades].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.VWAP:
                        VWAP = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.VWAP].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.MutualDiv:
                        MutualDiv = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.MutualDiv].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.SevenDayYield:
                        SevenDayYield = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.SevenDayYield].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.OpenInterest:
                        OpenInterest = (int?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.OpenInterest].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Settlement:
                        Settlement = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Settlement].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.SettlementDate:
                        SettlementDate = (DateTime?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.SettlementDate].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.ExpirationDate:
                        ExpirationDate = (DateTime?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.ExpirationDate].Invoke(values[index]);
                        break;

                    case MarketSummaryDynamicFieldset.Strike:
                        Strike = (double?)marketSummaryHandler.FieldConvertors[(int)MarketSummaryDynamicFieldset.Strike].Invoke(values[index]);
                        break;
                }
            }
        }

        public MarketSummaryMessage(string symbol, int exchangeId, int type, double? last, int? tradeSize,
            int? tradedMarket, DateTime? tradeDate, TimeSpan? tradeTime, double? open, double? high, double? low, double? close,
            double? bid, int? bidMarket, int? bidSize, double? ask, int? askMarket, int? askSize, int? volume, int? pDayVolume,
            int? upVolume, int? downVolume, int? neutralVolume, int? tradeCount, int? upTrades, int? downTrades,
            int? neutralTrades, double? vwap, double? mutualDiv, double? sevenDayYield, int? openInterest,
            double? settlement, DateTime? settlementDate, DateTime? expirationDate, double? strike,
            string requestId = null)
        {
            RequestId = requestId;
            Symbol = symbol;
            ExchangeId = exchangeId;
            SecurityType = (SecurityType)type;
            Last = last;
            TradeSize = tradeSize;
            TradedMarket = tradedMarket;
            TradeDate = tradeDate;
            TradeTime = tradeTime;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Bid = bid;
            BidMarket = bidMarket;
            BidSize = bidSize;
            Ask = ask;
            AskMarket = askMarket;
            AskSize = askSize;
            Volume = volume;
            PDayVolume = pDayVolume;
            UpVolume = upVolume;
            DownVolume = downVolume;
            NeutralVolume = neutralVolume;
            TradeCount = tradeCount;
            UpTrades = upTrades;
            DownTrades = downTrades;
            NeutralTrades = neutralTrades;
            VWAP = vwap;
            MutualDiv = mutualDiv;
            SevenDayYield = sevenDayYield;
            OpenInterest = openInterest;
            Settlement = settlement;
            SettlementDate = settlementDate;
            ExpirationDate = expirationDate;
            Strike = strike;
        }

        public string RequestId { get; private set; }
        public string Symbol { get; private set; }
        public int ExchangeId { get; private set; }
        public SecurityType SecurityType { get; private set; }
        public double? Last { get; private set; }
        public int? TradeSize { get; private set; }
        public int? TradedMarket { get; private set; }
        public DateTime? TradeDate { get; private set; }
        public TimeSpan? TradeTime { get; private set; }
        public double? Open { get; private set; }
        public double? High { get; private set; }
        public double? Low { get; private set; }
        public double? Close { get; private set; }
        public double? Bid { get; private set; }
        public int? BidMarket { get; private set; }
        public int? BidSize { get; private set; }
        public double? Ask { get; private set; }
        public int? AskMarket { get; private set; }
        public int? AskSize { get; private set; }
        public int? Volume { get; private set; }
        public int? PDayVolume { get; private set; }
        public int? UpVolume { get; private set; }
        public int? DownVolume { get; private set; }
        public int? NeutralVolume { get; private set; }
        public int? TradeCount { get; private set; }
        public int? UpTrades { get; private set; }
        public int? DownTrades { get; private set; }
        public int? NeutralTrades { get; private set; }
        public double? VWAP { get; private set; }
        public double? MutualDiv { get; private set; }
        public double? SevenDayYield { get; private set; }
        public int? OpenInterest { get; private set; }
        public double? Settlement { get; private set; }
        public DateTime? SettlementDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public double? Strike { get; private set; }

        // Utility Accessors
        public DateTime? TradeDateTime => (TradeDate != null && TradeTime != null) ? TradeDate.Value.Add(TradeTime.Value) : (DateTime?)null;

        public static MarketSummaryMessage Parse(string message, MarketSummaryHandler marketSummaryHandler)
        {
            if (marketSummaryHandler.FieldNames.Count == 0)
            {
                // The first line received will be field names, which will tell us what we're getting and in what order
                foreach (var fieldName in message.SplitFeedMessage())
                {
                    // Work out which field we're dealing with, and then add it to a map of fieldname => marketSummaryDynamicFieldset
                    if (!Enum.TryParse<MarketSummaryDynamicFieldset>(fieldName, out var marketSummaryDynamicFieldsetKey))
                    {
                        throw new Exception($"Unknown Market Summary Field Type: {fieldName}");
                    }

                    // this is all done in the same order that values will be received so everything will just work
                    marketSummaryHandler.FieldNames.Add(marketSummaryDynamicFieldsetKey);
                }

                return null;
            }

            var values = message.SplitFeedMessage();
            return new MarketSummaryMessage(values, marketSummaryHandler);
        }

        public static MarketSummaryMessage ParseWithRequestId(string message, MarketSummaryHandler marketSummaryHandler)
        {
            // no difference with dynamic - RequestId will be a field
            return Parse(message, marketSummaryHandler);
        }

        public override bool Equals(object obj)
        {
            return obj is MarketSummaryMessage message &&
                   RequestId == message.RequestId &&
                   Symbol == message.Symbol &&
                   ExchangeId == message.ExchangeId &&
                   SecurityType == message.SecurityType &&
                   Last == message.Last &&
                   TradeSize == message.TradeSize &&
                   TradedMarket == message.TradedMarket &&
                   TradeDate == message.TradeDate &&
                   TradeTime == message.TradeTime &&
                   Open == message.Open &&
                   High == message.High &&
                   Low == message.Low &&
                   Close == message.Close &&
                   Bid == message.Bid &&
                   BidMarket == message.BidMarket &&
                   BidSize == message.BidSize &&
                   Ask == message.Ask &&
                   AskMarket == message.AskMarket &&
                   AskSize == message.AskSize &&
                   Volume == message.Volume &&
                   PDayVolume == message.PDayVolume &&
                   UpVolume == message.UpVolume &&
                   DownVolume == message.DownVolume &&
                   NeutralVolume == message.NeutralVolume &&
                   TradeCount == message.TradeCount &&
                   UpTrades == message.UpTrades &&
                   DownTrades == message.DownTrades &&
                   NeutralTrades == message.NeutralTrades &&
                   VWAP == message.VWAP &&
                   MutualDiv == message.MutualDiv &&
                   SevenDayYield == message.SevenDayYield &&
                   OpenInterest == message.OpenInterest &&
                   Settlement == message.Settlement &&
                   SettlementDate == message.SettlementDate &&
                   ExpirationDate == message.ExpirationDate &&
                   Strike == message.Strike;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + ExchangeId.GetHashCode();
                hash = hash * 29 + SecurityType.GetHashCode();
                hash = hash * 29 + (Last.HasValue ? Last.GetHashCode() : 0);
                hash = hash * 29 + (TradeSize.HasValue ? TradeSize.GetHashCode() : 0);
                hash = hash * 29 + (TradedMarket.HasValue ? TradedMarket.GetHashCode() : 0);
                hash = hash * 29 + (TradeDate.HasValue ? TradeDate.GetHashCode() : 0);
                hash = hash * 29 + (TradeTime.HasValue ? TradeTime.GetHashCode() : 0);
                hash = hash * 29 + (Open.HasValue ? Open.GetHashCode() : 0);
                hash = hash * 29 + (High.HasValue ? High.GetHashCode() : 0);
                hash = hash * 29 + (Low.HasValue ? Low.GetHashCode() : 0);
                hash = hash * 29 + (Close.HasValue ? Close.GetHashCode() : 0);
                hash = hash * 29 + (Bid.HasValue ? Bid.GetHashCode() : 0);
                hash = hash * 29 + (BidMarket.HasValue ? BidMarket.GetHashCode() : 0);
                hash = hash * 29 + (BidSize.HasValue ? BidSize.GetHashCode() : 0);
                hash = hash * 29 + (Ask.HasValue ? Ask.GetHashCode() : 0);
                hash = hash * 29 + (AskMarket.HasValue ? AskMarket.GetHashCode() : 0);
                hash = hash * 29 + (AskSize.HasValue ? AskSize.GetHashCode() : 0);
                hash = hash * 29 + (Volume.HasValue ? Volume.GetHashCode() : 0);
                hash = hash * 29 + (PDayVolume.HasValue ? PDayVolume.GetHashCode() : 0);
                hash = hash * 29 + (UpVolume.HasValue ? UpVolume.GetHashCode() : 0);
                hash = hash * 29 + (DownVolume.HasValue ? DownVolume.GetHashCode() : 0);
                hash = hash * 29 + (NeutralVolume.HasValue ? NeutralVolume.GetHashCode() : 0);
                hash = hash * 29 + (TradeCount.HasValue ? TradeCount.GetHashCode() : 0);
                hash = hash * 29 + (UpTrades.HasValue ? UpTrades.GetHashCode() : 0);
                hash = hash * 29 + (DownTrades.HasValue ? DownTrades.GetHashCode() : 0);
                hash = hash * 29 + (NeutralTrades.HasValue ? NeutralTrades.GetHashCode() : 0);
                hash = hash * 29 + (VWAP.HasValue ? VWAP.GetHashCode() : 0);
                hash = hash * 29 + (MutualDiv.HasValue ? MutualDiv.GetHashCode() : 0);
                hash = hash * 29 + (SevenDayYield.HasValue ? SevenDayYield.GetHashCode() : 0);
                hash = hash * 29 + (OpenInterest.HasValue ? OpenInterest.GetHashCode() : 0);
                hash = hash * 29 + (Settlement.HasValue ? Settlement.GetHashCode() : 0);
                hash = hash * 29 + (SettlementDate.HasValue ? SettlementDate.GetHashCode() : 0);
                hash = hash * 29 + (ExpirationDate.HasValue ? ExpirationDate.GetHashCode() : 0);
                hash = hash * 29 + (Strike.HasValue ? Strike.GetHashCode() : 0);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(RequestId)}: {RequestId}, " +
                   $"{nameof(Symbol)}: {Symbol}, " +
                   $"{nameof(ExchangeId)}: {ExchangeId}, " +
                   $"{nameof(SecurityType)}: {SecurityType}, " +
                   $"{nameof(Last)}: {Last}, " +
                   $"{nameof(TradeSize)}: {TradeSize}, " +
                   $"{nameof(TradedMarket)}: {TradedMarket}, " +
                   $"{nameof(TradeDate)}: {TradeDate}, " +
                   $"{nameof(TradeTime)}: {TradeTime}, " +
                   $"{nameof(Open)}: {Open}, " +
                   $"{nameof(High)}: {High}, " +
                   $"{nameof(Low)}: {Low}, " +
                   $"{nameof(Close)}: {Close}, " +
                   $"{nameof(Bid)}: {Bid}, " +
                   $"{nameof(BidMarket)}: {BidMarket}, " +
                   $"{nameof(BidSize)}: {BidSize}, " +
                   $"{nameof(Ask)}: {Ask}, " +
                   $"{nameof(AskMarket)}: {AskMarket}, " +
                   $"{nameof(AskSize)}: {AskSize}, " +
                   $"{nameof(Volume)}: {Volume}, " +
                   $"{nameof(PDayVolume)}: {PDayVolume}, " +
                   $"{nameof(UpVolume)}: {UpVolume}, " +
                   $"{nameof(DownVolume)}: {DownVolume}, " +
                   $"{nameof(NeutralVolume)}: {NeutralVolume}, " +
                   $"{nameof(TradeCount)}: {TradeCount}, " +
                   $"{nameof(UpTrades)}: {UpTrades}, " +
                   $"{nameof(DownTrades)}: {DownTrades}, " +
                   $"{nameof(NeutralTrades)}: {NeutralTrades}, " +
                   $"{nameof(VWAP)}: {VWAP}, " +
                   $"{nameof(MutualDiv)}: {MutualDiv}, " +
                   $"{nameof(SevenDayYield)}: {SevenDayYield}, " +
                   $"{nameof(OpenInterest)}: {OpenInterest}, " +
                   $"{nameof(Settlement)}: {Settlement}, " +
                   $"{nameof(SettlementDate)}: {SettlementDate}, " +
                   $"{nameof(ExpirationDate)}: {ExpirationDate}, " +
                   $"{nameof(Strike)}: {Strike}";
        }
    }
}
