using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQFeed.CSharpApiClient.Streaming.Level2
{
    public enum Level2Side
    {
        Sell,
        Buy
    }

    public static class Level2SideParser
    {
        public static Level2Side Parse(string text)
        {
            if (text == "S")
            {
                return Level2Side.Sell;
            } else if(text == "B")
            {
                return Level2Side.Buy;
            } else
            {
                return (Level2Side)Enum.Parse(typeof(Level2Side), text, true);
            }
        }

        public static bool TryParse(string text, out Level2Side result)
        {
            if (text == "S")
            {
                result =  Level2Side.Sell;
                return true;
            }

            if (text == "B")
            {
                result = Level2Side.Buy;
                return true;
            }

            return Enum.TryParse<Level2Side>(text, out result);
        }
    }
}
