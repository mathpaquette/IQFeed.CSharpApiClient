using System;
using IQFeed.CSharpApiClient.Common;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Lookup.News.Enums;

namespace IQFeed.CSharpApiClient.Lookup.News
{
    public class NewsRequestFormatter : RequestFormatter
    {
        public const string NewsDateFormat = "yyyyMMdd";

        // NCG,[XML/Text],[RequestID]<CR><LF>
        public string ReqNewsConfiguration(FormatType formatType, string requestId = null)
        {
            var formattedFormat = formatType.ToString().ToLowerInvariant().Substring(0, 1);
            return $"NCG,{formattedFormat},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        // NHL,[Sources],[Symbols],[XML/Text],[Limit],[Date],[RequestID]
        public string ReqNewsHeadlines(string[] sources = null, string[] symbols = null, FormatType? formatType = null, int? limit = null, DateTime? date = null, string requestId = null)
        {
            var sourcesCsv = string.Join(";", sources ?? Array.Empty<string>());
            var symbolsCsv = string.Join(";", symbols ?? Array.Empty<string>());
            var formattedFormat = formatType?.ToString().ToLowerInvariant().Substring(0, 1);

            return $"NHL,{sourcesCsv},{symbolsCsv},{formattedFormat},{limit},{date.ToInvariantString(NewsDateFormat)},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        // NSY,[ID],[XML/Text/Email],[DeliverTo],[RequestID]<CR><LF>
        public string ReqNewsStory(string id, NewsFormatType? formatType = null, string deliverTo = null, string requestId = null)
        {
            var formattedFormat = formatType?.ToString().ToLowerInvariant().Substring(0, 1);
            return $"NSY,{id},{formattedFormat},{deliverTo},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }

        // NSC,[Symbols],[XML/Text],[Sources],[DateRange],[RequestID]<CR>
        public string ReqNewsStoryCount(string[] symbols, FormatType? formatType = null, string[] sources = null, DateTime? fromDate = null, DateTime? toDate = null, string requestId = null)
        {
            var symbolsCsv = string.Join(";", symbols ?? Array.Empty<string>());
            var formattedFormat = formatType?.ToString().ToLowerInvariant().Substring(0, 1);
            var sourcesCsv = string.Join(";", sources ?? Array.Empty<string>());
            var dateRange = fromDate.HasValue && toDate.HasValue ? $"{fromDate.ToInvariantString(NewsDateFormat)}-{toDate.ToInvariantString(NewsDateFormat)}" : fromDate.ToInvariantString(NewsDateFormat);

            return $"NSC,{symbolsCsv},{formattedFormat},{sourcesCsv},{dateRange},{requestId}{IQFeedDefault.ProtocolTerminatingCharacters}";
        }
    }
}