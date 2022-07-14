using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
//using IQFeed.CSharpApiClient.Examples.Common;ConcurrentFileHistoricalExample
using IQFeed.CSharpApiClient;
using IQFeed.CSharpApiClient.Lookup;
using IQFeed.CSharpApiClient.Lookup.Historical.Messages;

namespace IQFeed.CSharpApiClient.Demo
{
    public sealed class Program
    {
        #region Examples
        [DisplayName("BasicHistoricalExample")]
        private static void BasicHistoricalExample()
        {
            Demos.BasicHistoricalLookUp();
            Console.ReadLine();
        }

        [DisplayName("ConcurrentFileHistorical")]
        private static void ConcurrentFileHistorical()
        {
            Demos.ConcurrentFileHistorical();
            Console.ReadLine();
        }

        [DisplayName("StreamingLevel1 - 20 sec. of updates")]
        private static void StreamingLevel1()
        {
            _ = Demos.StreamingLevel1();
            Console.ReadLine();
        }
 
        [DisplayName("StreamingLevel2 - MBP (Market By Price) - 20 sec. of updates")]
        private static void StreamingLevel2()
        {
            _ = Demos.StreamingLevel2_MBP();
            Console.ReadLine();
        }

        [DisplayName("StreamingLevel2 - MBO (Market By Order) - 20 sec. of updates")] 
        private static void MBODataExample()
        {
            _ = Demos.StreamingLevel2_MBO();
            Console.ReadLine();
        }
        #endregion

        private struct Function
        {
            public readonly MethodInfo Method;
            public readonly string Name;

            public Function(MethodInfo method, string name)
            {
                this.Method = method;
                this.Name = name;
            }
        }

        private static Function[] GetFunctions()
        {
            var functions = new List<Function>();
            foreach (var method in typeof(Program).GetMethods(BindingFlags.Static | BindingFlags.NonPublic))
            {
                var nameAttr = method.GetCustomAttribute<DisplayNameAttribute>();
                if (nameAttr == null)
                {
                    continue;
                }

                functions.Add(new Function(method, nameAttr.DisplayName));
            }
            return functions.ToArray();
        }

        public static void Main(string[] args)
        {
            // Maximize the console window for cleanest results *****************************
            // (.NET 6 Console application)
            [DllImport("kernel32.dll", ExactSpelling = true)]
            static extern IntPtr GetConsoleWindow();
            IntPtr ThisConsole = GetConsoleWindow();

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
            const int MAXIMIZE = 3;
            ShowWindow(ThisConsole, MAXIMIZE);
            //*******************************************************************************

            var demos = GetFunctions();
            while (true)
            {
                Console.WriteLine("This Demo targets the .NET 6 runtime and uses the DTN IQFeed 6.2 protocol for all data.");
                Console.WriteLine("*** Configure your credentials for IQConnect in user Environment Variables or in App.config ***");
                Console.WriteLine("For streaming data, make sure your market is open.  Level 2 requires a separate subscription.");
                Console.WriteLine("See the API Documentation for more information.\n");

                Console.WriteLine("Pick an action:\n");
                for (int demo = 0; demo < demos.Length; demo++)
                {
                    Console.WriteLine($"{demo,4} - {demos[demo].Name}");
                }

                Console.WriteLine("Exit - Exit from Demo (or use 'e' or 'x')\n");
                var response = Console.ReadLine().Trim();
                if (string.Equals(response, "exit", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(response, "e", StringComparison.OrdinalIgnoreCase) || 
                    string.Equals(response, "x", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                int demoToRun;
                if (!int.TryParse(response, NumberStyles.Integer, CultureInfo.InvariantCulture, out demoToRun))
                {
                    demoToRun = -1;
                }

                if (demoToRun >= 0 && demoToRun < demos.Length)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{demos[demoToRun].Name}\n");
                    demos[demoToRun].Method.Invoke(null, null);
                }
                else
                {
                    Console.WriteLine("Unknown Query Demo");
                }
            }
        }
    }
}
