using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;


namespace Robot.Infrastructure.StateMachine.States.Rotation
{
    public class LeftRotationStep : IStateStep
    {

        private readonly IRobotLocationService _locationService;

        public LeftRotationStep(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IStateStep.MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
