using System.Collections;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Extensions.Tests.Common
{
    public class CultureNameTestCase
    {
        public static IEnumerable CultureNames
        {
            get
            {
                yield return new TestCaseData("en-US");
                yield return new TestCaseData("fr-FR");
                yield return new TestCaseData("ru-RU");
                yield return new TestCaseData("ja-JP");
                yield return new TestCaseData("ar-SA");
            }
        }
    }
}