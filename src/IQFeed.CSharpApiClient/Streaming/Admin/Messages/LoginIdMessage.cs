namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class LoginIdMessage
    {
        public string UserLoginId { get; private set; }

        public LoginIdMessage(string userLoginId)
        {
            UserLoginId = userLoginId;
        }
    }
}