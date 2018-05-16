using System.Text;
using IQFeed.CSharpApiClient.Socket;
using NUnit.Framework;

namespace IQFeed.CSharpApiClient.Tests.Socket
{
    public class SocketMessageHandlerTests
    {
        private SocketMessageHandler _socketMessageHandler;

        [SetUp]
        public void SetUp()
        {
            _socketMessageHandler = new SocketMessageHandler(8192, '\n');
        }

        [Test]
        public void TryRead_Should_Return_Positive_Count_After_Receiving_Delimeter_On_First_Write()
        {
            // Arrange
            var msg = "2008-09-30 16:29:56,26.6000,100,104865900,26.6000,26.6100,2836662,0,0,E,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);

            // Act
            _socketMessageHandler.Write(msgBytes, 0, msgBytes.Length);
            var count = _socketMessageHandler.TryRead(out var readBytes);

            // Assert
            Assert.AreEqual(count, msg.Length);
            Assert.AreEqual(Encoding.ASCII.GetString(readBytes, 0, count), msg);
        }

        [Test]
        public void TryRead_Should_Return_Positive_Count_After_Receiving_Delimeter_On_Second_Write()
        {
            // Arrange
            var msg1 = "2008-09-30 16:29:56,26.6000,100,104865900";
            var msg2 = ",26.6000,26.6100,2836662,0,0,E,\r\n";
            var msg1Bytes = Encoding.ASCII.GetBytes(msg1);
            var msg2Bytes = Encoding.ASCII.GetBytes(msg2);

            // Act
            _socketMessageHandler.Write(msg1Bytes, 0, msg1Bytes.Length);
            _socketMessageHandler.Write(msg2Bytes, 0, msg2Bytes.Length);
            var count = _socketMessageHandler.TryRead(out var readBytes);

            // Assert
            Assert.AreEqual(count, msg1.Length + msg2.Length);
            Assert.AreEqual(Encoding.ASCII.GetString(readBytes, 0, count), msg1 + msg2);
        }

        [Test]
        public void TryRead_Should_Return_Positive_Count_After_Receiving_Delimeter_On_First_Write_With_Remainder()
        {
            // Arrange
            var msg1 = "2008-09-30 16:29:56,26.6000,100,104865900,26.6000,26.6100,2836662,0,0,E,\r\n";
            var msg2 = "2008-09-30 ";
            var msgBytes = Encoding.ASCII.GetBytes(msg1 + msg2);

            // Act
            _socketMessageHandler.Write(msgBytes, 0, msgBytes.Length);
            var count = _socketMessageHandler.TryRead(out var readBytes);

            // Assert
            Assert.AreEqual(count, msg1.Length);
            Assert.AreEqual(Encoding.ASCII.GetString(readBytes, 0, count), msg1);
        }

        [Test]
        public void TryRead_Should_Return_Positive_Count_After_Receiving_Delimeter_On_Second_Write_With_Remainder()
        {
            // Arrange
            var msg1 = "2008-09-30 16:29:56,26.6000,100,104865900";
            var msg2 = ",26.6000,26.6100,2836662,0,0,E,\r\n";
            var msg3 = "2008-09-30 ";
            var msg1Bytes = Encoding.ASCII.GetBytes(msg1);
            var msg2Bytes = Encoding.ASCII.GetBytes(msg2 + msg3);

            // Act
            _socketMessageHandler.Write(msg1Bytes, 0, msg1Bytes.Length);
            _socketMessageHandler.Write(msg2Bytes, 0, msg2Bytes.Length);
            var count = _socketMessageHandler.TryRead(out var readBytes);

            // Assert
            Assert.AreEqual(count, msg1.Length + msg2.Length);
            Assert.AreEqual(Encoding.ASCII.GetString(readBytes, 0, count), msg1 + msg2);
        }

        [Test]
        public void TryRead_Should_Return_Zero_After_One_Succesful_Read()
        {
            // Arrange
            var msg = "2008-09-30 16:29:56,26.6000,100,104865900,26.6000,26.6100,2836662,0,0,E,\r\n";
            var msgBytes = Encoding.ASCII.GetBytes(msg);

            // Act
            byte[] readBytes;
            _socketMessageHandler.Write(msgBytes, 0, msgBytes.Length);
            var count1 = _socketMessageHandler.TryRead(out readBytes);
            var count2 = _socketMessageHandler.TryRead(out readBytes);

            // Assert
            Assert.Greater(count1, 0);
            Assert.AreEqual(count2, 0);
        }

        [Test]
        public void TryRead_Should_Return_Sum_Of_Completed_Messages()
        {
            // Arrange
            var msg1 = "2008-09-30 16:29:56,26.6000,100,104865900,26.6000,26.6100,2836662,0,0,E,\r\n";
            var msg2 = "2008-09-30 16:28:58,26.6000,900,106037244,26.6000,26.6100,2836564,0,0,E,\r\n";
            var msg3 = "2008-09-30 16:28:58,26.6000,900,106037244,26.6000,26.6100,2836564,0,0,E";
            var msg1Bytes = Encoding.ASCII.GetBytes(msg1);
            var msg2Bytes = Encoding.ASCII.GetBytes(msg2 + msg3);

            // Act
            byte[] readBytes;
            _socketMessageHandler.Write(msg1Bytes, 0, msg1Bytes.Length);
            _socketMessageHandler.Write(msg2Bytes, 0, msg2Bytes.Length);
            var count = _socketMessageHandler.TryRead(out readBytes);

            // Assert
            Assert.AreEqual(count, msg1.Length + msg2.Length);
        }
    }
}