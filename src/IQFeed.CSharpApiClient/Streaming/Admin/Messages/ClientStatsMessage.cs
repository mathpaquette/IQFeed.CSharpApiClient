using System;
using System.Globalization;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class ClientStatsMessage
    {
        public const string ClientStatsDatetimeFormat = "yyyyMMdd HHmmss";

        public ClientType Type { get; private set; }
        public int ClientId { get; private set; }
        public string ClientName { get; private set; }
        public DateTime StartTime { get; private set; }
        public int? Symbols { get; private set; }
        public int? RegionalSymbols { get; private set; }
        public float KbReceived { get; private set; }
        public float KbSent { get; private set; }
        public float KbQueued { get; private set; }

        public ClientStatsMessage(ClientType type, int clientId, string clientName, DateTime startTime, int? symbols, int? regionalSymbols, float kbReceived, float kbSent, float kbQueued)
        {
            Type = type;
            ClientId = clientId;
            ClientName = clientName;
            StartTime = startTime;
            Symbols = symbols;
            RegionalSymbols = regionalSymbols;
            KbReceived = kbReceived;
            KbSent = kbSent;
            KbQueued = kbQueued;
        }

        public static ClientStatsMessage CreateClientStatsMessage(string[] values)
        {
            int.TryParse(values[1], out var clientTypeInt);
            var clientType = (ClientType)clientTypeInt;
            int.TryParse(values[2], out var clientId);
            var clientName = values[3];
            DateTime.TryParseExact(values[4], ClientStatsDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startTime);
            var symbols = values[5].ToNullableInt();
            var regionalSymbols = values[6].ToNullableInt();
            float.TryParse(values[7], out var kbReceived);
            float.TryParse(values[8], out var kbSent);
            float.TryParse(values[9], out var kbQueued);

            return new ClientStatsMessage(clientType, clientId, clientName, startTime, symbols, regionalSymbols, kbReceived, kbSent, kbQueued);
        }
    }
}