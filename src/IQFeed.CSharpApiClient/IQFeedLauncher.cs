using System;
using System.Configuration;
using IQFeed.CSharpApiClient.Extensions;
using IQFeed.CSharpApiClient.Socket;

namespace IQFeed.CSharpApiClient
{
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

            System.Diagnostics.Process.Start(
                "IQConnect.exe", $"-product {productId} -version {productVersion} -login {login} -password {password} -autoconnect"
            );

            // TODO: check the status of the admin port using stats messages
            var adminPortReady = SocketDiagnostic.IsPortOpen(IQFeedDefault.Hostname, IQFeedDefault.AdminPort, connectionTimeoutMs, retry);
            if (!adminPortReady)
                throw new Exception($"Can't establish TCP connection with host: {IQFeedDefault.Hostname}:{IQFeedDefault.AdminPort}");
        }
    }
}