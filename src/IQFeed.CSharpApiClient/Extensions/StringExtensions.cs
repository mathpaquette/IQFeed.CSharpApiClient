using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Extensions
{
    public static class StringExtensions
    {
        public static string NullIfEmpty(this string s)
        {
            if (s == null)
                return null;

            return s.Trim() == string.Empty ? null : s;
        }

        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out var i))
                return i;
            return null;
        }

        public static float? ToNullableFloat(this string s)
        {
            if (float.TryParse(s, out var f))
                return f;
            return null;
        }

        public static DateTime? ToNullableDateTime(this string s, string format)
        {
            if (DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
                return d;
            return null;
        }
    }
}