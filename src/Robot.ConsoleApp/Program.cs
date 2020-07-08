using Microsoft.Extensions.DependencyInjection;
using Robot.Common.Logging;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Robot.ConsoleApp
{
    class Program
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static CancellationTokenSource TokenSource;
        private static CancellationToken Token;
        static void Main(string[] args)
        {
            TokenSource = new CancellationTokenSource();
            Token = TokenSource.Token;

            try
            {
                LogManager.Configure("log4net.config"); 

                ServiceProvider serviceProvider = StartUp.BuildServiceProvider();

                _log.Message(LogLevel.Warn, "Application started.......");

                var exitEvent = new ManualResetEvent(false);
               
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                RunAsync();
                Console.CancelKeyPress -= new ConsoleCancelEventHandler(Console_CancelKeyPress);

            }
            catch
            {
                Console.WriteLine("Unhandled exception occured");
                Console.ReadKey();
            }
        }

        private static async Task RunAsync()
        {
            while (!TokenSource.IsCancellationRequested)
            {

               Console.WriteLine($"Time: {DateTime.Now}");
               var input = Console.ReadLine();
            }
        }
        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                TokenSource.Cancel();
                e.Cancel = true;
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }

    }
}
