using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Examples.Common
{
    public interface IExample
    {
        void Run();
        bool Enable { get; }
        string Name { get; }
    }
}