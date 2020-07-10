using Microsoft.Extensions.DependencyInjection;
using Robot.Common;
using Robot.Common.Logging;
using Robot.Infrastructure.RobotService;
using System;
using System.Reflection;
using System.Threading;

namespace Robot.ConsoleApp
{
    class Program
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private static ServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            try
            {
                InitProgramSettings();          
                Console.CancelKeyPress += new ConsoleCancelEventHandler(Console_CancelKeyPress);
                InitRobotFramework();
                Console.CancelKeyPress -= new ConsoleCancelEventHandler(Console_CancelKeyPress);

                Console.WriteLine(RobotConstantsValues.RobotProgramLastMessage);
                Thread.Sleep(5000);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unhandled exception occured.. program is about to terminate");
                _log.Exception(ex);
                Console.ReadKey();
            }
        }

        #region ProgramInit

        static void InitProgramSettings()
        {
            LogManager.Configure("log4net.config");
            _serviceProvider = StartUp.BuildServiceProvider();
            _log.Message(LogLevel.Info, "Robot is about to  start.......");
        }

        static void InitRobotFramework()
        {
            var robot = _serviceProvider.GetService<IMainRobotService>();

            if (!robot.OsInitialization())
            {
                Console.WriteLine("Robot Operating System initialization failure");
                _log.Message(LogLevel.Fatal, "Critical error in os");
                return;
            }
            robot.StartCommunicationThreadAsync(_cancellationTokenSource);
        }

        #endregion

        #region CTRL + C exit handler
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
        #endregion
    }
}
