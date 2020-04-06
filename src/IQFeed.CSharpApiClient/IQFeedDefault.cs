namespace IQFeed.CSharpApiClient
{
    // ReSharper disable once InconsistentNaming
    public class IQFeedDefault
    {
        public const string Hostname = "localhost";

        public const string ProtocolVersion = "6.1";

        public const string ProtocolTerminatingCharacters = "\r\n";
        public const string ProtocolEndOfMessageCharacters = "!ENDMSG!";
        public const string ProtocolNoDataCharacters = "!NO_DATA!";
        public const string ProtocolSyntaxErrorCharacters = "!SYNTAX_ERROR!";
        
        public const char ProtocolLineFeedCharacter ='\n';
        public const char ProtocolDelimiterCharacter = ',';

        public const char PrototolErrorCharacter = 'E';

        public const int Level1Port = 5009;
        public const int LookupPort = 9100;
        public const int Level2Port = 9200;
        public const int AdminPort = 9300;
        public const int DerivativePort = 9400;
        public const int LoginPort = 60020;
    }
}