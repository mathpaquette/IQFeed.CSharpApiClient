namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    public class PasswordMessage
    {
        public PasswordMessage(string userPassword)
        {
            UserPassword = userPassword;
        }
        public string UserPassword { get; }
    }
}