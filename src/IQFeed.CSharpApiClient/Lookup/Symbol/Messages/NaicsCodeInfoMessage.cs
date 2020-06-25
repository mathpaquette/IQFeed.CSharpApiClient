﻿using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Lookup.Symbol.Messages
{
    public class NaicsCodeInfoMessage
    {
        public NaicsCodeInfoMessage(int naicsCode, string description, string requestId = null)
        {
            NaicsCode = naicsCode;
            Description = description;
            RequestId = requestId;
        }

        public int NaicsCode { get; private set; }
        public string Description { get; private set; }
        public string RequestId { get; private set; }

        public static NaicsCodeInfoMessage Parse(string message)
        {
            var values = message.SplitFeedMessage();

            return new NaicsCodeInfoMessage(
                int.Parse(values[0], CultureInfo.InvariantCulture),
                values[1]);
        }

        public static NaicsCodeInfoMessage ParseWithRequestId(string message)
        {
            var values = message.SplitFeedMessage();
            var requestId = values[0];

            return new NaicsCodeInfoMessage(
                int.Parse(values[1], CultureInfo.InvariantCulture),
                values[2],
                requestId);
        }

        public override bool Equals(object obj)
        {
            return obj is NaicsCodeInfoMessage message &&
                   RequestId == message.RequestId &&
                   NaicsCode == message.NaicsCode &&
                   Description == message.Description;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 17;
                hash = hash * 29 + RequestId != null ? RequestId.GetHashCode() : 0;
                hash = hash * 29 + NaicsCode.GetHashCode();
                hash = hash * 29 + Description.GetHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return $"{nameof(NaicsCode)}: {NaicsCode}, {nameof(Description)}: {Description}, {nameof(RequestId)}: {RequestId}";
        }
    }
}