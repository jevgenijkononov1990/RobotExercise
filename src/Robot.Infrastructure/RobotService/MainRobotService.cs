using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Common.Models;
using Robot.Infrastructure.Communication;
using Robot.Infrastructure.Settings;
using System;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robot.Infrastructure.RobotService
{
    public class MainRobotService : IMianRobotService
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICommunicationHandler _communicationHandler;
        private readonly IRobotRepoSettings _robotSettings;
        private RobotConfig _robotConfig;


        public MainRobotService(ICommunicationHandler communicationHandler, IRobotRepoSettings robotSettings)
        {
            _communicationHandler = communicationHandler ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(communicationHandler)}");
            _robotSettings = robotSettings ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(robotSettings)}");
        }

        public bool InitializeRobotFramework()
        {
            try
            {
                _robotConfig = _robotSettings.GetEnviromentSettings();
                if (_robotConfig == null
                    || string.IsNullOrWhiteSpace(_robotConfig.Name)
                    || _robotConfig.StartPosition == null
                    || _robotConfig.StartPosition?.X < 0
                    || _robotConfig.StartPosition?.Y < 0
                    || !Enum.IsDefined(typeof(Direction), _robotConfig.StartDirection))
                {
                    _log.Message(LogLevel.Warn, "Default settings will be applied, Config Settings read error");

                    _robotConfig = RobotConstantsValues.RobotDefaultCongifs;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(RobotConstantsValues.CriticalErrorOccuredMissionWillContinue);
                _log.Exception(ex);
            }

            //update state machine

            return true;
        }

        public async Task StartRobotFrameworkThreadAsync(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource == null)
            {
                _log.Message(LogLevel.Error, "cancellationTokenSource is null");
                throw new ArgumentNullException("");
            }

            while (!cancellationTokenSource.IsCancellationRequested)
            {

                Console.WriteLine($"Robot Rover is ready: {DateTime.Now}");
                var input = Console.ReadLine();
            }
        }
    }
}
