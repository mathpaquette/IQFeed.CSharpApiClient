using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

// ReSharper disable InconsistentNaming

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class FundamentalMessage
    {
        public const string FundamentalDateTimeFormat = "MM/dd/yyyy";

        public FundamentalMessage(string symbol, string exchangeId, float? pe, int? averageVolume, float? fiftyTwoWeekHigh, float? fiftyTwoWeekLow, float? calendarYearHigh, float? calendarYearLow, float? dividendYield, float? dividendAmount, float? dividendRate, DateTime? payDate, DateTime? exDividendDate, int? shortInterest, float? currentYearEarningsPerShare, float? nextYearEarningsPerShare, float? fiveYearGrowthPercentage, int? fiscalYearEnd, string companyName, string rootOptionSymbol, float? percentHeldByInstitutions, float? beta, string leaps, float? currentAssets, float? currentLiabilities, DateTime? balanceSheetDate, float? longTermDebt, float? commonSharesOutstanding, string splitFactor1, string splitFactor2, string formatCode, int? precision, int? sic, float? historicalVolatility, string securityType, string listedMarket, DateTime? fiftyTwoWeekHighDate, DateTime? fiftyTwoWeekLowDate, DateTime? calendarYearHighDate, DateTime? calendarYearLowDate, float? yearEndClose, DateTime? maturityDate, float? couponRate, DateTime? expirationDate, float? strikePrice, int? naics, string exchangeRoot, float? optionsPremiumMultiplier, int? optionsMultipleDeliverables)
        {
            Symbol = symbol;
            ExchangeId = exchangeId;
            PE = pe;
            AverageVolume = averageVolume;
            FiftyTwoWeekHigh = fiftyTwoWeekHigh;
            FiftyTwoWeekLow = fiftyTwoWeekLow;
            CalendarYearHigh = calendarYearHigh;
            CalendarYearLow = calendarYearLow;
            DividendYield = dividendYield;
            DividendAmount = dividendAmount;
            DividendRate = dividendRate;
            PayDate = payDate;
            ExDividendDate = exDividendDate;
            ShortInterest = shortInterest;
            CurrentYearEarningsPerShare = currentYearEarningsPerShare;
            NextYearEarningsPerShare = nextYearEarningsPerShare;
            FiveYearGrowthPercentage = fiveYearGrowthPercentage;
            FiscalYearEnd = fiscalYearEnd;
            CompanyName = companyName;
            RootOptionSymbol = rootOptionSymbol;
            PercentHeldByInstitutions = percentHeldByInstitutions;
            Beta = beta;
            Leaps = leaps;
            CurrentAssets = currentAssets;
            CurrentLiabilities = currentLiabilities;
            BalanceSheetDate = balanceSheetDate;
            LongTermDebt = longTermDebt;
            CommonSharesOutstanding = commonSharesOutstanding;
            SplitFactor1 = splitFactor1;
            SplitFactor2 = splitFactor2;
            FormatCode = formatCode;
            Precision = precision;
            SIC = sic;
            HistoricalVolatility = historicalVolatility;
            SecurityType = securityType;
            ListedMarket = listedMarket;
            FiftyTwoWeekHighDate = fiftyTwoWeekHighDate;
            FiftyTwoWeekLowDate = fiftyTwoWeekLowDate;
            CalendarYearHighDate = calendarYearHighDate;
            CalendarYearLowDate = calendarYearLowDate;
            YearEndClose = yearEndClose;
            MaturityDate = maturityDate;
            CouponRate = couponRate;
            ExpirationDate = expirationDate;
            StrikePrice = strikePrice;
            NAICS = naics;
            ExchangeRoot = exchangeRoot;
            OptionsPremiumMultiplier = optionsPremiumMultiplier;
            OptionsMultipleDeliverables = optionsMultipleDeliverables;
        }

        public string Symbol { get; }                           // 0
        public string ExchangeId { get; }                      // 1
        public float? PE { get; }                               // 2
        public int? AverageVolume { get; }                     // 3
        /// <summary>
        /// 52-week high
        /// </summary>
        public float? FiftyTwoWeekHigh { get; }                    // 4
        /// <summary>
        /// 52-week low
        /// </summary>
        public float? FiftyTwoWeekLow { get; }                     // 5
        public float? CalendarYearHigh { get; }               // 6
        public float? CalendarYearLow { get; }                // 7
        public float? DividendYield { get; }                   // 8
        public float? DividendAmount { get; }                  // 9
        public float? DividendRate { get; }                    // 10
        public DateTime? PayDate { get; }                      // 11
        public DateTime? ExDividendDate { get; }              // 12
        // (Reserved)                                           // 13
        // (Reserved)                                           // 14
        // (Reserved)                                           // 15
        public int? ShortInterest { get; }                     // 16
        // (Reserved)                                           // 17
        public float? CurrentYearEarningsPerShare { get; }  // 18
        public float? NextYearEarningsPerShare { get; }     // 19
        public float? FiveYearGrowthPercentage { get; }      // 20
        public int? FiscalYearEnd { get; }                    // 21
        // (Reserved)                                           // 22
        public string CompanyName { get; }                     // 23
        public string RootOptionSymbol { get; }               // 24
        public float? PercentHeldByInstitutions { get; }     // 25
        public float? Beta { get; }                             // 26
        public string Leaps { get; }                            // 27
        public float? CurrentAssets { get; }                   // 28
        public float? CurrentLiabilities { get; }              // 29
        public DateTime? BalanceSheetDate { get; }            // 30
        public float? LongTermDebt { get; }                   // 31
        public float? CommonSharesOutstanding { get; }        // 32
        // (Reserved)                                           // 33
        public string SplitFactor1 { get; }                   // 34
        public string SplitFactor2 { get; }                   // 35
        // (Reserved)                                           // 36
        // (Reserved)                                           // 37
        public string FormatCode { get; }                      // 38
        public int? Precision { get; }                          // 39   
        public int? SIC { get; }                                // 40
        public float? HistoricalVolatility { get; }            // 41
        public string SecurityType { get; }                    // 42
        public string ListedMarket { get; }                    // 43
        public DateTime? FiftyTwoWeekHighDate { get; }            // 44
        public DateTime? FiftyTwoWeekLowDate { get; }             // 45
        public DateTime? CalendarYearHighDate { get; }       // 46
        public DateTime? CalendarYearLowDate { get; }        // 47
        public float? YearEndClose { get; }                   // 48
        public DateTime? MaturityDate { get; }                 // 49
        public float? CouponRate { get; }                      // 50
        public DateTime? ExpirationDate { get; }               // 51
        public float? StrikePrice { get; }                     // 52
        public int? NAICS { get; }                              // 53
        public string ExchangeRoot { get; }                    // 54
        public float? OptionsPremiumMultiplier { get; }       // 55
        public int? OptionsMultipleDeliverables { get; }      // 56

        public static FundamentalMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();
            return new FundamentalMessage(
                values[1].NullIfEmpty(),
                values[2].NullIfEmpty(),
                values[3].ToNullableFloat(),
                values[4].ToNullableInt(),
                values[5].ToNullableFloat(),
                values[6].ToNullableFloat(),
                values[7].ToNullableFloat(),
                values[8].ToNullableFloat(),
                values[9].ToNullableFloat(),
                values[10].ToNullableFloat(),
                values[11].ToNullableFloat(),
                values[12].ToNullableDateTime(FundamentalDateTimeFormat),
                values[13].ToNullableDateTime(FundamentalDateTimeFormat),
                //// (Reserved)
                //// (Reserved)
                //// (Reserved)
                values[17].ToNullableInt(),
                //// (Reserved)
                values[19].ToNullableFloat(),
                values[20].ToNullableFloat(),
                values[21].ToNullableFloat(),
                values[22].ToNullableInt(),
                //// (Reserved)    
                values[24].NullIfEmpty(),
                values[25].NullIfEmpty(),
                values[26].ToNullableFloat(),
                values[27].ToNullableFloat(),
                values[28].NullIfEmpty(),
                values[29].ToNullableFloat(),
                values[30].ToNullableFloat(),
                values[31].ToNullableDateTime(FundamentalDateTimeFormat),
                values[32].ToNullableFloat(),
                values[33].ToNullableFloat(),
                //// (Reserved)
                values[35].NullIfEmpty(),
                values[36].NullIfEmpty(),
                //// (Reserved)
                //// (Reserved)
                values[39].NullIfEmpty(),
                values[40].ToNullableInt(),
                values[41].ToNullableInt(),
                values[42].ToNullableFloat(),
                values[43].NullIfEmpty(),
                values[44].NullIfEmpty(),
                values[45].ToNullableDateTime(FundamentalDateTimeFormat),
                values[46].ToNullableDateTime(FundamentalDateTimeFormat),
                values[47].ToNullableDateTime(FundamentalDateTimeFormat),
                values[48].ToNullableDateTime(FundamentalDateTimeFormat),
                values[49].ToNullableFloat(),
                values[50].ToNullableDateTime(FundamentalDateTimeFormat),
                values[51].ToNullableFloat(),
                values[52].ToNullableDateTime(FundamentalDateTimeFormat),
                values[53].ToNullableFloat(),
                values[54].ToNullableInt(),
                values[55].NullIfEmpty(),
                values[56].ToNullableFloat(),
                values[57].ToNullableInt()
            );
        }

        public override bool Equals(object obj)
        {
            return obj is FundamentalMessage message &&
                   Symbol == message.Symbol &&
                   ExchangeId == message.ExchangeId &&
                   EqualityComparer<float?>.Default.Equals(PE, message.PE) &&
                   EqualityComparer<int?>.Default.Equals(AverageVolume, message.AverageVolume) &&
                   EqualityComparer<float?>.Default.Equals(FiftyTwoWeekHigh, message.FiftyTwoWeekHigh) &&
                   EqualityComparer<float?>.Default.Equals(FiftyTwoWeekLow, message.FiftyTwoWeekLow) &&
                   EqualityComparer<float?>.Default.Equals(CalendarYearHigh, message.CalendarYearHigh) &&
                   EqualityComparer<float?>.Default.Equals(CalendarYearLow, message.CalendarYearLow) &&
                   EqualityComparer<float?>.Default.Equals(DividendYield, message.DividendYield) &&
                   EqualityComparer<float?>.Default.Equals(DividendAmount, message.DividendAmount) &&
                   EqualityComparer<float?>.Default.Equals(DividendRate, message.DividendRate) &&
                   EqualityComparer<DateTime?>.Default.Equals(PayDate, message.PayDate) &&
                   EqualityComparer<DateTime?>.Default.Equals(ExDividendDate, message.ExDividendDate) &&
                   EqualityComparer<int?>.Default.Equals(ShortInterest, message.ShortInterest) &&
                   EqualityComparer<float?>.Default.Equals(CurrentYearEarningsPerShare, message.CurrentYearEarningsPerShare) &&
                   EqualityComparer<float?>.Default.Equals(NextYearEarningsPerShare, message.NextYearEarningsPerShare) &&
                   EqualityComparer<float?>.Default.Equals(FiveYearGrowthPercentage, message.FiveYearGrowthPercentage) &&
                   EqualityComparer<int?>.Default.Equals(FiscalYearEnd, message.FiscalYearEnd) &&
                   CompanyName == message.CompanyName &&
                   RootOptionSymbol == message.RootOptionSymbol &&
                   EqualityComparer<float?>.Default.Equals(PercentHeldByInstitutions, message.PercentHeldByInstitutions) &&
                   EqualityComparer<float?>.Default.Equals(Beta, message.Beta) &&
                   Leaps == message.Leaps &&
                   EqualityComparer<float?>.Default.Equals(CurrentAssets, message.CurrentAssets) &&
                   EqualityComparer<float?>.Default.Equals(CurrentLiabilities, message.CurrentLiabilities) &&
                   EqualityComparer<DateTime?>.Default.Equals(BalanceSheetDate, message.BalanceSheetDate) &&
                   EqualityComparer<float?>.Default.Equals(LongTermDebt, message.LongTermDebt) &&
                   EqualityComparer<float?>.Default.Equals(CommonSharesOutstanding, message.CommonSharesOutstanding) &&
                   SplitFactor1 == message.SplitFactor1 &&
                   SplitFactor2 == message.SplitFactor2 &&
                   FormatCode == message.FormatCode &&
                   EqualityComparer<int?>.Default.Equals(Precision, message.Precision) &&
                   EqualityComparer<int?>.Default.Equals(SIC, message.SIC) &&
                   EqualityComparer<float?>.Default.Equals(HistoricalVolatility, message.HistoricalVolatility) &&
                   SecurityType == message.SecurityType &&
                   ListedMarket == message.ListedMarket &&
                   EqualityComparer<DateTime?>.Default.Equals(FiftyTwoWeekHighDate, message.FiftyTwoWeekHighDate) &&
                   EqualityComparer<DateTime?>.Default.Equals(FiftyTwoWeekLowDate, message.FiftyTwoWeekLowDate) &&
                   EqualityComparer<DateTime?>.Default.Equals(CalendarYearHighDate, message.CalendarYearHighDate) &&
                   EqualityComparer<DateTime?>.Default.Equals(CalendarYearLowDate, message.CalendarYearLowDate) &&
                   EqualityComparer<float?>.Default.Equals(YearEndClose, message.YearEndClose) &&
                   EqualityComparer<DateTime?>.Default.Equals(MaturityDate, message.MaturityDate) &&
                   EqualityComparer<float?>.Default.Equals(CouponRate, message.CouponRate) &&
                   EqualityComparer<DateTime?>.Default.Equals(ExpirationDate, message.ExpirationDate) &&
                   EqualityComparer<float?>.Default.Equals(StrikePrice, message.StrikePrice) &&
                   EqualityComparer<int?>.Default.Equals(NAICS, message.NAICS) &&
                   ExchangeRoot == message.ExchangeRoot &&
                   EqualityComparer<float?>.Default.Equals(OptionsPremiumMultiplier, message.OptionsPremiumMultiplier) &&
                   EqualityComparer<int?>.Default.Equals(OptionsMultipleDeliverables, message.OptionsMultipleDeliverables);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Symbol);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeId);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(PE);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(AverageVolume);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(FiftyTwoWeekHigh);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(FiftyTwoWeekLow);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CalendarYearHigh);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CalendarYearLow);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(DividendYield);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(DividendAmount);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(DividendRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(PayDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExDividendDate);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(ShortInterest);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CurrentYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(NextYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(FiveYearGrowthPercentage);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(FiscalYearEnd);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(CompanyName);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(RootOptionSymbol);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(PercentHeldByInstitutions);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(Beta);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Leaps);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CurrentAssets);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CurrentLiabilities);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(BalanceSheetDate);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(LongTermDebt);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CommonSharesOutstanding);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor1);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor2);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(FormatCode);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(Precision);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(SIC);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(HistoricalVolatility);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SecurityType);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ListedMarket);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekLowDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearLowDate);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(YearEndClose);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(MaturityDate);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(CouponRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExpirationDate);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(StrikePrice);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(NAICS);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeRoot);
                hash = hash * 29 + EqualityComparer<float?>.Default.GetHashCode(OptionsPremiumMultiplier);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(OptionsMultipleDeliverables);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(ExchangeId)}: {ExchangeId}, {nameof(PE)}: {PE}, {nameof(AverageVolume)}: {AverageVolume}, {nameof(FiftyTwoWeekHigh)}: {FiftyTwoWeekHigh}, {nameof(FiftyTwoWeekLow)}: {FiftyTwoWeekLow}, {nameof(CalendarYearHigh)}: {CalendarYearHigh}, {nameof(CalendarYearLow)}: {CalendarYearLow}, {nameof(DividendYield)}: {DividendYield}, {nameof(DividendAmount)}: {DividendAmount}, {nameof(DividendRate)}: {DividendRate}, {nameof(PayDate)}: {PayDate}, {nameof(ExDividendDate)}: {ExDividendDate}, {nameof(ShortInterest)}: {ShortInterest}, {nameof(CurrentYearEarningsPerShare)}: {CurrentYearEarningsPerShare}, {nameof(NextYearEarningsPerShare)}: {NextYearEarningsPerShare}, {nameof(FiveYearGrowthPercentage)}: {FiveYearGrowthPercentage}, {nameof(FiscalYearEnd)}: {FiscalYearEnd}, {nameof(CompanyName)}: {CompanyName}, {nameof(RootOptionSymbol)}: {RootOptionSymbol}, {nameof(PercentHeldByInstitutions)}: {PercentHeldByInstitutions}, {nameof(Beta)}: {Beta}, {nameof(Leaps)}: {Leaps}, {nameof(CurrentAssets)}: {CurrentAssets}, {nameof(CurrentLiabilities)}: {CurrentLiabilities}, {nameof(BalanceSheetDate)}: {BalanceSheetDate}, {nameof(LongTermDebt)}: {LongTermDebt}, {nameof(CommonSharesOutstanding)}: {CommonSharesOutstanding}, {nameof(SplitFactor1)}: {SplitFactor1}, {nameof(SplitFactor2)}: {SplitFactor2}, {nameof(FormatCode)}: {FormatCode}, {nameof(Precision)}: {Precision}, {nameof(SIC)}: {SIC}, {nameof(HistoricalVolatility)}: {HistoricalVolatility}, {nameof(SecurityType)}: {SecurityType}, {nameof(ListedMarket)}: {ListedMarket}, {nameof(FiftyTwoWeekHighDate)}: {FiftyTwoWeekHighDate}, {nameof(FiftyTwoWeekLowDate)}: {FiftyTwoWeekLowDate}, {nameof(CalendarYearHighDate)}: {CalendarYearHighDate}, {nameof(CalendarYearLowDate)}: {CalendarYearLowDate}, {nameof(YearEndClose)}: {YearEndClose}, {nameof(MaturityDate)}: {MaturityDate}, {nameof(CouponRate)}: {CouponRate}, {nameof(ExpirationDate)}: {ExpirationDate}, {nameof(StrikePrice)}: {StrikePrice}, {nameof(NAICS)}: {NAICS}, {nameof(ExchangeRoot)}: {ExchangeRoot}, {nameof(OptionsPremiumMultiplier)}: {OptionsPremiumMultiplier}, {nameof(OptionsMultipleDeliverables)}: {OptionsMultipleDeliverables}";
        }
    }
}