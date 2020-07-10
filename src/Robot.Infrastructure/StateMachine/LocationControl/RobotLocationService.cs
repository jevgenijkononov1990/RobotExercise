using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Common.Models;
using Robot.Infrastructure.StateMachine.LocationControl;
using System;
using System.Linq;
using System.Reflection;

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
            _currentPosition = new RobotPosition { X = 0 , Y = 0 , Direction = Direction.N };
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
            _log.Starting();

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
                    return PocessNorthDirection(x, y, direction);
                case Direction.S: // y--
                    return PocessSouthDirection(x, y, direction);
                case Direction.E: //x++
                    return PocessEestDirection(x, y, direction);
                case Direction.W: //x--
                    return PocessWestDirection(x, y, direction);
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

        private bool PocessNorthDirection(int x, int y, Direction direction)  // y++
        {
            var temp = _currentPosition.Y + y;

            if (!GeneralValidationHelper.IsWithin(temp, _workingMatrixSize.Min_Y_Value, _workingMatrixSize.Max_Y_Value))
            {
                return false;
            }

            _currentPosition.Y = temp;
            _currentPosition.Direction = direction;

            return true;
        }

        private bool PocessEestDirection(int x, int y, Direction direction)  //x++
        {
            var temp = _currentPosition.X + x;

            if (!GeneralValidationHelper.IsWithin(temp, _workingMatrixSize.Min_X_Value, _workingMatrixSize.Max_X_Value))
            {
                return false;
            }

            _currentPosition.X = temp;
            _currentPosition.Direction = direction;

            return true;
        }

        private bool PocessSouthDirection(int x, int y, Direction direction)// y--
        {
            var temp = _currentPosition.Y - y;

            if (!GeneralValidationHelper.IsWithin(temp, _workingMatrixSize.Min_Y_Value, _workingMatrixSize.Max_Y_Value))
            {
                return false;
            }

            _currentPosition.Y = temp;
            _currentPosition.Direction = direction;
            return true;
        }

        private bool PocessWestDirection(int x, int y, Direction direction) //x--
        {

            var temp = _currentPosition.X -  x;

            if (!GeneralValidationHelper.IsWithin(temp, _workingMatrixSize.Min_X_Value, _workingMatrixSize.Max_X_Value))
            {
                return false;
            }

            _currentPosition.X = temp;
            _currentPosition.Direction = direction;
            return true;
        }

        #endregion
    }
}
