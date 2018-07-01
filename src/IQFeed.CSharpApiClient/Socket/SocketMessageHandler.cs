using System;
using System.IO;
using IQFeed.CSharpApiClient.Extensions;

namespace IQFeed.CSharpApiClient.Socket
{
    public class SocketMessageHandler : IDisposable
    {
        private readonly char _delimeter;

        private readonly MemoryStream _completeStream;
        private readonly MemoryStream _remainderStream;
        private readonly byte[] _readBytes;

        public SocketMessageHandler(int bufferSize, char delimeter)
        {
            _delimeter = delimeter;

            _completeStream = new MemoryStream(bufferSize);
            _completeStream.Seek(0, SeekOrigin.Begin);

            _remainderStream = new MemoryStream(bufferSize);
            _remainderStream.Seek(0, SeekOrigin.Begin);

            _readBytes = new byte[bufferSize * 2];
        }

        public void Write(byte[] message, int offset, int count)
        {
            // check if delimeter is found
            var delimeterIndex = message.GetLastDelimeterIndex(offset, count, _delimeter);

            // if not found, simply copy bytes into the remainder and return
            if (delimeterIndex == -1)
            {
                _remainderStream.Write(message, offset, count);
                return;
            }

            // if remainder exists, copy bytes into complete
            if (_remainderStream.Position > 0)
            {
                _remainderStream.WriteTo(_completeStream);
                _remainderStream.SetLength(0); // TODO: add a unit test to handle that case!
            }

            // copy received bytes with last delimeter into complete
            _completeStream.Write(message, offset, delimeterIndex + 1);

            // delimeter found at the end of the message
            if (delimeterIndex == count - 1)
                return;

            _remainderStream.Write(message, delimeterIndex + 1, count - delimeterIndex - 1);
        }

        public int TryRead(out byte[] output)
        {
            output = null;
            if (_completeStream.Position == 0)
                return 0;

            var length = (int) _completeStream.Length;
            _completeStream.Position = 0;
            _completeStream.Read(_readBytes, 0, length);
            _completeStream.SetLength(0);
            output = _readBytes;
            return length;
        }

        #region IDisposable

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            _completeStream?.Dispose();
            _remainderStream?.Dispose();
        }

        #endregion
    }
}