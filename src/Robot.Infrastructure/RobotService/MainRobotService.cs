using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Common.Models;
using Robot.Infrastructure.Communication;
using Robot.Infrastructure.Settings;
using Robot.Infrastructure.StateMachine;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robot.Infrastructure.RobotService
{
    public class MainRobotService : IMainRobotService
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ICommunicationHandler _communicationHandler;
        private readonly IRobotRepoSettings _robotSettings;
        private readonly IRobotStateMachineFactory  _robotStateMachine;
        private RobotConfig _robotConfig;


        public MainRobotService(ICommunicationHandler communicationHandler, IRobotRepoSettings robotSettings, IRobotStateMachineFactory robotStateMachine)
        {
            _communicationHandler = communicationHandler 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(communicationHandler)}");

            _robotSettings = robotSettings 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(robotSettings)}");

            _robotStateMachine = robotStateMachine 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(robotStateMachine)}");
        }

        public bool InitializeFramework()
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

                Console.WriteLine($"Robot {_robotConfig.Name} init success! Start settings :---> [{_robotConfig.StartPosition.X}:{_robotConfig.StartPosition.Y}:{_robotConfig.StartDirection}]");
            }
            catch (Exception ex)
            {
                Console.WriteLine(RobotConstantsValues.CriticalErrorOccuredMissionWillContinue);
                _log.Exception(ex);
            }

            //update state machine

            return true;
        }

        public async Task StartCommunicationThreadAsync(CancellationTokenSource cancellationTokenSource)
        {
            if (cancellationTokenSource == null)
            {
                _log.Message(LogLevel.Error, "cancellationTokenSource is null");
                throw new ArgumentNullException("");
            }

            while (!cancellationTokenSource.IsCancellationRequested)
            {
                Console.WriteLine($"Robot {_robotConfig.Name} is ready: {DateTime.Now} for the next input");
                var consoleLineInput = Console.ReadLine();
                try
                {
                    var response = _communicationHandler.ConvertInputToCommand(consoleLineInput);

                    if (response.success)
                    {
                        if( response.robotCommands != null && response.robotCommands.Count > 0)
                        {
                            //call state machine
                        }
                    }
                    else
                    {
                        _log.Message(LogLevel.Warn, "Input command failure. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    _log.Exception(ex);
                    Console.WriteLine(RobotConstantsValues.CommunicationError);
                }
            }
        }

        private void ProceedRobotCommands(List<RobotCommandView> cmdList)
        {
            foreach(var item in cmdList)
            {
                _robotStateMachine.Build(item.Cmd).MakeStep(item);
            }
        }
    }
}
