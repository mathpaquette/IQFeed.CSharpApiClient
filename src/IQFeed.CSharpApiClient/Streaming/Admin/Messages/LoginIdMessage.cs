using System;

namespace IQFeed.CSharpApiClient.Streaming.Admin.Messages
{
    [Serializable]
    public class LoginIdMessage
    {
        public string UserLoginId { get; }
        private LoginIdMessage()
        {
            //empty constructor for serialization.
        }
        public LoginIdMessage(string userLoginId)
        {
            UserLoginId = userLoginId;
        }
    }
}