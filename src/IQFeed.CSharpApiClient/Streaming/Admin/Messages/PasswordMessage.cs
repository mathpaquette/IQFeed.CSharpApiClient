using System;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class PasswordMessage
    {
        private PasswordMessage()
        {
            //empty constructor for serialization.
        }
        public PasswordMessage(string userPassword)
        {
            UserPassword = userPassword;
        }
        public string UserPassword { get; }
    }
}