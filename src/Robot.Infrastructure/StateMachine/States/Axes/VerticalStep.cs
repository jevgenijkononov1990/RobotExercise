using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.StateMachine.States.Axes
{
    public class VerticalStep : IStateStep
    {
        private readonly IRobotLocationService _locationService;

        public VerticalStep(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        (bool isSuccess, RobotPosition currentPosition) IStateStep.MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize)
        {
            throw new NotImplementedException();
        }
    }
}
