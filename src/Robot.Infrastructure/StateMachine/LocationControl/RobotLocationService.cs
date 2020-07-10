
using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Robot.Infrastructure.StateMachine
{
    public class RobotLocationService : IRobotLocationService
    {
        //Ideally we need Matrix or Plateo RepoService to hold that, But I will skip currently this step and will hold everything inside service as service is singelton
        //Also location processor should be a service

        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private MatrixSize _workingMatrixSize { get; set; }
        private RobotPosition _currentPosition { get; set; }

        public RobotLocationService()
        {
            _workingMatrixSize = new MatrixSize();
        }

        public bool SetupWorkingMatrix(MatrixSize matrixSize)
        {
            if (matrixSize == null)
            {
                _log.Message(LogLevel.Warn, $"Matrix size setup abort due to {typeof(MatrixSize).Name} value null");
                return false;
            }

            _workingMatrixSize = matrixSize;

            return true;
        }

        public bool SetPosition(int x, int y, Direction direction)
        {

            if (!Enum.IsDefined(typeof(Direction), direction))
            {
                throw new ArgumentOutOfRangeException();
            }

            if (GeneralValidationHelper.IsIntegerValueNegative(x) || GeneralValidationHelper.IsIntegerValueNegative(y))
            {
                return false;
            }

            switch (direction)
            {
                case Direction.N: // y++
                    return PocessNorthDirection(x, y);
                case Direction.S: // y--
                    return PocessSouthDirection(x, y);
                case Direction.E: //x++
                    return PocessEestDirection(x, y);
                case Direction.W: //x--
                    return PocessWestDirection(x, y);
                default:
                    throw new NotImplementedException();
            }
        }
    
        public RobotPosition GetCurrentRobotPosition()
        {
            return _currentPosition;
        }

        public MatrixSize GetMatrixSize()
        {
            return _workingMatrixSize;
        }

        #region LocationProcessors

        private bool PocessNorthDirection(int x, int y)
        {
            return true;
        }

        private bool PocessEestDirection(int x, int y)
        {
            return true;
        }

        private bool PocessSouthDirection(int x, int y)
        {
            return true;
        }

        private bool PocessWestDirection(int x, int y)
        {
            return true;
        }

        #endregion
    }
}
