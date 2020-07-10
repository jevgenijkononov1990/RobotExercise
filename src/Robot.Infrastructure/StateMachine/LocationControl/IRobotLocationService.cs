using Robot.Common.Enms;
using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.LocationControl
{
    public interface IRobotLocationService
    {
        bool SetupWorkingMatrix(MatrixSize matrixSize);
        bool SetPosition(int x, int y, Direction direction);
        RobotPosition GetCurrentRobotPosition();
        MatrixSize GetMatrixSize();
    }
}
