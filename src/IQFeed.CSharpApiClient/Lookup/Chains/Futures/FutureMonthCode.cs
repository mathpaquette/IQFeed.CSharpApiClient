using System;

namespace IQFeed.CSharpApiClient.Lookup.Chains.Futures
{
    public static class FutureMonthCode
    {
        public static int Decode(string monthCode)
        {
            switch (monthCode)
            {
                case "F":
                    return 1;
                case "G":
                    return 2;
                case "H":
                    return 3;
                case "J":
                    return 4;
                case "K":
                    return 5;
                case "M":
                    return 6;
                case "N":
                    return 7;
                case "Q":
                    return 8;
                case "U":
                    return 9;
                case "V":
                    return 10;
                case "X":
                    return 11;
                case "Z":
                    return 12;
                default:
                    throw new NotSupportedException("MonthCode not supported");
            }
        }
    }
}