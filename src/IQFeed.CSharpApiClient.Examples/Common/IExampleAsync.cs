using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Examples.Common
{
    public interface IExampleAsync : IExample
    {
        Task RunAsync();
    }
}