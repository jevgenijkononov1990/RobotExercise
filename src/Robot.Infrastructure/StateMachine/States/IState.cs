using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.States
{
    public interface IState
    {
        (bool isSuccess, RobotResponse robotResponse) ProcessState(RobotCommand robotCommand, MatrixSize matrixSize = null);
    }
}
