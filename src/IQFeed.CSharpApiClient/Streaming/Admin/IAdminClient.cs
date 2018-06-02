namespace IQFeed.CSharpApiClient.Streaming.Admin
{
    public interface IAdminClient: IAdminMessage
    {
        void SetProtocol();
        void SetClientName(string name);
        void RegisterClientApp(string productId, string productVersion);
        void RemoveClientApp(string productId, string productVersion);
        void SetLoginId(string userLoginId);
        void SetPassword(string userPassword);
        void SetSaveLoginInfo(bool on = true);
        void SetAutoconnect(bool on = true);
        void ReqServerConnect();
        void ReqServerDisconnect();
        void SetClientStats(bool on = true);


        // TODO: should be part from another interface
        void Connect();
        void Disconnect();
    }
}