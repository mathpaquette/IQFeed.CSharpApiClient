using System;
using System.Text;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1DynamicFields
    {
        public const string UpdateMessageTimeFormat = UpdateSummaryMessage.UpdateMessageTimeFormat;
        public const string UpdateMessageDateFormat = FundamentalMessage.FundamentalDateTimeFormat;

        public double SevenDayYield { get; private set; }
        public double Ask { get; private set; }
        public double AskChange { get; private set; }
        public int AskMarketCenter { get; private set; }
        public int AskSize { get; private set; }
        public TimeSpan AskTime { get; private set; }
        public string AvailableRegions { get; private set; }
        public double AverageMaturity { get; private set; }
        public double Bid { get; private set; }
        public double BidChange { get; private set; }
        public int BidMarketCenter { get; private set; }
        public int BidSize { get; private set; }
        public TimeSpan BidTime { get; private set; }
        public double Change { get; private set; }
        public double ChangeFromOpen { get; private set; }
        public double Close { get; private set; }
        public double CloseRange1 { get; private set; }
        public double CloseRange2 { get; private set; }
        public string DaysToExpiration { get; private set; }
        public string DecimalPrecision { get; private set; }
        public int Delay { get; private set; }
        public string ExchangeID { get; private set; }
        public double ExtendedTrade { get; private set; }
        public DateTime ExtendedTradeDate { get; private set; }
        public int ExtendedTradeMarketCenter { get; private set; }
        public int ExtendedTradeSize { get; private set; }
        public TimeSpan ExtendedTradeTime { get; private set; }
        public double ExtendedTradingChange { get; private set; }
        public double ExtendedTradingDifference { get; private set; }
        public string FinancialStatusIndicator { get; private set; }
        public string FractionDisplayCode { get; private set; }
        public double High { get; private set; }
        public double Last { get; private set; }
        public DateTime LastDate { get; private set; }
        public int LastMarketCenter { get; private set; }
        public int LastSize { get; private set; }
        public TimeSpan LastTime { get; private set; }
        public double Low { get; private set; }
        public double MarketCapitalization { get; private set; }
        public int MarketOpen { get; private set; }
        public string MessageContents { get; private set; }
        public double MostRecentTrade { get; private set; }
        public int MostRecentTradeAggressor { get; private set; }
        public string MostRecentTradeConditions { get; private set; }
        public DateTime MostRecentTradeDate { get; private set; }
        public int MostRecentTradeDayCode { get; private set; }
        public int MostRecentTradeMarketCenter { get; private set; }
        public int MostRecentTradeSize { get; private set; }
        public TimeSpan MostRecentTradeTime { get; private set; }
        public double NetAssetValue { get; private set; }
        public int NumberOfTradesToday { get; private set; }
        public double Open { get; private set; }
        public int OpenInterest { get; private set; }
        public double OpenRange1 { get; private set; }
        public double OpenRange2 { get; private set; }
        public double PercentChange { get; private set; }
        public double PercentOffAverageVolume { get; private set; }
        public int PreviousDayVolume { get; private set; }
        public double PriceEarningsRatio { get; private set; }
        public double Range { get; private set; }
        public string RestrictedCode { get; private set; }
        public double Settle { get; private set; }
        public DateTime SettlementDate { get; private set; }
        public double Spread { get; private set; }
        public string Symbol { get; private set; }
        public int Tick { get; private set; }
        public int TickID { get; private set; }
        public int TotalVolume { get; private set; }
        public string Type { get; private set; }
        public double Volatility { get; private set; }
        public double VWAP { get; private set; }

        public Level1DynamicFields(
            double sevenDayYield,
            double ask,
            double askChange,
            int askMarketCenter,
            int askSize,
            TimeSpan askTime,
            string availableRegions,
            double averageMaturity,
            double bid,
            double bidChange,
            int bidMarketCenter,
            int bidSize,
            TimeSpan bidTime,
            double change,
            double changeFromOpen,
            double close,
            double closeRange1,
            double closeRange2,
            string daysToExpiration,
            string decimalPrecision,
            int delay,
            string exchangeID,
            double extendedTrade,
            DateTime extendedTradeDate,
            int extendedTradeMarketCenter,
            int extendedTradeSize,
            TimeSpan extendedTradeTime,
            double extendedTradingChange,
            double extendedTradingDifference,
            string financialStatusIndicator,
            string fractionDisplayCode,
            double high,
            double last,
            DateTime lastDate,
            int lastMarketCenter,
            int lastSize,
            TimeSpan lastTime,
            double low,
            double marketCapitalization,
            int marketOpen,
            string messageContents,
            double mostRecentTrade,
            int mostRecentTradeAggressor,
            string mostRecentTradeConditions,
            DateTime mostRecentTradeDate,
            int mostRecentTradeDayCode,
            int mostRecentTradeMarketCenter,
            int mostRecentTradeSize,
            TimeSpan mostRecentTradeTime,
            double netAssetValue,
            int numberOfTradesToday,
            double open,
            int openInterest,
            double openRange1,
            double openRange2,
            double percentChange,
            double percentOffAverageVolume,
            int previousDayVolume,
            double priceEarningsRatio,
            double range,
            string restrictedCode,
            double settle,
            DateTime settlementDate,
            double spread,
            string symbol,
            int tick,
            int tickID,
            int totalVolume,
            string type,
            double volatility,
            double vwap)
        {
            SevenDayYield = sevenDayYield;
            Ask = ask;
            AskChange = askChange;
            AskMarketCenter = askMarketCenter;
            AskSize = askSize;
            AskTime = askTime;
            AvailableRegions = availableRegions;
            AverageMaturity = averageMaturity;
            Bid = bid;
            BidChange = bidChange;
            BidMarketCenter = bidMarketCenter;
            BidSize = bidSize;
            BidTime = bidTime;
            Change = change;
            ChangeFromOpen = changeFromOpen;
            Close = close;
            CloseRange1 = closeRange1;
            CloseRange2 = closeRange2;
            DaysToExpiration = daysToExpiration;
            DecimalPrecision = decimalPrecision;
            Delay = delay;
            ExchangeID = exchangeID;
            ExtendedTrade = extendedTrade;
            ExtendedTradeDate = extendedTradeDate;
            ExtendedTradeMarketCenter = extendedTradeMarketCenter;
            ExtendedTradeSize = extendedTradeSize;
            ExtendedTradeTime = extendedTradeTime;
            ExtendedTradingChange = extendedTradingChange;
            ExtendedTradingDifference = extendedTradingDifference;
            FinancialStatusIndicator = financialStatusIndicator;
            FractionDisplayCode = fractionDisplayCode;
            High = high;
            Last = last;
            LastDate = lastDate;
            LastMarketCenter = lastMarketCenter;
            LastSize = lastSize;
            LastTime = lastTime;
            Low = low;
            MarketCapitalization = marketCapitalization;
            MarketOpen = marketOpen;
            MessageContents = messageContents;
            MostRecentTrade = mostRecentTrade;
            MostRecentTradeAggressor = mostRecentTradeAggressor;
            MostRecentTradeConditions = mostRecentTradeConditions;
            MostRecentTradeDate = mostRecentTradeDate;
            MostRecentTradeDayCode = mostRecentTradeDayCode;
            MostRecentTradeMarketCenter = mostRecentTradeMarketCenter;
            MostRecentTradeSize = mostRecentTradeSize;
            MostRecentTradeTime = mostRecentTradeTime;
            NetAssetValue = netAssetValue;
            NumberOfTradesToday = numberOfTradesToday;
            Open = open;
            OpenInterest = openInterest;
            OpenRange1 = openRange1;
            OpenRange2 = openRange2;
            PercentChange = percentChange;
            PercentOffAverageVolume = percentOffAverageVolume;
            PreviousDayVolume = previousDayVolume;
            PriceEarningsRatio = priceEarningsRatio;
            Range = range;
            RestrictedCode = restrictedCode;
            Settle = settle;
            SettlementDate = settlementDate;
            Spread = spread;
            Symbol = symbol;
            Tick = tick;
            TickID = tickID;
            TotalVolume = totalVolume;
            Type = type;
            Volatility = volatility;
            VWAP = vwap;
        }

        public static Level1DynamicFields Parse(string message, DynamicFieldset[] fields)
        {
            var values = message.SplitFeedMessage();

            #region Variables

            double sevenDayYield = default;
            double ask = default;
            double askChange = default;
            int askMarketCenter = default;
            int askSize = default;
            TimeSpan askTime = default;
            string availableRegions = default;
            double averageMaturity = default;
            double bid = default;
            double bidChange = default;
            int bidMarketCenter = default;
            int bidSize = default;
            TimeSpan bidTime = default;
            double change = default;
            double changeFromOpen = default;
            double close = default;
            double closeRange1 = default;
            double closeRange2 = default;
            string daysToExpiration = default;
            string decimalPrecision = default;
            int delay = default;
            string exchangeID = default;
            double extendedTrade = default;
            DateTime extendedTradeDate = default;
            int extendedTradeMarketCenter = default;
            int extendedTradeSize = default;
            TimeSpan extendedTradeTime = default;
            double extendedTradingChange = default;
            double extendedTradingDifference = default;
            string financialStatusIndicator = default;
            string fractionDisplayCode = default;
            double high = default;
            double last = default;
            DateTime lastDate = default;
            int lastMarketCenter = default;
            int lastSize = default;
            TimeSpan lastTime = default;
            double low = default;
            double marketCapitalization = default;
            int marketOpen = default;
            string messageContents = default;
            double mostRecentTrade = default;
            int mostRecentTradeAggressor = default;
            string mostRecentTradeConditions = default;
            DateTime mostRecentTradeDate = default;
            int mostRecentTradeDayCode = default;
            int mostRecentTradeMarketCenter = default;
            int mostRecentTradeSize = default;
            TimeSpan mostRecentTradeTime = default;
            double netAssetValue = default;
            int numberOfTradesToday = default;
            double open = default;
            int openInterest = default;
            double openRange1 = default;
            double openRange2 = default;
            double percentChange = default;
            double percentOffAverageVolume = default;
            int previousDayVolume = default;
            double priceEarningsRatio = default;
            double range = default;
            string restrictedCode = default;
            double settle = default;
            DateTime settlementDate = default;
            double spread = default;
            string symbol = default;
            int tick = default;
            int tickID = default;
            int totalVolume = default;
            double volatility = default;
            double vwap = default;

            #endregion

            string type = values[0];

            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i + 1];

                switch (field)
                {
                    case DynamicFieldset.SevenDayYield:
                        sevenDayYield = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Ask:
                        ask = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.AskChange:
                        askChange = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.AskMarketCenter:
                        askMarketCenter = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.AskSize:
                        askSize = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.AskTime:
                        askTime = FieldParser.ParseTime(value, UpdateMessageTimeFormat);
                        break;
                    case DynamicFieldset.AvailableRegions:
                        availableRegions = value;
                        break;
                    case DynamicFieldset.AverageMaturity:
                        averageMaturity = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Bid:
                        bid = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.BidChange:
                        bidChange = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.BidMarketCenter:
                        bidMarketCenter = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.BidSize:
                        bidSize = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.BidTime:
                        bidTime = FieldParser.ParseTime(value, UpdateMessageTimeFormat);
                        break;
                    case DynamicFieldset.Change:
                        change = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.ChangeFromOpen:
                        changeFromOpen = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Close:
                        close = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.CloseRange1:
                        closeRange1 = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.CloseRange2:
                        closeRange2 = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.DaysToExpiration:
                        daysToExpiration = value;
                        break;
                    case DynamicFieldset.DecimalPrecision:
                        decimalPrecision = value;
                        break;
                    case DynamicFieldset.Delay:
                        delay = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.ExchangeID:
                        exchangeID = value;
                        break;
                    case DynamicFieldset.ExtendedTrade:
                        extendedTrade = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.ExtendedTradeDate:
                        extendedTradeDate = FieldParser.ParseDate(value, UpdateMessageDateFormat);
                        break;
                    case DynamicFieldset.ExtendedTradeMarketCenter:
                        extendedTradeMarketCenter = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.ExtendedTradeSize:
                        extendedTradeSize = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.ExtendedTradeTime:
                        extendedTradeTime = FieldParser.ParseTime(value, UpdateMessageTimeFormat);
                        break;
                    case DynamicFieldset.ExtendedTradingChange:
                        extendedTradingChange = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.ExtendedTradingDifference:
                        extendedTradingDifference = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.FinancialStatusIndicator:
                        financialStatusIndicator = value;
                        break;
                    case DynamicFieldset.FractionDisplayCode:
                        fractionDisplayCode = value;
                        break;
                    case DynamicFieldset.High:
                        high = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Last:
                        last = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.LastDate:
                        lastDate = FieldParser.ParseDate(value, UpdateMessageDateFormat);
                        break;
                    case DynamicFieldset.LastMarketCenter:
                        lastMarketCenter = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.LastSize:
                        lastSize = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.LastTime:
                        lastTime = FieldParser.ParseTime(value, UpdateMessageTimeFormat);
                        break;
                    case DynamicFieldset.Low:
                        low = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.MarketCapitalization:
                        marketCapitalization = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.MarketOpen:
                        marketOpen = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.MessageContents:
                        messageContents = value;
                        break;
                    case DynamicFieldset.MostRecentTrade:
                        mostRecentTrade = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.MostRecentTradeAggressor:
                        mostRecentTradeAggressor = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.MostRecentTradeConditions:
                        mostRecentTradeConditions = value;
                        break;
                    case DynamicFieldset.MostRecentTradeDate:
                        mostRecentTradeDate = FieldParser.ParseDate(value, UpdateMessageDateFormat);
                        break;
                    case DynamicFieldset.MostRecentTradeDayCode:
                        mostRecentTradeDayCode = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.MostRecentTradeMarketCenter:
                        mostRecentTradeMarketCenter = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.MostRecentTradeSize:
                        mostRecentTradeSize = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.MostRecentTradeTime:
                        mostRecentTradeTime = FieldParser.ParseTime(value, UpdateMessageTimeFormat);
                        break;
                    case DynamicFieldset.NetAssetValue:
                        netAssetValue = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.NumberOfTradesToday:
                        numberOfTradesToday = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.Open:
                        open = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.OpenInterest:
                        openInterest = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.OpenRange1:
                        openRange1 = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.OpenRange2:
                        openRange2 = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.PercentChange:
                        percentChange = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.PercentOffAverageVolume:
                        percentOffAverageVolume = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.PreviousDayVolume:
                        previousDayVolume = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.PriceEarningsRatio:
                        priceEarningsRatio = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Range:
                        range = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.RestrictedCode:
                        restrictedCode = value;
                        break;
                    case DynamicFieldset.Settle:
                        settle = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.SettlementDate:
                        settlementDate = FieldParser.ParseDate(value, UpdateMessageDateFormat);
                        break;
                    case DynamicFieldset.Spread:
                        spread = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.Symbol:
                        symbol = value;
                        break;
                    case DynamicFieldset.Tick:
                        tick = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.TickID:
                        tickID = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.TotalVolume:
                        totalVolume = FieldParser.ParseInt(value);
                        break;
                    case DynamicFieldset.Type:
                        type = value;
                        break;
                    case DynamicFieldset.Volatility:
                        volatility = FieldParser.ParseDouble(value);
                        break;
                    case DynamicFieldset.VWAP:
                        vwap = FieldParser.ParseDouble(value);
                        break;
                }
            }

            return new Level1DynamicFields(
                sevenDayYield,
                ask,
                askChange,
                askMarketCenter,
                askSize,
                askTime,
                availableRegions,
                averageMaturity,
                bid,
                bidChange,
                bidMarketCenter,
                bidSize,
                bidTime,
                change,
                changeFromOpen,
                close,
                closeRange1,
                closeRange2,
                daysToExpiration,
                decimalPrecision,
                delay,
                exchangeID,
                extendedTrade,
                extendedTradeDate,
                extendedTradeMarketCenter,
                extendedTradeSize,
                extendedTradeTime,
                extendedTradingChange,
                extendedTradingDifference,
                financialStatusIndicator,
                fractionDisplayCode,
                high,
                last,
                lastDate,
                lastMarketCenter,
                lastSize,
                lastTime,
                low,
                marketCapitalization,
                marketOpen,
                messageContents,
                mostRecentTrade,
                mostRecentTradeAggressor,
                mostRecentTradeConditions,
                mostRecentTradeDate,
                mostRecentTradeDayCode,
                mostRecentTradeMarketCenter,
                mostRecentTradeSize,
                mostRecentTradeTime,
                netAssetValue,
                numberOfTradesToday,
                open,
                openInterest,
                openRange1,
                openRange2,
                percentChange,
                percentOffAverageVolume,
                previousDayVolume,
                priceEarningsRatio,
                range,
                restrictedCode,
                settle,
                settlementDate,
                spread,
                symbol,
                tick,
                tickID,
                totalVolume,
                type,
                volatility,
                vwap);
        }

        public override string ToString()
        {
            var sb = new StringBuilder($"Symbol: {Symbol}");
            sb.Append(SevenDayYield == default ? "" : $", {nameof(SevenDayYield)}: {SevenDayYield}");
            sb.Append(Ask == default ? "" : $", {nameof(Ask)}: {Ask}");
            sb.Append(AskChange == default ? "" : $", {nameof(AskChange)}: {AskChange}");
            sb.Append(AskMarketCenter == default ? "" : $", {nameof(AskMarketCenter)}: {AskMarketCenter}");
            sb.Append(AskSize == default ? "" : $", {nameof(AskSize)}: {AskSize}");
            sb.Append(AskTime == default ? "" : $", {nameof(AskTime)}: {AskTime}");
            sb.Append(AvailableRegions == default ? "" : $", {nameof(AvailableRegions)}: {AvailableRegions}");
            sb.Append(AverageMaturity == default ? "" : $", {nameof(AverageMaturity)}: {AverageMaturity}");
            sb.Append(Bid == default ? "" : $", {nameof(Bid)}: {Bid}");
            sb.Append(BidChange == default ? "" : $", {nameof(BidChange)}: {BidChange}");
            sb.Append(BidMarketCenter == default ? "" : $", {nameof(BidMarketCenter)}: {BidMarketCenter}");
            sb.Append(BidSize == default ? "" : $", {nameof(BidSize)}: {BidSize}");
            sb.Append(BidTime == default ? "" : $", {nameof(BidTime)}: {BidTime}");
            sb.Append(Change == default ? "" : $", {nameof(Change)}: {Change}");
            sb.Append(ChangeFromOpen == default ? "" : $", {nameof(ChangeFromOpen)}: {ChangeFromOpen}");
            sb.Append(Close == default ? "" : $", {nameof(Close)}: {Close}");
            sb.Append(CloseRange1 == default ? "" : $", {nameof(CloseRange1)}: {CloseRange1}");
            sb.Append(CloseRange2 == default ? "" : $", {nameof(CloseRange2)}: {CloseRange2}");
            sb.Append(DaysToExpiration == default ? "" : $", {nameof(DaysToExpiration)}: {DaysToExpiration}");
            sb.Append(DecimalPrecision == default ? "" : $", {nameof(DecimalPrecision)}: {DecimalPrecision}");
            sb.Append(Delay == default ? "" : $", {nameof(Delay)}: {Delay}");
            sb.Append(ExchangeID == default ? "" : $", {nameof(ExchangeID)}: {ExchangeID}");
            sb.Append(ExtendedTrade == default ? "" : $", {nameof(ExtendedTrade)}: {ExtendedTrade}");
            sb.Append(ExtendedTradeDate == default ? "" : $", {nameof(ExtendedTradeDate)}: {ExtendedTradeDate}");
            sb.Append(ExtendedTradeMarketCenter == default ? "" : $", {nameof(ExtendedTradeMarketCenter)}: {ExtendedTradeMarketCenter}");
            sb.Append(ExtendedTradeSize == default ? "" : $", {nameof(ExtendedTradeSize)}: {ExtendedTradeSize}");
            sb.Append(ExtendedTradeTime == default ? "" : $", {nameof(ExtendedTradeTime)}: {ExtendedTradeTime}");
            sb.Append(ExtendedTradingChange == default ? "" : $", {nameof(ExtendedTradingChange)}: {ExtendedTradingChange}");
            sb.Append(ExtendedTradingDifference == default ? "" : $", {nameof(ExtendedTradingDifference)}: {ExtendedTradingDifference}");
            sb.Append(FinancialStatusIndicator == default ? "" : $", {nameof(FinancialStatusIndicator)}: {FinancialStatusIndicator}");
            sb.Append(FractionDisplayCode == default ? "" : $", {nameof(FractionDisplayCode)}: {FractionDisplayCode}");
            sb.Append(High == default ? "" : $", {nameof(High)}: {High}");
            sb.Append(Last == default ? "" : $", {nameof(Last)}: {Last}");
            sb.Append(LastDate == default ? "" : $", {nameof(LastDate)}: {LastDate}");
            sb.Append(LastMarketCenter == default ? "" : $", {nameof(LastMarketCenter)}: {LastMarketCenter}");
            sb.Append(LastSize == default ? "" : $", {nameof(LastSize)}: {LastSize}");
            sb.Append(LastTime == default ? "" : $", {nameof(LastTime)}: {LastTime}");
            sb.Append(Low == default ? "" : $", {nameof(Low)}: {Low}");
            sb.Append(MarketCapitalization == default ? "" : $", {nameof(MarketCapitalization)}: {MarketCapitalization}");
            sb.Append(MarketOpen == default ? "" : $", {nameof(MarketOpen)}: {MarketOpen}");
            sb.Append(MessageContents == default ? "" : $", {nameof(MessageContents)}: {MessageContents}");
            sb.Append(MostRecentTrade == default ? "" : $", {nameof(MostRecentTrade)}: {MostRecentTrade}");
            sb.Append(MostRecentTradeAggressor == default ? "" : $", {nameof(MostRecentTradeAggressor)}: {MostRecentTradeAggressor}");
            sb.Append(MostRecentTradeConditions == default ? "" : $", {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}");
            sb.Append(MostRecentTradeDate == default ? "" : $", {nameof(MostRecentTradeDate)}: {MostRecentTradeDate}");
            sb.Append(MostRecentTradeDayCode == default ? "" : $", {nameof(MostRecentTradeDayCode)}: {MostRecentTradeDayCode}");
            sb.Append(MostRecentTradeMarketCenter == default ? "" : $", {nameof(MostRecentTradeMarketCenter)}: {MostRecentTradeMarketCenter}");
            sb.Append(MostRecentTradeSize == default ? "" : $", {nameof(MostRecentTradeSize)}: {MostRecentTradeSize}");
            sb.Append(MostRecentTradeTime == default ? "" : $", {nameof(MostRecentTradeTime)}: {MostRecentTradeTime}");
            sb.Append(NetAssetValue == default ? "" : $", {nameof(NetAssetValue)}: {NetAssetValue}");
            sb.Append(NumberOfTradesToday == default ? "" : $", {nameof(NumberOfTradesToday)}: {NumberOfTradesToday}");
            sb.Append(Open == default ? "" : $", {nameof(Open)}: {Open}");
            sb.Append(OpenInterest == default ? "" : $", {nameof(OpenInterest)}: {OpenInterest}");
            sb.Append(OpenRange1 == default ? "" : $", {nameof(OpenRange1)}: {OpenRange1}");
            sb.Append(OpenRange2 == default ? "" : $", {nameof(OpenRange2)}: {OpenRange2}");
            sb.Append(PercentChange == default ? "" : $", {nameof(PercentChange)}: {PercentChange}");
            sb.Append(PercentOffAverageVolume == default ? "" : $", {nameof(PercentOffAverageVolume)}: {PercentOffAverageVolume}");
            sb.Append(PreviousDayVolume == default ? "" : $", {nameof(PreviousDayVolume)}: {PreviousDayVolume}");
            sb.Append(PriceEarningsRatio == default ? "" : $", {nameof(PriceEarningsRatio)}: {PriceEarningsRatio}");
            sb.Append(Range == default ? "" : $", {nameof(Range)}: {Range}");
            sb.Append(RestrictedCode == default ? "" : $", {nameof(RestrictedCode)}: {RestrictedCode}");
            sb.Append(Settle == default ? "" : $", {nameof(Settle)}: {Settle}");
            sb.Append(SettlementDate == default ? "" : $", {nameof(SettlementDate)}: {SettlementDate}");
            sb.Append(Spread == default ? "" : $", {nameof(Spread)}: {Spread}");
            sb.Append(Tick == default ? "" : $", {nameof(Tick)}: {Tick}");
            sb.Append(TickID == default ? "" : $", {nameof(TickID)}: {TickID}");
            sb.Append(TotalVolume == default ? "" : $", {nameof(TotalVolume)}: {TotalVolume}");
            sb.Append(Type == default ? "" : $", {nameof(Type)}: {Type}");
            sb.Append(Volatility == default ? "" : $", {nameof(Volatility)}: {Volatility}");
            sb.Append(VWAP == default ? "" : $", {nameof(VWAP)}: {VWAP}");
            return sb.ToString();
        }
    }
}