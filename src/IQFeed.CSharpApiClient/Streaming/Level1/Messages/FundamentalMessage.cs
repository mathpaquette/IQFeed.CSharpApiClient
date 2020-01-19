using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

// ReSharper disable InconsistentNaming

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class FundamentalMessage
    {
        public const string FundamentalDateTimeFormat = "MM/dd/yyyy";

        public FundamentalMessage(
            string symbol,
            string exchangeId,
            decimal? pe,
            int? averageVolume,
            decimal? fiftyTwoWeekHigh,
            decimal? fiftyTwoWeekLow,
            decimal? calendarYearHigh,
            decimal? calendarYearLow,
            decimal? dividendYield,
            decimal? dividendAmount,
            decimal? dividendRate,
            DateTime? payDate,
            DateTime? exDividendDate,
            int? shortInterest,
            decimal? currentYearEarningsPerShare,
            decimal? nextYearEarningsPerShare,
            decimal? fiveYearGrowthPercentage,
            int? fiscalYearEnd,
            string companyName,
            string rootOptionSymbol,
            decimal? percentHeldByInstitutions,
            decimal? beta,
            string leaps,
            decimal? currentAssets,
            decimal? currentLiabilities,
            DateTime? balanceSheetDate,
            decimal? longTermDebt,
            decimal? commonSharesOutstanding,
            string splitFactor1,
            string splitFactor2,
            string formatCode,
            int? precision,
            int? sic,
            decimal? historicalVolatility,
            string securityType,
            string listedMarket,
            DateTime? fiftyTwoWeekHighDate,
            DateTime? fiftyTwoWeekLowDate,
            DateTime? calendarYearHighDate,
            DateTime? calendarYearLowDate,
            decimal? yearEndClose,
            DateTime? maturityDate,
            decimal? couponRate,
            DateTime? expirationDate,
            decimal? strikePrice,
            int? naics,
            string exchangeRoot,
            decimal? optionsPremiumMultiplier,
            int? optionsMultipleDeliverables)
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

        public string Symbol { get; private set; }                               // 0
        public string ExchangeId { get; private set; }                           // 1
        public decimal? PE { get; private set; }                                 // 2
        public int? AverageVolume { get; private set; }                          // 3
        /// <summary>
        /// 52-week high
        /// </summary>
        public decimal? FiftyTwoWeekHigh { get; private set; }                   // 4
        /// <summary>
        /// 52-week low
        /// </summary>
        public decimal? FiftyTwoWeekLow { get; private set; }                    // 5
        public decimal? CalendarYearHigh { get; private set; }                   // 6
        public decimal? CalendarYearLow { get; private set; }                    // 7
        public decimal? DividendYield { get; private set; }                      // 8
        public decimal? DividendAmount { get; private set; }                     // 9
        public decimal? DividendRate { get; private set; }                       // 10
        public DateTime? PayDate { get; private set; }                           // 11
        public DateTime? ExDividendDate { get; private set; }                    // 12
        // (Reserved)                                               // 13
        // (Reserved)                                               // 14
        // (Reserved)                                               // 15
        public int? ShortInterest { get; private set; }                          // 16
        // (Reserved)                                               // 17
        public decimal? CurrentYearEarningsPerShare { get; private set; }        // 18
        public decimal? NextYearEarningsPerShare { get; private set; }           // 19
        public decimal? FiveYearGrowthPercentage { get; private set; }           // 20
        public int? FiscalYearEnd { get; private set; }                          // 21
        // (Reserved)                                               // 22
        public string CompanyName { get; private set; }                          // 23
        public string RootOptionSymbol { get; private set; }                     // 24
        public decimal? PercentHeldByInstitutions { get; private set; }          // 25
        public decimal? Beta { get; private set; }                               // 26
        public string Leaps { get; private set; }                                // 27
        public decimal? CurrentAssets { get; private set; }                      // 28
        public decimal? CurrentLiabilities { get; private set; }                 // 29
        public DateTime? BalanceSheetDate { get; private set; }                  // 30
        public decimal? LongTermDebt { get; private set; }                       // 31
        public decimal? CommonSharesOutstanding { get; private set; }            // 32
        // (Reserved)                                               // 33
        public string SplitFactor1 { get; private set; }                         // 34
        public string SplitFactor2 { get; private set; }                         // 35
        // (Reserved)                                               // 36
        // (Reserved)                                               // 37
        public string FormatCode { get; private set; }                           // 38
        public int? Precision { get; private set; }                              // 39   
        public int? SIC { get; private set; }                                    // 40
        public decimal? HistoricalVolatility { get; private set; }               // 41
        public string SecurityType { get; private set; }                         // 42
        public string ListedMarket { get; private set; }                         // 43
        public DateTime? FiftyTwoWeekHighDate { get; private set; }              // 44
        public DateTime? FiftyTwoWeekLowDate { get; private set; }               // 45
        public DateTime? CalendarYearHighDate { get; private set; }              // 46
        public DateTime? CalendarYearLowDate { get; private set; }               // 47
        public decimal? YearEndClose { get; private set; }                       // 48
        public DateTime? MaturityDate { get; private set; }                      // 49
        public decimal? CouponRate { get; private set; }                         // 50
        public DateTime? ExpirationDate { get; private set; }                    // 51
        public decimal? StrikePrice { get; private set; }                        // 52
        public int? NAICS { get; private set; }                                  // 53
        public string ExchangeRoot { get; private set; }                         // 54
        public decimal? OptionsPremiumMultiplier { get; private set; }           // 55
        public int? OptionsMultipleDeliverables { get; private set; }            // 56

        public static FundamentalMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new FundamentalMessage(
                values[1].NullIfEmpty(),
                values[2].NullIfEmpty(),
                values[3].ToNullableDecimal(),
                values[4].ToNullableInt(),
                values[5].ToNullableDecimal(),
                values[6].ToNullableDecimal(),
                values[7].ToNullableDecimal(),
                values[8].ToNullableDecimal(),
                values[9].ToNullableDecimal(),
                values[10].ToNullableDecimal(),
                values[11].ToNullableDecimal(),
                values[12].ToNullableDateTime(FundamentalDateTimeFormat),
                values[13].ToNullableDateTime(FundamentalDateTimeFormat),
                //// (Reserved)
                //// (Reserved)
                //// (Reserved)
                values[17].ToNullableInt(),
                //// (Reserved)
                values[19].ToNullableDecimal(),
                values[20].ToNullableDecimal(),
                values[21].ToNullableDecimal(),
                values[22].ToNullableInt(),
                //// (Reserved)    
                values[24].NullIfEmpty(),
                values[25].NullIfEmpty(),
                values[26].ToNullableDecimal(),
                values[27].ToNullableDecimal(),
                values[28].NullIfEmpty(),
                values[29].ToNullableDecimal(),
                values[30].ToNullableDecimal(),
                values[31].ToNullableDateTime(FundamentalDateTimeFormat),
                values[32].ToNullableDecimal(),
                values[33].ToNullableDecimal(),
                //// (Reserved)
                values[35].NullIfEmpty(),
                values[36].NullIfEmpty(),
                //// (Reserved)
                //// (Reserved)
                values[39].NullIfEmpty(),
                values[40].ToNullableInt(),
                values[41].ToNullableInt(),
                values[42].ToNullableDecimal(),
                values[43].NullIfEmpty(),
                values[44].NullIfEmpty(),
                values[45].ToNullableDateTime(FundamentalDateTimeFormat),
                values[46].ToNullableDateTime(FundamentalDateTimeFormat),
                values[47].ToNullableDateTime(FundamentalDateTimeFormat),
                values[48].ToNullableDateTime(FundamentalDateTimeFormat),
                values[49].ToNullableDecimal(),
                values[50].ToNullableDateTime(FundamentalDateTimeFormat),
                values[51].ToNullableDecimal(),
                values[52].ToNullableDateTime(FundamentalDateTimeFormat),
                values[53].ToNullableDecimal(),
                values[54].ToNullableInt(),
                values[55].NullIfEmpty(),
                values[56].ToNullableDecimal(),
                values[57].ToNullableInt()
            );
        }


        public override bool Equals(object obj)
        {
            return obj is FundamentalMessage message &&
                   Symbol == message.Symbol &&
                   ExchangeId == message.ExchangeId &&
                   PE == message.PE &&
                   AverageVolume == message.AverageVolume &&
                   FiftyTwoWeekHigh == message.FiftyTwoWeekHigh &&
                   FiftyTwoWeekLow == message.FiftyTwoWeekLow &&
                   CalendarYearHigh == message.CalendarYearHigh &&
                   CalendarYearLow == message.CalendarYearLow &&
                   DividendYield == message.DividendYield &&
                   DividendAmount == message.DividendAmount &&
                   DividendRate == message.DividendRate &&
                   PayDate == message.PayDate &&
                   ExDividendDate == message.ExDividendDate &&
                   ShortInterest == message.ShortInterest &&
                   CurrentYearEarningsPerShare == message.CurrentYearEarningsPerShare &&
                   NextYearEarningsPerShare == message.NextYearEarningsPerShare &&
                   FiveYearGrowthPercentage == message.FiveYearGrowthPercentage &&
                   FiscalYearEnd == message.FiscalYearEnd &&
                   CompanyName == message.CompanyName &&
                   RootOptionSymbol == message.RootOptionSymbol &&
                   PercentHeldByInstitutions == message.PercentHeldByInstitutions &&
                   Beta == message.Beta &&
                   Leaps == message.Leaps &&
                   CurrentAssets == message.CurrentAssets &&
                   CurrentLiabilities == message.CurrentLiabilities &&
                   BalanceSheetDate == message.BalanceSheetDate &&
                   LongTermDebt == message.LongTermDebt &&
                   CommonSharesOutstanding == message.CommonSharesOutstanding &&
                   SplitFactor1 == message.SplitFactor1 &&
                   SplitFactor2 == message.SplitFactor2 &&
                   FormatCode == message.FormatCode &&
                   Precision == message.Precision &&
                   SIC == message.SIC &&
                   HistoricalVolatility == message.HistoricalVolatility &&
                   SecurityType == message.SecurityType &&
                   ListedMarket == message.ListedMarket &&
                   FiftyTwoWeekHighDate == message.FiftyTwoWeekHighDate &&
                   FiftyTwoWeekLowDate == message.FiftyTwoWeekLowDate &&
                   CalendarYearHighDate == message.CalendarYearHighDate &&
                   CalendarYearLowDate == message.CalendarYearLowDate &&
                   YearEndClose == message.YearEndClose &&
                   MaturityDate == message.MaturityDate &&
                   CouponRate == message.CouponRate &&
                   ExpirationDate == message.ExpirationDate &&
                   StrikePrice == message.StrikePrice &&
                   NAICS == message.NAICS &&
                   ExchangeRoot == message.ExchangeRoot &&
                   OptionsPremiumMultiplier == message.OptionsPremiumMultiplier &&
                   OptionsMultipleDeliverables == message.OptionsMultipleDeliverables;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Symbol);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeId);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(PE);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(AverageVolume);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(FiftyTwoWeekHigh);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(FiftyTwoWeekLow);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CalendarYearHigh);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CalendarYearLow);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(DividendYield);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(DividendAmount);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(DividendRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(PayDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExDividendDate);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(ShortInterest);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CurrentYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(NextYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(FiveYearGrowthPercentage);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(FiscalYearEnd);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(CompanyName);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(RootOptionSymbol);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(PercentHeldByInstitutions);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(Beta);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Leaps);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CurrentAssets);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CurrentLiabilities);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(BalanceSheetDate);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(LongTermDebt);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CommonSharesOutstanding);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor1);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor2);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(FormatCode);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(Precision);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(SIC);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(HistoricalVolatility);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SecurityType);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ListedMarket);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekLowDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearLowDate);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(YearEndClose);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(MaturityDate);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(CouponRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExpirationDate);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(StrikePrice);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(NAICS);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeRoot);
                hash = hash * 29 + EqualityComparer<decimal?>.Default.GetHashCode(OptionsPremiumMultiplier);
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