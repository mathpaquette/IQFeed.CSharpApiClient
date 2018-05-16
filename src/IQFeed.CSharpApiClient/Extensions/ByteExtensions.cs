namespace IQFeed.CSharpApiClient.Extensions
{
    public static class ByteExtensions
    {
        public static int GetLastDelimeterIndex(this byte[] buffer, int offset, int length, char delimeter)
        {
            for (var i = offset + length - 1; i >= offset; i--)
            {
                if (buffer[i] == delimeter)
                    return i;
            }
            return -1;
        }
    }
}