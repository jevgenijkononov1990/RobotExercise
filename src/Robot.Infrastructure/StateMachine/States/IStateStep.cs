using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.States
{
    public interface IStateStep
    {
        (bool isSuccess, RobotPosition currentPosition) MakeStep(RobotCommandView robotCommandView, MatrixSize matrixSize = null);
    }
}
