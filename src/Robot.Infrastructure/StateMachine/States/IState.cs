using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.States
{
    public interface IState
    {
        (bool isSuccess, StateResponse stateResponse) ProcessState(RobotCommand robotCommand, MatrixSize matrixSize = null);
    }
}
