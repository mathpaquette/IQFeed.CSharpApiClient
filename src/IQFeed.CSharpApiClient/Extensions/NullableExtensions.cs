using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Extensions
{
    public static class NullableExtensions
    {
        public static string ToInvariantString(this DateTime? date, string format)
        {
            return date.HasValue ? date.Value.ToString(format, CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToInvariantString(this TimeSpan? time, string format)
        {
            return time.HasValue ? time.Value.ToString(format, CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToInvariantString(this double? value)
        {
            return value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }

        public static string ToInvariantString(this int? value)
        {
            return value.HasValue ? value.Value.ToString(CultureInfo.InvariantCulture) : string.Empty;
        }
    }
}