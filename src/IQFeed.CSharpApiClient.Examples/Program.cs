using System;
using System.Linq;
using System.Threading.Tasks;
using IQFeed.CSharpApiClient.Examples.Common;

namespace IQFeed.CSharpApiClient.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            RunExamples().Wait();
            ConsoleHelper.PressEnterToContinue();
        }

        /// <summary>
        /// Running the examples one by one. You don't need to look here.
        /// Check the Example files only.
        /// </summary>
        /// <returns></returns>
        static async Task RunExamples()
        {
            var examples = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IExample).IsAssignableFrom(p) && !p.IsInterface).OrderBy(x => x.Name).ToList();

            var enabledExamples = examples.Select(x => (IExample)Activator.CreateInstance(x)).Where(example => example.Enable).ToList();

            if (enabledExamples.Count == 0)
            {
                ConsoleHelper.ShowEnableWarning();

                foreach (var example in examples)
                    Console.WriteLine($"{example.Name}.cs");
            }

            foreach (var example in enabledExamples)
            {
                ConsoleHelper.ShowStarted(example.Name);
                if (example is IExampleAsync exampleAsync)
                {
                    await exampleAsync.RunAsync();
                }
                else
                {
                    example.Run();
                }
                ConsoleHelper.ShowFinished(example.Name);
            }

            if (enabledExamples.Count > 0)
                ConsoleHelper.ShowExamplesCompleted();
        }
    }
}
