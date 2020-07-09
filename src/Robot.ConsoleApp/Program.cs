using Microsoft.Extensions.DependencyInjection;
using Robot.Common.Logging;
using Robot.Infrastructure.Communication;
using Robot.Infrastructure.RobotService;
using Robot.Infrastructure.Settings;
using System;
using System.Reflection;
using System.Threading;

namespace Robot.ConsoleApp
{
    class Program
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            try
            {
                LogManager.Configure("log4net.config"); 
                ServiceProvider serviceProvider = StartUp.BuildServiceProvider();
                _log.Message(LogLevel.Info, "Robot Robert started.......");
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);

                serviceProvider.GetService<IMianRobotService>().
                    .HandleInputAsync(_cancellationTokenSource);

                Console.CancelKeyPress -= new ConsoleCancelEventHandler(Console_CancelKeyPress);

            }
            catch(Exception ex)
            {
                Console.WriteLine("Unhandled exception occured.. program is about to terminate");
                _log.Exception(ex);
                Console.ReadKey();
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.WriteLine("Cancelling");
            if (e.SpecialKey == ConsoleSpecialKey.ControlC)
            {
                _log.Message(LogLevel.Info, "Program termination");
                _cancellationTokenSource.Cancel();
                e.Cancel = true;
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
        }

    }
}
