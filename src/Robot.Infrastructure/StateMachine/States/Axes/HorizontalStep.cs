using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;

namespace Robot.Infrastructure.StateMachine.States.Axes
{
    public class HorizontalStep : IStateStep
    {
        private readonly IRobotLocationService _locationService;

        public HorizontalStep(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IStateStep.MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
