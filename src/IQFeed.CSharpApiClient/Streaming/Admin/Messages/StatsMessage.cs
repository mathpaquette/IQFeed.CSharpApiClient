using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class StatsMessage
    {
        public const string StatsMessageDatetimeFormat = "MMM dd H:mmtt";
        private StatsMessage()
        {
            //empty constructor for serialization.
        }
        public StatsMessage(string serverIp, int serverPort, int maxSymbols, int numberOfSymbols, int clientsConnected, int secondsSinceLastUpdate, int reconnections, int attemptedReconnections, DateTime startTime, DateTime marketTime, StatsStatusType status, string iqFeedVersion, string loginId, float totalKBsRecv, float kbsPerSecRecv, float avgKBsPerSecRecv, float totalKBsSent, float kbsPerSecSent, float avgKBsPerSecSent)
        {
            ServerIp = serverIp;
            ServerPort = serverPort;
            MaxSymbols = maxSymbols;
            NumberOfSymbols = numberOfSymbols;
            ClientsConnected = clientsConnected;
            SecondsSinceLastUpdate = secondsSinceLastUpdate;
            Reconnections = reconnections;
            AttemptedReconnections = attemptedReconnections;
            StartTime = startTime;
            MarketTime = marketTime;
            Status = status;
            IQFeedVersion = iqFeedVersion;
            LoginId = loginId;
            TotalKBsRecv = totalKBsRecv;
            KBsPerSecRecv = kbsPerSecRecv;
            AvgKBsPerSecRecv = avgKBsPerSecRecv;
            TotalKBsSent = totalKBsSent;
            KBsPerSecSent = kbsPerSecSent;
            AvgKBsPerSecSent = avgKBsPerSecSent;
        }

        public string ServerIp { get; }
        public int ServerPort { get; }
        public int MaxSymbols { get; }
        public int NumberOfSymbols { get; }
        public int ClientsConnected { get; }
        public int SecondsSinceLastUpdate { get; }
        public int Reconnections { get; }
        public int AttemptedReconnections { get; }
        public DateTime StartTime { get; }
        public DateTime MarketTime { get; }
        public StatsStatusType Status { get; }
        public string IQFeedVersion { get; }
        public string LoginId { get; }
        public float TotalKBsRecv { get; }
        public float KBsPerSecRecv { get; }
        public float AvgKBsPerSecRecv { get; }
        public float TotalKBsSent { get; }
        public float KBsPerSecSent { get; }
        public float AvgKBsPerSecSent { get; }

        public static StatsMessage CreateStatsMessage(string[] values)
        {
            var serverIp = values[1];
            int.TryParse(values[2], out var serverPort);
            int.TryParse(values[3], out var maxSymbols);
            int.TryParse(values[4], out var numberOfSymbols);
            int.TryParse(values[5], out var clientsConnected);
            int.TryParse(values[6], out var secondsSinceLastUpdate);
            int.TryParse(values[7], out var reconnections);
            int.TryParse(values[8], out var attemptedReconnections);
            DateTime.TryParseExact(values[9], StatsMessageDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var startTime);
            DateTime.TryParseExact(values[10], StatsMessageDatetimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var marketTime);
            var status = values[11] == "Connected" ? StatsStatusType.Connected : StatsStatusType.NotConnected;
            var iqFeedVersion = values[12];
            var loginId = values[13];
            float.TryParse(values[14], out var totalKBsRecv);
            float.TryParse(values[15], out var kBsPerSecRecv);
            float.TryParse(values[16], out var avgKBsPerSecRecv);
            float.TryParse(values[17], out var totalKBsSent);
            float.TryParse(values[18], out var kBsPerSecSent);
            float.TryParse(values[19], out var avgKBsPerSecSent);

            return new StatsMessage(
                serverIp,
                serverPort,
                maxSymbols,
                numberOfSymbols,
                clientsConnected,
                secondsSinceLastUpdate,
                reconnections,
                attemptedReconnections,
                startTime,
                marketTime,
                status,
                iqFeedVersion,
                loginId,
                totalKBsRecv,
                kBsPerSecRecv,
                avgKBsPerSecRecv,
                totalKBsSent,
                kBsPerSecSent,
                avgKBsPerSecSent
            );
        }
    }
}