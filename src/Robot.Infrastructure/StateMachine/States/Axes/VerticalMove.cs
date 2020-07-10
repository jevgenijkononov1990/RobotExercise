using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.StateMachine.States.Axes
{
    public class VerticalMove : IMove
    {
        private readonly IRobotLocationService _locationService;

        public VerticalMove(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IMove.Move(RobotCommand robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
