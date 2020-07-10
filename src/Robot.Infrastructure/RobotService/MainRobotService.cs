using Newtonsoft.Json;
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
        private RobotPosition _currentPosition { get; set; }


        public MainRobotService(ICommunicationHandler communicationHandler, IRobotRepoSettings robotSettings, IRobotStateMachineFactory robotStateMachine)
        {
            _communicationHandler = communicationHandler 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(communicationHandler)}");

            _robotSettings = robotSettings 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(robotSettings)}");

            _robotStateMachine = robotStateMachine 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(robotStateMachine)}");
        }

        public bool OsInitialization()
        {
            try
            {
                _robotConfig = _robotSettings.GetEnviromentSettings();
                if (_robotConfig == null || _robotConfig.MatrixSize == null || _robotConfig.Position == null)
                {
                    _log.Message(LogLevel.Warn, "Default settings will be applied, Config Settings read error");

                    _robotConfig = RobotConstantsValues.RobotDefaultCongifs;
                }

                Console.WriteLine($"Robot {_robotConfig.Name} settings read finish!");
                Console.WriteLine($"Start position :---> [{ _robotConfig.Position.X}:{ _robotConfig.Position.Y}:{ _robotConfig.Position.Direction}]");
                Console.WriteLine($"Matrix settings :---> [{ _robotConfig.MatrixSize.Max_X_Value}:{ _robotConfig.MatrixSize.Max_Y_Value}]");

                RobotCommand command = new RobotCommand
                {
                    QueueId = 0, 
                    Type = RobotCommandType.Initialization,
                    CurentDirection = _robotConfig.Position.Direction,
                    MoveTo =  new Position
                    {
                        X = _robotConfig.Position.X,
                        Y = _robotConfig.Position.Y,
                    } 
                };

                var cmdResult = _robotStateMachine.Build(RobotCommandType.Initialization).ProcessState(command, _robotConfig.MatrixSize);

                if (cmdResult.isSuccess && cmdResult.stateResponse != null && cmdResult.stateResponse.CurrentPosition!=null)
                {
                    _currentPosition = cmdResult.stateResponse.CurrentPosition;
                }

                _log.Message(LogLevel.Info, cmdResult.isSuccess ? $"{RobotCommandType.Initialization} Success" : $"{RobotCommandType.Initialization} Failure");

                return cmdResult.isSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine(RobotConstantsValues.CriticalErrorOccuredMissionWillNotContinue);
                _log.Exception(ex);
                return false;
            }
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
                    var response = _communicationHandler.ConvertInputToCommandList(consoleLineInput, _currentPosition);

                    if (response.success)
                    {
                        if( response.robotCommands != null && response.robotCommands.Count > 0)
                        {
                            Console.WriteLine($"Robot tries to handle command");
                            HandleRobotCommands(response.robotCommands);
                        }
                    }
                    else
                    {
                        _log.Message(LogLevel.Warn, "Input decode failure.");
                    }
                }
                catch (Exception ex)
                {
                    _log.Exception(ex);
                    Console.WriteLine(RobotConstantsValues.CommunicationError);
                }
            }
        }

        private void HandleRobotCommands(List<RobotCommand> cmdList)
        {
            int counter = 0;
            foreach(var cmd in cmdList)
            {
                cmd.QueueId = counter;
                try
                {
                    Console.WriteLine($"Request command:{cmd.Type}: Direction:{cmd.CurentDirection} request new Position({cmd.MoveTo.X}:{cmd.MoveTo.Y})");
                    var result = _robotStateMachine.Build(cmd.Type).ProcessState(cmd);

                    if (result.isSuccess && result.stateResponse != null)
                    {
                        _currentPosition = result.stateResponse.CurrentPosition;
                        Console.WriteLine($"Success! Position was changed to: [{result.stateResponse.CurrentPosition.X}:{result.stateResponse?.CurrentPosition.Y}:{result.stateResponse?.CurrentPosition.Direction}]");
                    }
                    else
                    {
                        Console.WriteLine($"Position was not changed");
                    }
                }
                catch (Exception ex)
                {
                    _log.Exception(ex, $"Robot command critical failure");
                    Console.WriteLine($"{RobotConstantsValues.CommandFailure}");
                }

                counter++;
            }
        }
    }
}
