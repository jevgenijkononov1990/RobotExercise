using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.States
{
    public interface IMove
    {
        (bool isSuccess, RobotPosition currentPosition) Move(RobotCommand robotCommand, MatrixSize matrixSize = null);
    }
}
