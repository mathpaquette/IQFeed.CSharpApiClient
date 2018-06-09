namespace IQFeed.CSharpApiClient.Common
{
    public enum LoggingLevel
    {
        Off = 0,
        Admin = 2,
        L1Data = 4,
        L1Request = 8,
        L1System = 16,
        L1Error = 32,
        L2Data = 64,
        L2Request = 128,
        L2System = 256,
        L2Error = 512,
        LookupData = 1024,
        LookupRequest = 2048,
        LookupError = 4096,
        Information = 8192,
        Debug = 16384,
        Connectivity = 32768
    }
}