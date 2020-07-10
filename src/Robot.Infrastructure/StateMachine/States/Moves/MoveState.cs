using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;

namespace Robot.Infrastructure.StateMachine.States.Moves
{
    public class MoveState : IState
    {
        private readonly IRobotLocationService _locationService;

        public MoveState(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");
        }

        public (bool isSuccess, StateResponse stateResponse) ProcessState(RobotCommand robotCommand, MatrixSize matrixSize = null)
        {
            if (robotCommand == null || robotCommand.MoveTo == null)
            {
                return (false, null);
            }

            bool result = _locationService.SetPosition(robotCommand.MoveTo.X, robotCommand.MoveTo.Y, robotCommand.CurentDirection);

            return (result, new StateResponse { CurrentPosition = _locationService.GetCurrentRobotPosition() });
        }
    }
}
