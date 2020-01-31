using System;
using System.Globalization;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class StatsMessage
    {
        public const string StatsMessageDatetimeFormat = "MMM dd H:mmtt";

        public StatsMessage(
            string serverIp, 
            int serverPort,
            int maxSymbols, 
            int numberOfSymbols, 
            int clientsConnected,
            int secondsSinceLastUpdate,
            int reconnections, 
            int attemptedReconnections,
            DateTime startTime, 
            DateTime marketTime, 
            StatsStatusType status,
            string iqFeedVersion, 
            string loginId,
            double totalKBsRecv,
            double kbsPerSecRecv,
            double avgKBsPerSecRecv,
            double totalKBsSent,
            double kbsPerSecSent,
            double avgKBsPerSecSent)
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

        public string ServerIp { get; private set; }
        public int ServerPort { get; private set; }
        public int MaxSymbols { get; private set; }
        public int NumberOfSymbols { get; private set; }
        public int ClientsConnected { get; private set; }
        public int SecondsSinceLastUpdate { get; private set; }
        public int Reconnections { get; private set; }
        public int AttemptedReconnections { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime MarketTime { get; private set; }
        public StatsStatusType Status { get; private set; }
        public string IQFeedVersion { get; private set; }
        public string LoginId { get; private set; }
        public double TotalKBsRecv { get; private set; }
        public double KBsPerSecRecv { get; private set; }
        public double AvgKBsPerSecRecv { get; private set; }
        public double TotalKBsSent { get; private set; }
        public double KBsPerSecSent { get; private set; }
        public double AvgKBsPerSecSent { get; private set; }

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
            double.TryParse(values[14], out var totalKBsRecv);
            double.TryParse(values[15], out var kBsPerSecRecv);
            double.TryParse(values[16], out var avgKBsPerSecRecv);
            double.TryParse(values[17], out var totalKBsSent);
            double.TryParse(values[18], out var kBsPerSecSent);
            double.TryParse(values[19], out var avgKBsPerSecSent);

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