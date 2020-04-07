using System;
using System.Collections.Generic;
using IQFeed.CSharpApiClient.Extensions;

// ReSharper disable InconsistentNaming

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class FundamentalMessage
    {
        public const string FundamentalDateTimeFormat = "MM/dd/yyyy";
        public const string FundamentalTimeSpanFormat = "HH:mm:ss";

        public FundamentalMessage(
            string symbol,
            string exchangeId,
            double? pe,
            int? averageVolume,
            double? fiftyTwoWeekHigh,
            double? fiftyTwoWeekLow,
            double? calendarYearHigh,
            double? calendarYearLow,
            double? dividendYield,
            double? dividendAmount,
            double? dividendRate,
            DateTime? payDate,
            DateTime? exDividendDate,
            int? shortInterest,
            double? currentYearEarningsPerShare,
            double? nextYearEarningsPerShare,
            double? fiveYearGrowthPercentage,
            int? fiscalYearEnd,
            string companyName,
            string rootOptionSymbol,
            double? percentHeldByInstitutions,
            double? beta,
            string leaps,
            double? currentAssets,
            double? currentLiabilities,
            DateTime? balanceSheetDate,
            double? longTermDebt,
            double? commonSharesOutstanding,
            string splitFactor1,
            string splitFactor2,
            string formatCode,
            int? precision,
            int? sic,
            double? historicalVolatility,
            string securityType,
            string listedMarket,
            DateTime? fiftyTwoWeekHighDate,
            DateTime? fiftyTwoWeekLowDate,
            DateTime? calendarYearHighDate,
            DateTime? calendarYearLowDate,
            double? yearEndClose,
            DateTime? maturityDate,
            double? couponRate,
            DateTime? expirationDate,
            double? strikePrice,
            int? naics,
            string exchangeRoot,
            double? optionsPremiumMultiplier,
            int? optionsMultipleDeliverables,
            TimeSpan? sessionOpenTime,
            TimeSpan? sessionCloseTime,
            string baseCurrency,
            string contractSize,
            string contractMonths,
            double? minimumTickSize,
            DateTime? firstDeliveryDate,
            string figi,
            int? securitySubType
            )
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
        public double? PE { get; private set; }                                 // 2
        public int? AverageVolume { get; private set; }                          // 3
        /// <summary>
        /// 52-week high
        /// </summary>
        public double? FiftyTwoWeekHigh { get; private set; }                   // 4
        /// <summary>
        /// 52-week low
        /// </summary>
        public double? FiftyTwoWeekLow { get; private set; }                    // 5
        public double? CalendarYearHigh { get; private set; }                   // 6
        public double? CalendarYearLow { get; private set; }                    // 7
        public double? DividendYield { get; private set; }                      // 8
        public double? DividendAmount { get; private set; }                     // 9
        public double? DividendRate { get; private set; }                       // 10
        public DateTime? PayDate { get; private set; }                           // 11
        public DateTime? ExDividendDate { get; private set; }                    // 12
        // (Reserved)                                               // 13
        // (Reserved)                                               // 14
        // (Reserved)                                               // 15
        public int? ShortInterest { get; private set; }                          // 16
        // (Reserved)                                               // 17
        public double? CurrentYearEarningsPerShare { get; private set; }        // 18
        public double? NextYearEarningsPerShare { get; private set; }           // 19
        public double? FiveYearGrowthPercentage { get; private set; }           // 20
        public int? FiscalYearEnd { get; private set; }                          // 21
        // (Reserved)                                               // 22
        public string CompanyName { get; private set; }                          // 23
        public string RootOptionSymbol { get; private set; }                     // 24
        public double? PercentHeldByInstitutions { get; private set; }          // 25
        public double? Beta { get; private set; }                               // 26
        public string Leaps { get; private set; }                                // 27
        public double? CurrentAssets { get; private set; }                      // 28
        public double? CurrentLiabilities { get; private set; }                 // 29
        public DateTime? BalanceSheetDate { get; private set; }                  // 30
        public double? LongTermDebt { get; private set; }                       // 31
        public double? CommonSharesOutstanding { get; private set; }            // 32
        // (Reserved)                                               // 33
        public string SplitFactor1 { get; private set; }                         // 34
        public string SplitFactor2 { get; private set; }                         // 35
        // (Reserved)                                               // 36
        // (Reserved)                                               // 37
        public string FormatCode { get; private set; }                           // 38
        public int? Precision { get; private set; }                              // 39   
        public int? SIC { get; private set; }                                    // 40
        public double? HistoricalVolatility { get; private set; }               // 41
        public string SecurityType { get; private set; }                         // 42
        public string ListedMarket { get; private set; }                         // 43
        public DateTime? FiftyTwoWeekHighDate { get; private set; }              // 44
        public DateTime? FiftyTwoWeekLowDate { get; private set; }               // 45
        public DateTime? CalendarYearHighDate { get; private set; }              // 46
        public DateTime? CalendarYearLowDate { get; private set; }               // 47
        public double? YearEndClose { get; private set; }                       // 48
        public DateTime? MaturityDate { get; private set; }                      // 49
        public double? CouponRate { get; private set; }                         // 50
        public DateTime? ExpirationDate { get; private set; }                    // 51
        public double? StrikePrice { get; private set; }                        // 52
        public int? NAICS { get; private set; }                                  // 53
        public string ExchangeRoot { get; private set; }                         // 54
        public double? OptionsPremiumMultiplier { get; private set; }           // 55
        public int? OptionsMultipleDeliverables { get; private set; }            // 56
        public TimeSpan? SessionOpenTime { get; private set; }                  // 57
        public TimeSpan? SessionCloseTime { get; private set; }                 // 58
        public string BaseCurrency { get; private set; }                        // 59
        public string ContractSize { get; private set; }                        // 60
        public string ContractMonths { get; private set; }                      // 61
        public double? MinimumTickSize { get; private set; }                      // 62
        public DateTime? FirstDeliveryDate { get; private set; }                // 63
        public string FIGI { get; private set; }                                // 64
        public int? SecuritySubType { get; private set; }                        // 65

        public static FundamentalMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new FundamentalMessage(
                values[1].NullIfEmpty(),
                values[2].NullIfEmpty(),
                values[3].ToNullableDouble(),
                values[4].ToNullableInt(),
                values[5].ToNullableDouble(),
                values[6].ToNullableDouble(),
                values[7].ToNullableDouble(),
                values[8].ToNullableDouble(),
                values[9].ToNullableDouble(),
                values[10].ToNullableDouble(),
                values[11].ToNullableDouble(),
                values[12].ToNullableDateTime(FundamentalDateTimeFormat),
                values[13].ToNullableDateTime(FundamentalDateTimeFormat),
                //// (Reserved)
                //// (Reserved)
                //// (Reserved)
                values[17].ToNullableInt(),
                //// (Reserved)
                values[19].ToNullableDouble(),
                values[20].ToNullableDouble(),
                values[21].ToNullableDouble(),
                values[22].ToNullableInt(),
                //// (Reserved)    
                values[24].NullIfEmpty(),
                values[25].NullIfEmpty(),
                values[26].ToNullableDouble(),
                values[27].ToNullableDouble(),
                values[28].NullIfEmpty(),
                values[29].ToNullableDouble(),
                values[30].ToNullableDouble(),
                values[31].ToNullableDateTime(FundamentalDateTimeFormat),
                values[32].ToNullableDouble(),
                values[33].ToNullableDouble(),
                //// (Reserved)
                values[35].NullIfEmpty(),
                values[36].NullIfEmpty(),
                //// (Reserved)
                //// (Reserved)
                values[39].NullIfEmpty(),
                values[40].ToNullableInt(),
                values[41].ToNullableInt(),
                values[42].ToNullableDouble(),
                values[43].NullIfEmpty(),
                values[44].NullIfEmpty(),
                values[45].ToNullableDateTime(FundamentalDateTimeFormat),
                values[46].ToNullableDateTime(FundamentalDateTimeFormat),
                values[47].ToNullableDateTime(FundamentalDateTimeFormat),
                values[48].ToNullableDateTime(FundamentalDateTimeFormat),
                values[49].ToNullableDouble(),
                values[50].ToNullableDateTime(FundamentalDateTimeFormat),
                values[51].ToNullableDouble(),
                values[52].ToNullableDateTime(FundamentalDateTimeFormat),
                values[53].ToNullableDouble(),
                values[54].ToNullableInt(),
                values[55].NullIfEmpty(),
                values[56].ToNullableDouble(),
                values[57].ToNullableInt(),
                values[58].ToNullableTimeSpan(FundamentalTimeSpanFormat),
                values[59].ToNullableTimeSpan(FundamentalTimeSpanFormat),
                values[60].NullIfEmpty(),
                values[61].NullIfEmpty(),
                values[62],
                values[63].ToNullableDouble(),
                values[64].ToNullableDateTime(FundamentalDateTimeFormat),
                values[65],
                values[66].ToNullableInt()
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
                   OptionsMultipleDeliverables == message.OptionsMultipleDeliverables &&
                   SessionOpenTime == message.SessionOpenTime &&
                   SessionCloseTime == message.SessionCloseTime &&
                   BaseCurrency == message.BaseCurrency &&
                   ContractSize == message.ContractSize &&
                   ContractMonths == message.ContractMonths &&
                   MinimumTickSize == message.MinimumTickSize &&
                   FirstDeliveryDate == message.FirstDeliveryDate &&
                   FIGI == message.FIGI &&
                   SecuritySubType == message.SecuritySubType;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Symbol);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeId);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(PE);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(AverageVolume);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(FiftyTwoWeekHigh);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(FiftyTwoWeekLow);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CalendarYearHigh);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CalendarYearLow);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(DividendYield);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(DividendAmount);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(DividendRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(PayDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExDividendDate);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(ShortInterest);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CurrentYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(NextYearEarningsPerShare);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(FiveYearGrowthPercentage);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(FiscalYearEnd);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(CompanyName);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(RootOptionSymbol);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(PercentHeldByInstitutions);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(Beta);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(Leaps);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CurrentAssets);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CurrentLiabilities);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(BalanceSheetDate);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(LongTermDebt);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CommonSharesOutstanding);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor1);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SplitFactor2);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(FormatCode);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(Precision);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(SIC);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(HistoricalVolatility);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(SecurityType);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ListedMarket);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FiftyTwoWeekLowDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearHighDate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(CalendarYearLowDate);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(YearEndClose);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(MaturityDate);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(CouponRate);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(ExpirationDate);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(StrikePrice);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(NAICS);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ExchangeRoot);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(OptionsPremiumMultiplier);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(OptionsMultipleDeliverables);

                hash = hash * 29 + EqualityComparer<TimeSpan?>.Default.GetHashCode(SessionOpenTime);
                hash = hash * 29 + EqualityComparer<TimeSpan?>.Default.GetHashCode(SessionCloseTime);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(BaseCurrency);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ContractSize);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(ContractMonths);
                hash = hash * 29 + EqualityComparer<double?>.Default.GetHashCode(MinimumTickSize);
                hash = hash * 29 + EqualityComparer<DateTime?>.Default.GetHashCode(FirstDeliveryDate);
                hash = hash * 29 + EqualityComparer<string>.Default.GetHashCode(FIGI);
                hash = hash * 29 + EqualityComparer<int?>.Default.GetHashCode(SecuritySubType);
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(Symbol)}: {Symbol}, {nameof(ExchangeId)}: {ExchangeId}, {nameof(PE)}: {PE}, {nameof(AverageVolume)}: {AverageVolume}, {nameof(FiftyTwoWeekHigh)}: {FiftyTwoWeekHigh}, {nameof(FiftyTwoWeekLow)}: {FiftyTwoWeekLow}, {nameof(CalendarYearHigh)}: {CalendarYearHigh}, {nameof(CalendarYearLow)}: {CalendarYearLow}, {nameof(DividendYield)}: {DividendYield}, {nameof(DividendAmount)}: {DividendAmount}, {nameof(DividendRate)}: {DividendRate}, {nameof(PayDate)}: {PayDate}, {nameof(ExDividendDate)}: {ExDividendDate}, {nameof(ShortInterest)}: {ShortInterest}, {nameof(CurrentYearEarningsPerShare)}: {CurrentYearEarningsPerShare}, {nameof(NextYearEarningsPerShare)}: {NextYearEarningsPerShare}, {nameof(FiveYearGrowthPercentage)}: {FiveYearGrowthPercentage}, {nameof(FiscalYearEnd)}: {FiscalYearEnd}, {nameof(CompanyName)}: {CompanyName}, {nameof(RootOptionSymbol)}: {RootOptionSymbol}, {nameof(PercentHeldByInstitutions)}: {PercentHeldByInstitutions}, {nameof(Beta)}: {Beta}, {nameof(Leaps)}: {Leaps}, {nameof(CurrentAssets)}: {CurrentAssets}, {nameof(CurrentLiabilities)}: {CurrentLiabilities}, {nameof(BalanceSheetDate)}: {BalanceSheetDate}, {nameof(LongTermDebt)}: {LongTermDebt}, {nameof(CommonSharesOutstanding)}: {CommonSharesOutstanding}, {nameof(SplitFactor1)}: {SplitFactor1}, {nameof(SplitFactor2)}: {SplitFactor2}, {nameof(FormatCode)}: {FormatCode}, {nameof(Precision)}: {Precision}, {nameof(SIC)}: {SIC}, {nameof(HistoricalVolatility)}: {HistoricalVolatility}, {nameof(SecurityType)}: {SecurityType}, {nameof(ListedMarket)}: {ListedMarket}, {nameof(FiftyTwoWeekHighDate)}: {FiftyTwoWeekHighDate}, {nameof(FiftyTwoWeekLowDate)}: {FiftyTwoWeekLowDate}, {nameof(CalendarYearHighDate)}: {CalendarYearHighDate}, {nameof(CalendarYearLowDate)}: {CalendarYearLowDate}, {nameof(YearEndClose)}: {YearEndClose}, {nameof(MaturityDate)}: {MaturityDate}, {nameof(CouponRate)}: {CouponRate}, {nameof(ExpirationDate)}: {ExpirationDate}, {nameof(StrikePrice)}: {StrikePrice}, {nameof(NAICS)}: {NAICS}, {nameof(ExchangeRoot)}: {ExchangeRoot}, {nameof(OptionsPremiumMultiplier)}: {OptionsPremiumMultiplier}, {nameof(OptionsMultipleDeliverables)}: {OptionsMultipleDeliverables}";
        }
    }
}