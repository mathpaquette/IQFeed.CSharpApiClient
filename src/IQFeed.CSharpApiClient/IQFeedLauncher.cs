using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Socket;
using IQFeed.CSharpApiClient.Streaming.Admin;
using IQFeed.CSharpApiClient.Streaming.Admin.Messages;

namespace IQFeed.CSharpApiClient
{
    // ReSharper disable once InconsistentNaming
    public static class IQFeedLauncher
    {
        public static void Start(string login = null, string password = null, string productId = null, string productVersion = null, int connectionTimeoutMs = 100, int retry = 50)
        {
             var appSettings = ConfigurationManager.AppSettings;

            login = login ??
                    Environment.GetEnvironmentVariable("IQCONNECT_LOGIN") ??
                    appSettings["IQConnect:login"].NullIfEmpty() ??
                    throw new Exception("Unable to find IQConnect login from environment variable or app.config");

            password = password ??
                       Environment.GetEnvironmentVariable("IQCONNECT_PASSWORD") ??
                       appSettings["IQConnect:password"].NullIfEmpty() ??
                       throw new Exception("Unable to find IQConnect password from environment variable or app.config");

            productId = productId ??
                        Environment.GetEnvironmentVariable("IQCONNECT_PRODUCT_ID") ??
                        appSettings["IQConnect:product_id"].NullIfEmpty() ??
                        throw new Exception("Unable to find IQConnect product ID from environment variable or app.config");

            productVersion = productVersion ??
                             Environment.GetEnvironmentVariable("IQCONNECT_PRODUCT_VERSION") ??
                             appSettings["IQConnect:product_version"].NullIfEmpty() ??
                             "1.0.0.0";

            var iqConnectParameters = $"-product {productId} -version {productVersion} -login {login} -password {password} -autoconnect";
            Process.Start("IQConnect.exe", iqConnectParameters);

            WaitForAdminPortReady(connectionTimeoutMs, retry);
            WaitForServerConnectedStatus(IQFeedDefault.Hostname, IQFeedDefault.AdminPort);
        }

        public static void Terminate()
        {
            foreach (var process in Process.GetProcessesByName("IQConnect"))
            {
                process.Kill();
            }
        }

        private static void WaitForAdminPortReady(int connectionTimeoutMs, int retry)
        {
            var adminPortReady = SocketDiagnostic.IsPortOpen(IQFeedDefault.Hostname, IQFeedDefault.AdminPort, connectionTimeoutMs, retry);
            if (!adminPortReady)
                throw new Exception($"Can't establish TCP connection with host: {IQFeedDefault.Hostname}:{IQFeedDefault.AdminPort}");
        }

        private static void WaitForServerConnectedStatus(string host, int port, int timeoutMs = 10000)
        {
            var manualResetEvent = new ManualResetEvent(false);
            var adminClient = AdminClientFactory.CreateNew(host, port);

            adminClient.Stats += AdminClientOnStats;
            adminClient.Connect();

            var connected = manualResetEvent.WaitOne(timeoutMs);
            if (!connected)
                throw new Exception($"Haven't received connected status with host: {host}:{port}");

            adminClient.Stats -= AdminClientOnStats;
            adminClient.Disconnect();

            void AdminClientOnStats(StatsMessage message)
            {
                if (message.Status == StatsStatusType.Connected)
                    manualResetEvent.Set();
            }
        }
    }
}