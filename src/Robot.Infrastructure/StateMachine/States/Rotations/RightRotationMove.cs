using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;

namespace Robot.Infrastructure.StateMachine.States.Rotation
{
    public class RightRotationMove : IMove
    {

        private readonly IRobotLocationService _locationService;

        public RightRotationMove(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IMove.Move(RobotCommand robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
