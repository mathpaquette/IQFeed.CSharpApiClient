namespace IQFeed.CSharpApiClient.Common.Interfaces
{
    public interface IRequestFormatter
    {
        string SetClientName(string name);
        string SetProtocol(string version);
    }
}