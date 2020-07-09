using Robot.Common.Enms;
using Robot.Infrastructure.StateMachine.States;

namespace Robot.Infrastructure.StateMachine
{
    public interface IRobotStateMachineFactory
    {
        IStateStep Build(RobotCommand stepCommand);
    }
}
