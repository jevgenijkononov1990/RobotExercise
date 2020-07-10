using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;

namespace Robot.Infrastructure.StateMachine.States
{
    public class InitializationMove : IMove
    {
        private readonly IRobotLocationService _locationService;

        public InitializationMove(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");
        }

        public (bool isSuccess, RobotPosition currentPosition) Move(RobotCommand robotCommand, MatrixSize matrixSize)
        {
            if(robotCommand == null || matrixSize == null || robotCommand.MoveTo == null)
            {
                return (false, _locationService.GetCurrentRobotPosition());
            }

            bool result = _locationService.SetupWorkingMatrix(matrixSize);

            if (result == false)
            {
                return (result, _locationService.GetCurrentRobotPosition());
            }

            result = _locationService.SetPosition(robotCommand.MoveTo.X, robotCommand.MoveTo.Y, robotCommand.CurentDirection);

            return (result, _locationService.GetCurrentRobotPosition());
        }
    }
}
