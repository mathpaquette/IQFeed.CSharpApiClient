using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Common
{
    /// <summary>
    /// Helper class used for parsing IQ Feed fields
    /// </summary>
    public static class FieldParser
    {
        public static double ParseDouble(string value)
        {
            double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue);
            return parsedValue;
        }

        public static int ParseInt(string value)
        {
            int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedValue);
            return parsedValue;
        }

        public static TimeSpan ParseTime(string value, string format)
        {
            DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedValue);
            return parsedValue.TimeOfDay;
        }

        public static DateTime ParseDate(string value, string format)
        {
            DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedValue);
            return parsedValue;
        }
    }
}