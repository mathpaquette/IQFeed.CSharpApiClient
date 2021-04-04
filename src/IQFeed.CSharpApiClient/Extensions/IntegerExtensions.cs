namespace IQFeed.CSharpApiClient.Extensions
{
    public static class IntegerExtensions
    {
        /**
         * integer value greater than -1 and less than max 16 bit unsigned integer.
         * Defaults to 0 (All available) for request where not a required field.
         * Reference: http://www.iqfeed.net/dev/upgrade.cfm?protocol=2
         */
        public static int ToMaximumDaysWeeksMonths(this int value)
        {
            return value > short.MaxValue ? 0 : value;
        }
    }
}