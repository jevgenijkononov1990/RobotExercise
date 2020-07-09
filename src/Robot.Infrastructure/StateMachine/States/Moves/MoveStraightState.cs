using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.StateMachine.States.Moves
{
    public class MoveStraightState : IStateStep
    {

        private readonly IRobotLocationService _locationService;

        public MoveStraightState(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IStateStep.MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
