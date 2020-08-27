using System.Globalization;
using System.Threading;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Common
{
    public static class TestHelper
    {
        public static void SetThreadCulture(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}