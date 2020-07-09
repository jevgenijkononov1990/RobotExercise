using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.LocationControl
{
    public interface IRobotLocationService
    {
        bool SetupLocationFirstTime(MatrixSize matrixSize, RobotPosition currentPosition);
        bool ChangeLocationTo(int x, int y);
        MatrixLocation CheckLocation();
    }
}
