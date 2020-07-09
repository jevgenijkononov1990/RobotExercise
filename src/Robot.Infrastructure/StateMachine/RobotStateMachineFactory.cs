using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Infrastructure.StateMachine.LocationControl;
using Robot.Infrastructure.StateMachine.States;
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
            _locationService = locationService;
        }
        public IStateStep Build(RobotCommand stepCommand)
        {
            if(!Enum.IsDefined(typeof(RobotCommand), stepCommand))
            {
                throw new NotImplementedException();
            }

            switch (stepCommand)
            {
                case RobotCommand.Initialization:
                    break;
                case RobotCommand.MoveRight:
                    break;
                case RobotCommand.MoveLeft:
                    break;
                case RobotCommand.MoveStraight:
                    break;
                case RobotCommand.MoveBack:
                    break;
                case RobotCommand.RotateLeft:
                    break;
                case RobotCommand.RotateRight:
                    break;
                case RobotCommand.Vertically:
                    break;
                case RobotCommand.Horizontally:
                    break;
            }


        }
    }
}
