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
                if (_robotConfig == null || _robotConfig.MatrixSize == null || _robotConfig.Position == null)
                {
                    _log.Message(LogLevel.Warn, "Default settings will be applied, Config Settings read error");

                    _robotConfig = RobotConstantsValues.RobotDefaultCongifs;
                }

                Console.WriteLine($"Robot {_robotConfig.Name} init success!");
                Console.WriteLine($"Start position :---> [{ _robotConfig.Position.X}:{ _robotConfig.Position.Y}:{ _robotConfig.Position.Direction}]");
                Console.WriteLine($"Matrix settings :---> [{ _robotConfig.MatrixSize.Max_X_Value}:{ _robotConfig.MatrixSize.Max_Y_Value}]");

                RobotCommandView robotView = new RobotCommandView
                {
                    OriginalText = "Init",
                    QueueId = 0, 
                    Command = RobotCommand.Initialization,
                    CurentDirection = _robotConfig.Position.Direction,
                    MoveTo =  new MatrixLocation
                    {
                        X = _robotConfig.Position.X,
                        Y = _robotConfig.Position.Y,
                    } 

                };
                var cmdResult = _robotStateMachine.Build(robotView.Command).MakeStep(robotView, new MatrixSize { });
        
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
                            ProceedRobotCommands(response.robotCommands);
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
            int counter = 0;
            foreach(var item in cmdList)
            {
                item.QueueId = counter;
                try
                {
                    var result = _robotStateMachine.Build(item.Command).MakeStep(item);

                    if (result.isSuccess && result.currentPosition !=null)
                    {
                        Console.WriteLine($"Success! Position was changed to: [{result.currentPosition.X}:{result.currentPosition.X}:{result.currentPosition.Direction}]");
                    }
                    else
                    {
                        Console.WriteLine($"Position was not changed");
                    }
                }
                catch (Exception ex)
                {
                    _log.Exception(ex, $"Robot command critical failure: {JsonConvert.SerializeObject(item)}");
                    Console.WriteLine($"{RobotConstantsValues.CommandFailure}: {JsonConvert.SerializeObject(item)}");
                }

                counter++;
            }
        }
    }
}
