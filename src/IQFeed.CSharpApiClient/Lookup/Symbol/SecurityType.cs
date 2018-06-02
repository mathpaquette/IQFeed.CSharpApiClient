namespace IQFeed.CSharpApiClient.Lookup.Symbol
{
    public enum SecurityType
    {
        /// <summary>
        /// Equity
        /// </summary>
        EQUITY = 1,

        /// <summary>
        /// Index/Equity Option
        /// </summary>
        IEOPTION = 2,

        /// <summary>
        /// Mutual Fund
        /// </summary>
        MUTUAL = 3,

        /// <summary>
        /// Money Market Fund
        /// </summary>
        MONEY = 4,

        /// <summary>
        /// Bond
        /// </summary>
        BONDS = 5,

        /// <summary>
        /// Index
        /// </summary>
        INDEX = 6,

        /// <summary>
        /// Market Statistic
        /// </summary>
        MKTSTATS = 7,

        /// <summary>
        /// Future
        /// </summary>
        FUTURE = 8,

        /// <summary>
        /// Future Option
        /// </summary>
        FOPTION = 9,

        /// <summary>
        /// Future Spread
        /// </summary>
        SPREAD = 10,

        /// <summary>
        /// Spot (Cash) Price
        /// </summary>
        SPOT = 11,

        /// <summary>
        /// Forward
        /// </summary>
        FORWARD = 12,

        /// <summary>
        /// DTN Calculated Statistic
        /// </summary>
        CALC = 13,

        /// <summary>
        /// Calculated Strip
        /// </summary>
        STRIP = 14,

        /// <summary>
        /// Single Stock Future
        /// </summary>
        SSFUTURE = 15,

        /// <summary>
        /// Foreign Monetary Exchange
        /// </summary>
        FOREX = 16,

        /// <summary>
        /// Future Market Depth
        /// </summary>
        MKTDEPTH = 17,

        /// <summary>
        /// Precious Metals
        /// </summary>
        PRECMTL = 18,
    }
}