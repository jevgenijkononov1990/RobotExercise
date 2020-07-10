using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;

namespace Robot.Infrastructure.StateMachine.States
{
    public class InitializationState : IState
    {
        private readonly IRobotLocationService _locationService;

        public InitializationState(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");
        }

        (bool isSuccess, RobotResponse robotResponse) IState.ProcessState(RobotCommand robotCommand, MatrixSize matrixSize)
        {
   
            if (robotCommand == null || matrixSize == null || robotCommand.MoveTo == null)
            {
                return (false, null);
            }

            if (_locationService.SetupWorkingMatrix(matrixSize) == false)
            {
                return (false, new RobotResponse
                {
                    CurrentPosition = _locationService.GetCurrentRobotPosition()
                });
            }

            var result = _locationService.SetPosition(robotCommand.MoveTo.X, robotCommand.MoveTo.Y, robotCommand.CurentDirection);

            return (result, new RobotResponse
            {
                CurrentPosition = _locationService.GetCurrentRobotPosition()
            });

        }
    }
}
