using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Infrastructure.StateMachine
{
    public class RobotLocationService : IRobotLocationService
    {
        public bool ChangeLocationTo(int x, int y)
        {
            throw new NotImplementedException();
        }

        public MatrixLocation CheckLocation()
        {
            throw new NotImplementedException();
        }

        public bool SetupLocationFirstTime(MatrixSize matrixSize, RobotPosition currentPosition)
        {
            throw new NotImplementedException();
        }
    }
}
