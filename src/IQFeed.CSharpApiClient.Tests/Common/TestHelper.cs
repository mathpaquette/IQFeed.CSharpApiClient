using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace IQFeed.CSharpApiClient.Tests.Common
{
    public static class TestHelper
    {
        public static void SetThreadCulture(string cultureName)
        {
            var culture = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public static byte[] GetMessageBytes(IEnumerable<string> messages)
        {
            return Encoding.ASCII.GetBytes(messages.Aggregate((acc, msg) => acc += msg));
        }

        public static byte[] GetMessageBytes(string message)
        {
            return GetMessageBytes(new List<string>() { message });
        }
    }
}