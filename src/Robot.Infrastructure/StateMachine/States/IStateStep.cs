using Robot.Common.Models;

namespace Robot.Infrastructure.StateMachine.States
{
    public interface IStateStep
    {
        bool MakeStep(RobotCommandView robotCommandView);
    }
}
