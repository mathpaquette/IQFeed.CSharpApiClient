using System;
using IQFeed.CSharpApiClient.Extensions;

// ReSharper disable InconsistentNaming

namespace IQFeed.CSharpApiClient.Streaming.Level1.Messages
{
    public class FundamentalMessage
    {
        public const string FundamentalDatetimeFormat = "MM/dd/yyyy";

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

        public static FundamentalMessage CreateFundamentalMessage(string[] values)
        {
            return new FundamentalMessage(
                values[0].NullIfEmpty(),
                values[1].NullIfEmpty(),
                values[2].ToNullableFloat(),
                values[3].ToNullableInt(),
                values[4].ToNullableFloat(),
                values[5].ToNullableFloat(),
                values[6].ToNullableFloat(),
                values[7].ToNullableFloat(),
                values[8].ToNullableFloat(),
                values[9].ToNullableFloat(),
                values[10].ToNullableFloat(),
                values[11].ToNullableDateTime(FundamentalDatetimeFormat),
                values[12].ToNullableDateTime(FundamentalDatetimeFormat),
                //// (Reserved)
                //// (Reserved)
                //// (Reserved)
                values[16].ToNullableInt(),
                //// (Reserved)
                values[18].ToNullableFloat(),
                values[19].ToNullableFloat(),
                values[20].ToNullableFloat(),
                values[21].ToNullableInt(),
                //// (Reserved)    
                values[23].NullIfEmpty(),
                values[24].NullIfEmpty(),
                values[25].ToNullableFloat(),
                values[26].ToNullableFloat(),
                values[27].NullIfEmpty(),
                values[28].ToNullableFloat(),
                values[29].ToNullableFloat(),
                values[30].ToNullableDateTime(FundamentalDatetimeFormat),
                values[31].ToNullableFloat(),
                values[32].ToNullableFloat(),
                //// (Reserved)
                values[34].NullIfEmpty(),
                values[35].NullIfEmpty(),
                //// (Reserved)
                //// (Reserved)
                values[38].NullIfEmpty(),
                values[39].ToNullableInt(),
                values[40].ToNullableInt(),
                values[41].ToNullableFloat(),
                values[42].NullIfEmpty(),
                values[43].NullIfEmpty(),
                values[44].ToNullableDateTime(FundamentalDatetimeFormat),
                values[45].ToNullableDateTime(FundamentalDatetimeFormat),
                values[46].ToNullableDateTime(FundamentalDatetimeFormat),
                values[47].ToNullableDateTime(FundamentalDatetimeFormat),
                values[48].ToNullableFloat(),
                values[49].ToNullableDateTime(FundamentalDatetimeFormat),
                values[50].ToNullableFloat(),
                values[51].ToNullableDateTime(FundamentalDatetimeFormat),
                values[52].ToNullableFloat(),
                values[53].ToNullableInt(),
                values[54].NullIfEmpty(),
                values[55].ToNullableFloat(),
                values[56].ToNullableInt()
            );
        }
    }
}