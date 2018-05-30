namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public interface IAdminClient: IAdminMessage
    {
        void SetProtocol();
        void SetClientName(string name);
        void RegisterClientApp(string appName);
        void RemoveClientApp(string appName);
        void SetLoginId(string login);
        void SetPassword(string password);
        void SetSaveLoginInfo();
        void SetAutoconnect();
        void SetClientStatusOn();
        void SetClientStatusOff();
        void ReqServerConnect();
        void ReqServerDisconnect();

        // TODO: should be part from another interface
        void Connect();
        void Disconnect();
    }
}