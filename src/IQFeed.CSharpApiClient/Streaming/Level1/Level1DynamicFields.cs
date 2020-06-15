using System;
using System.Globalization;
using System.Linq;
using System.Text;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Streaming.Level1.Messages;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1DynamicFields
    {
        public const string UpdateMessageTimeFormat = UpdateSummaryMessage.UpdateMessageTimeFormat;

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
        public string MostRecentTradeConditions { get; private set; }
        public DateTime MostRecentTradeDate { get; private set; }
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
            string mostRecentTradeConditions,
            DateTime mostRecentTradeDate,
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
            MostRecentTradeConditions = mostRecentTradeConditions;
            MostRecentTradeDate = mostRecentTradeDate;
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
            string mostRecentTradeConditions = default;
            DateTime mostRecentTradeDate = default;
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
            string type = default;
            double volatility = default;
            double vwap = default;

            #endregion

            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i+1];

                switch (field)
                {
                    case DynamicFieldset.SevenDayYield:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out sevenDayYield);
                        break;
                    case DynamicFieldset.Ask:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out ask);
                        break;
                    case DynamicFieldset.AskChange:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out askChange);
                        break;
                    case DynamicFieldset.AskMarketCenter:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out askMarketCenter);
                        break;
                    case DynamicFieldset.AskSize:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out askSize);
                        break;
                    case DynamicFieldset.AskTime:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var askTimeValue);
                        askTime = askTimeValue.TimeOfDay;
                        break;
                    case DynamicFieldset.AvailableRegions:
                        availableRegions = value;
                        break;
                    case DynamicFieldset.AverageMaturity:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out averageMaturity);
                        break;
                    case DynamicFieldset.Bid:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out bid);
                        break;
                    case DynamicFieldset.BidChange:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out bidChange);
                        break;
                    case DynamicFieldset.BidMarketCenter:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out bidMarketCenter);
                        break;
                    case DynamicFieldset.BidSize:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out bidSize);
                        break;
                    case DynamicFieldset.BidTime:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var bidTimeValue);
                        bidTime = bidTimeValue.TimeOfDay;
                        break;
                    case DynamicFieldset.Change:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out change);
                        break;
                    case DynamicFieldset.ChangeFromOpen:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out changeFromOpen);
                        break;
                    case DynamicFieldset.Close:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out close);
                        break;
                    case DynamicFieldset.CloseRange1:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out closeRange1);
                        break;
                    case DynamicFieldset.CloseRange2:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out closeRange2);
                        break;
                    case DynamicFieldset.DaysToExpiration:
                        daysToExpiration = value;
                        break;
                    case DynamicFieldset.DecimalPrecision:
                        decimalPrecision = value;
                        break;
                    case DynamicFieldset.Delay:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out delay);
                        break;
                    case DynamicFieldset.ExchangeID:
                        exchangeID = value;
                        break;
                    case DynamicFieldset.ExtendedTrade:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out extendedTrade);
                        break;
                    case DynamicFieldset.ExtendedTradeDate:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out extendedTradeDate);
                        break;
                    case DynamicFieldset.ExtendedTradeMarketCenter:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out extendedTradeMarketCenter);
                        break;
                    case DynamicFieldset.ExtendedTradeSize:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out extendedTradeSize);
                        break;
                    case DynamicFieldset.ExtendedTradeTime:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var extendedTradeTimeValue);
                        extendedTradeTime = extendedTradeTimeValue.TimeOfDay;
                        break;
                    case DynamicFieldset.ExtendedTradingChange:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out extendedTradingChange);
                        break;
                    case DynamicFieldset.ExtendedTradingDifference:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out extendedTradingDifference);
                        break;
                    case DynamicFieldset.FinancialStatusIndicator:
                        financialStatusIndicator = value;
                        break;
                    case DynamicFieldset.FractionDisplayCode:
                        fractionDisplayCode = value;
                        break;
                    case DynamicFieldset.High:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out high);
                        break;
                    case DynamicFieldset.Last:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out last);
                        break;
                    case DynamicFieldset.LastDate:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out lastDate);
                        break;
                    case DynamicFieldset.LastMarketCenter:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out lastMarketCenter);
                        break;
                    case DynamicFieldset.LastSize:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out lastSize);
                        break;
                    case DynamicFieldset.LastTime:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var lastTimeValue);
                        lastTime = lastTimeValue.TimeOfDay;
                        break;
                    case DynamicFieldset.Low:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out low);
                        break;
                    case DynamicFieldset.MarketCapitalization:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out marketCapitalization);
                        break;
                    case DynamicFieldset.MarketOpen:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out marketOpen);
                        break;
                    case DynamicFieldset.MessageContents:
                        messageContents = value;
                        break;
                    case DynamicFieldset.MostRecentTrade:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out mostRecentTrade);
                        break;
                    case DynamicFieldset.MostRecentTradeConditions:
                        mostRecentTradeConditions = value;
                        break;
                    case DynamicFieldset.MostRecentTradeDate:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out mostRecentTradeDate);
                        break;
                    case DynamicFieldset.MostRecentTradeMarketCenter:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out mostRecentTradeMarketCenter);
                        break;
                    case DynamicFieldset.MostRecentTradeSize:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out mostRecentTradeSize);
                        break;
                    case DynamicFieldset.MostRecentTradeTime:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var mostRecentTradeTimeValue);
                        mostRecentTradeTime = mostRecentTradeTimeValue.TimeOfDay;
                        break;
                    case DynamicFieldset.NetAssetValue:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out netAssetValue);
                        break;
                    case DynamicFieldset.NumberOfTradesToday:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out numberOfTradesToday);
                        break;
                    case DynamicFieldset.Open:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out open);
                        break;
                    case DynamicFieldset.OpenInterest:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out openInterest);
                        break;
                    case DynamicFieldset.OpenRange1:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out openRange1);
                        break;
                    case DynamicFieldset.OpenRange2:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out openRange2);
                        break;
                    case DynamicFieldset.PercentChange:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out percentChange);
                        break;
                    case DynamicFieldset.PercentOffAverageVolume:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out percentOffAverageVolume);
                        break;
                    case DynamicFieldset.PreviousDayVolume:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out previousDayVolume);
                        break;
                    case DynamicFieldset.PriceEarningsRatio:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out priceEarningsRatio);
                        break;
                    case DynamicFieldset.Range:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out range);
                        break;
                    case DynamicFieldset.RestrictedCode:
                        restrictedCode = value;
                        break;
                    case DynamicFieldset.Settle:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out settle);
                        break;
                    case DynamicFieldset.SettlementDate:
                        DateTime.TryParseExact(value, UpdateMessageTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out settlementDate);
                        break;
                    case DynamicFieldset.Spread:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out spread);
                        break;
                    case DynamicFieldset.Symbol:
                        symbol = value;
                        break;
                    case DynamicFieldset.Tick:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out tick);
                        break;
                    case DynamicFieldset.TickID:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out tickID);
                        break;
                    case DynamicFieldset.TotalVolume:
                        int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out totalVolume);
                        break;
                    case DynamicFieldset.Type:
                        type = value;
                        break;
                    case DynamicFieldset.Volatility:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out volatility);
                        break;
                    case DynamicFieldset.VWAP:
                        double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out vwap);
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
                mostRecentTradeConditions,
                mostRecentTradeDate,
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
            sb.Append(MostRecentTradeConditions == default ? "" : $", {nameof(MostRecentTradeConditions)}: {MostRecentTradeConditions}");
            sb.Append(MostRecentTradeDate == default ? "" : $", {nameof(MostRecentTradeDate)}: {MostRecentTradeDate}");
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