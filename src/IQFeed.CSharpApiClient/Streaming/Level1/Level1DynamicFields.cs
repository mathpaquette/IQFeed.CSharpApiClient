using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Level1
{
    public class Level1DynamicFields
    {
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
        public char FinancialStatusIndicator { get; private set; }
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

        public static Level1DynamicFields Parse(string message, DynamicFieldset[] fields)
        {
            var values = message.SplitFeedMessage();
            return new Level1DynamicFields(values, fields);
        }

        public Level1DynamicFields(string[] values, DynamicFieldset[] fields)
        {
            for (var i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var value = values[i];

                switch (field)
                {
                    case DynamicFieldset.SevenDayYield:
                        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var sevenDayYield))
                            SevenDayYield = sevenDayYield;
                        break;
                    case DynamicFieldset.Ask:
                        // TODO
                        break;
                    case DynamicFieldset.AskChange:
                        // TODO
                        break;
                    case DynamicFieldset.AskMarketCenter:
                        // TODO
                        break;
                    case DynamicFieldset.AskSize:
                        // TODO
                        break;
                    case DynamicFieldset.AskTime:
                        // TODO
                        break;
                    case DynamicFieldset.AvailableRegions:
                        // TODO
                        break;
                    case DynamicFieldset.AverageMaturity:
                        // TODO
                        break;
                    case DynamicFieldset.Bid:
                        // TODO
                        break;
                    case DynamicFieldset.BidChange:
                        // TODO
                        break;
                    case DynamicFieldset.BidMarketCenter:
                        // TODO
                        break;
                    case DynamicFieldset.BidSize:
                        // TODO
                        break;
                    case DynamicFieldset.BidTime:
                        // TODO
                        break;
                    case DynamicFieldset.Change:
                        // TODO
                        break;
                    case DynamicFieldset.ChangeFromOpen:
                        // TODO
                        break;
                    case DynamicFieldset.Close:
                        // TODO
                        break;
                    case DynamicFieldset.CloseRange1:
                        // TODO
                        break;
                    case DynamicFieldset.CloseRange2:
                        // TODO
                        break;
                    case DynamicFieldset.DaysToExpiration:
                        // TODO
                        break;
                    case DynamicFieldset.DecimalPrecision:
                        // TODO
                        break;
                    case DynamicFieldset.Delay:
                        // TODO
                        break;
                    case DynamicFieldset.ExchangeID:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTrade:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradeDate:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradeMarketCenter:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradeSize:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradeTime:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradingChange:
                        // TODO
                        break;
                    case DynamicFieldset.ExtendedTradingDifference:
                        // TODO
                        break;
                    case DynamicFieldset.FinancialStatusIndicator:
                        // TODO
                        break;
                    case DynamicFieldset.FractionDisplayCode:
                        // TODO
                        break;
                    case DynamicFieldset.High:
                        // TODO
                        break;
                    case DynamicFieldset.Last:
                        // TODO
                        break;
                    case DynamicFieldset.LastDate:
                        // TODO
                        break;
                    case DynamicFieldset.LastMarketCenter:
                        // TODO
                        break;
                    case DynamicFieldset.LastSize:
                        // TODO
                        break;
                    case DynamicFieldset.LastTime:
                        // TODO
                        break;
                    case DynamicFieldset.LastTradeDate:
                        // TODO
                        break;
                    case DynamicFieldset.Low:
                        // TODO
                        break;
                    case DynamicFieldset.MarketCapitalization:
                        // TODO
                        break;
                    case DynamicFieldset.MarketOpen:
                        // TODO
                        break;
                    case DynamicFieldset.MessageContents:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTrade:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTradeConditions:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTradeDate:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTradeMarketCenter:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTradeSize:
                        // TODO
                        break;
                    case DynamicFieldset.MostRecentTradeTime:
                        // TODO
                        break;
                    case DynamicFieldset.NetAssetValue:
                        // TODO
                        break;
                    case DynamicFieldset.NumberOfTradesToday:
                        // TODO
                        break;
                    case DynamicFieldset.Open:
                        // TODO
                        break;
                    case DynamicFieldset.OpenInterest:
                        // TODO
                        break;
                    case DynamicFieldset.OpenRange1:
                        // TODO
                        break;
                    case DynamicFieldset.OpenRange2:
                        // TODO
                        break;
                    case DynamicFieldset.PercentChange:
                        // TODO
                        break;
                    case DynamicFieldset.PercentOffAverageVolume:
                        // TODO
                        break;
                    case DynamicFieldset.PreviousDayVolume:
                        // TODO
                        break;
                    case DynamicFieldset.PriceEarningsRatio:
                        // TODO
                        break;
                    case DynamicFieldset.Range:
                        // TODO
                        break;
                    case DynamicFieldset.RestrictedCode:
                        // TODO
                        break;
                    case DynamicFieldset.Settle:
                        // TODO
                        break;
                    case DynamicFieldset.SettlementDate:
                        // TODO
                        break;
                    case DynamicFieldset.Spread:
                        // TODO
                        break;
                    case DynamicFieldset.Symbol:
                        Symbol = value;
                        break;
                    case DynamicFieldset.Tick:
                        // TODO
                        break;
                    case DynamicFieldset.TickID:
                        // TODO
                        break;
                    case DynamicFieldset.TotalVolume:
                        // TODO
                        break;
                    case DynamicFieldset.Type:
                        // TODO
                        break;
                    case DynamicFieldset.Volatility:
                        // TODO
                        break;
                    case DynamicFieldset.VWAP:
                        // TODO
                        break;
                }
            }
        }
    }
}