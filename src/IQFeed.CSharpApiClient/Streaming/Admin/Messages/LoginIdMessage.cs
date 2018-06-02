namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class LoginIdMessage
    {
        public string UserLoginId { get; }

        public LoginIdMessage(string userLoginId)
        {
            UserLoginId = userLoginId;
        }
    }
}