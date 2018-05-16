using System;

namespace IQFeed.CSharpApiClient.Socket
{
    public class SocketMessageEventArgs : EventArgs
    {
        public byte[] Message { get; set;}
        public int Count { get; set; }
    }
}