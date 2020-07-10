using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Infrastructure.StateMachine.LocationControl;
using Robot.Infrastructure.StateMachine.States;
using Robot.Infrastructure.StateMachine.States.Moves;
using System;
using System.Reflection;

namespace Robot.Infrastructure.StateMachine
{
    public class RobotStateMachineFactory : IRobotStateMachineFactory
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly IRobotLocationService _locationService; 

        public RobotStateMachineFactory(IRobotLocationService locationService)
        {
            _locationService = locationService 
                ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");
        }


        //I know, It is possible to avoid this step and directly in mainRobotService call locationService.setPosition
        //However, I have decided to do so as each step may have different configurations and different actions.
        //and let's say in future it may be required to implement new functionality or create a new step...move or any other action
        //which will be necessary for the robot MARS mission future.
        //So, following SOLID principles and particularly Single responsible principle and Open/Closed principle I have implemented 
        //factory pattern for each commandType
        //But I fully understand that it will be much easier to do so from the main service and do not bother with robotStateMachine
        //and from my practise it is better to design correctly your solution rather then re-think and did massive logic change when everything is fully implemented

        public IMove Build(RobotCommandType commandType)
        {
            if(!Enum.IsDefined(typeof(RobotCommandType), commandType))
            {
                throw new NotImplementedException();
            }

            switch (commandType)
            {
                case RobotCommandType.Initialization:
                    return new InitializationMove(_locationService);
                case RobotCommandType.MoveRight:
                    return new RightMove(_locationService);
                case RobotCommandType.MoveLeft:
                    break;
                case RobotCommandType.MoveStraight:
                    break;
                case RobotCommandType.MoveBack:
                    break;
                case RobotCommandType.RotateLeft:
                    break;
                case RobotCommandType.RotateRight:
                    break;
                case RobotCommandType.Vertically:
                    break;
                case RobotCommandType.Horizontally:
                    break;
            }

            throw new NotImplementedException();
        }
    }
}

