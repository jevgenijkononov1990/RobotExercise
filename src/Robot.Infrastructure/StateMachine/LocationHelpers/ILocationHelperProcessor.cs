using Robot.Common.Enms;
using Robot.Common.Models;


namespace Robot.Infrastructure.StateMachine.LocationHelpers
{
    public interface ILocationHelperProcessor
    {
        Direction ProcessNewDirection(RotationType rotationType, Direction currentDirection);
        Position ProcessNewPosition(Direction currentDirection, int x, int y, int distance);
    }
}
