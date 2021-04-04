using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Extensions
{
    public static class StringExtensions
    {
        private static readonly string[] LineDelimiter = { IQFeedDefault.ProtocolTerminatingCharacters };

        public static string NullIfEmpty(this string s)
        {
            if (s == null)
                return null;

            return s.Trim() == string.Empty ? null : s;
        }

        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var i))
                return i;
            return null;
        }

        public static double? ToNullableDouble(this string s)
        {
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var d))
                return d;
            return null;
        }

        public static DateTime? ToNullableDateTime(this string s, string format)
        {
            if (DateTime.TryParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var d))
                return d;
            return null;
        }

        public static TimeSpan? ToNullableTimeSpan(this string s, string format)
        {
            if (TimeSpan.TryParseExact(s, format, CultureInfo.InvariantCulture, TimeSpanStyles.None, out var d))
                return d;
            return null;
        }

        public static string[] SplitFeedMessage(this string s)
        {
            return s.Split(IQFeedDefault.ProtocolDelimiterCharacter);
        }

        public static string[] SplitFeedMessage(this string s, int expectedParts)
        {
            var parts = new string[expectedParts];
            var partStartIndex = 0;
            for (var partIndex = 0; partIndex < expectedParts - 1; ++partIndex)
            {
                var nextDelimiterIndex = s.IndexOf(IQFeedDefault.ProtocolDelimiterCharacter, partStartIndex);
                if (nextDelimiterIndex == -1)
                {
                    throw new ArgumentException($"The specified string '{s}' doesn't contain expected number of parts - {expectedParts}");
                }
                parts[partIndex] = s.Substring(partStartIndex, nextDelimiterIndex - partStartIndex);
                partStartIndex = nextDelimiterIndex + 1;
            }

            // take the last part entirely (except for the last delimiter value)	
            parts[expectedParts - 1] = s.Substring(partStartIndex, s.Length - partStartIndex - 1);
            return parts;
        }

        public static string[] SplitFeedLine(this string s)
        {
            return s.Split(LineDelimiter, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int GetHashCodeOrDefault(this string s)
        {
            return s != null ? s.GetHashCode() : 0;
        }

        public static string OrNullString(this string s)
        {
            return s ?? "<NULL>";
        }
    }
}