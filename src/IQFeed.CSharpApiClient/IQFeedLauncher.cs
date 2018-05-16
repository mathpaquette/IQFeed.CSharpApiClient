using System;
using System.Configuration;
using System.Threading;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient
{
    public static class IQFeedLauncher
    {
        public static void Start(string login = null, string password = null, string productId = null, string productVersion = null, int sleepTimeMs = 2500)
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

            Thread.Sleep(sleepTimeMs); // TODO: replace this by an active polling to check if the feed has been started
        }
    }
}