﻿using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class UpdateSummaryMessage : IUpdateSummaryMessage
    {
        public const string UpdateMessageTimeFormat = "HH:mm:ss.ffffff";

        // S,CURRENT UPDATE FIELDNAMES,Symbol,Most Recent Trade,Most Recent Trade Size,Most Recent Trade Time,Most Recent Trade Market Center,Total Volume,Bid,Bid Size,Ask,Ask Size,Open,High,Low,Close,Message Contents,Most Recent Trade Conditions
        public UpdateSummaryMessage(
            string symbol,
            double mostRecentTrade,
            int mostRecentTradeSize,
            DateTime mostRecentTradeTime,
            int mostRecentTradeMarketCenter,
            int totalVolume,
            double bid,
            int bidSize,
            double ask,
            int askSize,
            double open,
            double high,
            double low,
            double close,
            string messageContents,
            string mostRecentTradeConditions)
        {
            Symbol = symbol;
            MostRecentTrade = mostRecentTrade;
            MostRecentTradeSize = mostRecentTradeSize;
            MostRecentTradeTime = mostRecentTradeTime.TimeOfDay;
            MostRecentTradeMarketCenter = mostRecentTradeMarketCenter;
            TotalVolume = totalVolume;
            Bid = bid;
            BidSize = bidSize;
            Ask = ask;
            AskSize = askSize;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            MessageContents = messageContents;
            MostRecentTradeConditions = mostRecentTradeConditions;
        }

        public string Symbol { get; private set; }
        public double MostRecentTrade { get; private set; }
        public int MostRecentTradeSize { get; private set; }
        public TimeSpan MostRecentTradeTime { get; private set; }
        public int MostRecentTradeMarketCenter { get; private set; }
        public int TotalVolume { get; private set; }
        public double Bid { get; private set; }
        public int BidSize { get; private set; }
        public double Ask { get; private set; }
        public int AskSize { get; private set; }
        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }
        public string MessageContents { get; private set; }
        public string MostRecentTradeConditions { get; private set; }
        public Level1DynamicFields DynamicFields => throw new Exception("Level1MessageDynamicHandler is required to use DynamicFields property.");

        #region Dynamic Fields

        private readonly string DynamicFieldNotAvailableErrorMessage = $"Dynamic Field is not available. Please use {nameof(ILevel1Client)}.{nameof(ILevel1Client.SelectUpdateFieldNamesV2)} to enable!";

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

        public static UpdateSummaryMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            var symbol = values[1];                                                                         // field 2
            var mostRecentTrade = FieldParser.ParseDouble(values[2]);                                       // field 71
            var mostRecentTradeSize = FieldParser.ParseInt(values[3]);                                      // field 72
            var mostRecentTradeTime = FieldParser.ParseDate(values[4], UpdateMessageTimeFormat);        
            var mostRecentTradeMarketCenter = FieldParser.ParseInt(values[5]);                              // field 75
            var totalVolume = FieldParser.ParseInt(values[6]);                                              // field 7
            var bid = FieldParser.ParseDouble(values[7]);                                                   // field 11
            var bidSize = FieldParser.ParseInt(values[8]);                                                  // field 13
            var ask = FieldParser.ParseDouble(values[9]);                                                   // field 12
            var askSize = FieldParser.ParseInt(values[10]);                                                 // field 14
            var open = FieldParser.ParseDouble(values[11]);                                                 // field 20
            var high = FieldParser.ParseDouble(values[12]);                                                 // field 9
            var low = FieldParser.ParseDouble(values[13]);                                                  // field 10
            var close = FieldParser.ParseDouble(values[14]);                                                // field 21
            var messageContents = values[15];                                                               // field 80
            var mostRecentTradeConditions = values[16];                                                     // field 74

            return new UpdateSummaryMessage(
                symbol,
                mostRecentTrade,
                mostRecentTradeSize,
                mostRecentTradeTime,
                mostRecentTradeMarketCenter,
                totalVolume,
                bid,
                bidSize,
                ask,
                askSize,
                open,
                high,
                low,
                close,
                messageContents,
                mostRecentTradeConditions
            );
        }

        public override bool Equals(object obj)
        {
            return obj is UpdateSummaryMessage message &&
                   Symbol == message.Symbol &&
                   Equals(MostRecentTrade, message.MostRecentTrade) &&
                   MostRecentTradeSize == message.MostRecentTradeSize &&
                   MostRecentTradeTime == message.MostRecentTradeTime &&
                   MostRecentTradeMarketCenter == message.MostRecentTradeMarketCenter &&
                   TotalVolume == message.TotalVolume &&
                   Equals(Bid, message.Bid) &&
                   BidSize == message.BidSize &&
                   Equals(Ask, message.Ask) &&
                   AskSize == message.AskSize &&
                   Equals(Open, message.Open) &&
                   Equals(High, message.High) &&
                   Equals(Low, message.Low) &&
                   Equals(Close, message.Close) &&
                   MessageContents == message.MessageContents &&
                   MostRecentTradeConditions == message.MostRecentTradeConditions;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + Symbol.GetHashCode();
                hash = hash * 29 + MostRecentTrade.GetHashCode();
                hash = hash * 29 + MostRecentTradeSize.GetHashCode();
                hash = hash * 29 + MostRecentTradeTime.GetHashCode();
                hash = hash * 29 + MostRecentTradeMarketCenter.GetHashCode();
                hash = hash * 29 + TotalVolume.GetHashCode();
                hash = hash * 29 + Bid.GetHashCode();
                hash = hash * 29 + BidSize.GetHashCode();
                hash = hash * 29 + Ask.GetHashCode();
                hash = hash * 29 + AskSize.GetHashCode();
                hash = hash * 29 + Open.GetHashCode();
                hash = hash * 29 + High.GetHashCode();
                hash = hash * 29 + Low.GetHashCode();
                hash = hash * 29 + Close.GetHashCode();
                hash = hash * 29 + MessageContents.GetHashCode();
                hash = hash * 29 + MostRecentTradeConditions.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(MostRecentTrade)}: {MostRecentTrade}, {nameof(MostRecentTradeSize)}: {MostRecentTradeSize}, {nameof(MostRecentTradeTime)}: {MostRecentTradeTime}, {nameof(MostRecentTradeMarketCenter)}: {MostRecentTradeMarketCenter}, {nameof(TotalVolume)}: {TotalVolume}, {nameof(Bid)}: {Bid}, {nameof(BidSize)}: {BidSize}, {nameof(Ask)}: {Ask}, {nameof(AskSize)}: {AskSize}, {nameof(Open)}: {Open}, {nameof(High)}: {High}, {nameof(Low)}: {Low}, {nameof(Close)}: {Close}, {nameof(MessageContents)}: {MessageContents}, {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}";
        }
    }
}