﻿using Robot.Common;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.StateMachine.States
{
    public class InitializationStep : IStateStep
    {
        private readonly IRobotLocationService _locationService;

        public InitializationStep(IRobotLocationService locationService)
        {
            _locationService = locationService ?? throw new ArgumentNullException($"{GetType().Name} {RobotConstantsValues.ConstructorInitFailure} {nameof(locationService)}");

        }

        public (bool isSuccess, RobotPosition currentPosition) MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize = null)
        {
            throw new NotImplementedException();
        }
    }
}
