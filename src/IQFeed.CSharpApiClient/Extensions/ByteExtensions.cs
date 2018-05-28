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

        public static bool EndsWith(this byte[] self, int count, byte[] pattern)
        {
            var bufferIdx = count - 1;

            if (self.Length < pattern.Length)
                return false;

            if (count < pattern.Length)
                return false;

            for (var patternIndex = pattern.Length - 1; patternIndex >= 0; patternIndex--)
            {
                if (pattern[patternIndex] != self[bufferIdx])
                    return false;

                bufferIdx--;
            }

            return true;
        }
    }
}