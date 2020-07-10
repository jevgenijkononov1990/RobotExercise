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



        public IState Build(RobotCommandType commandType)
        {
            if(!Enum.IsDefined(typeof(RobotCommandType), commandType))
            {
                throw new NotImplementedException();
            }

            switch (commandType)
            {
                case RobotCommandType.Initialization:
                    return new InitializationState(_locationService);
                case RobotCommandType.Move:
                    return new MoveState(_locationService);

            }

            throw new NotImplementedException();
        }
    }
}

