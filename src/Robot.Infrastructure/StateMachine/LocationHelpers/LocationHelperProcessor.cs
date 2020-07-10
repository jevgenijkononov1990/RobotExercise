using Robot.Common;
using Robot.Common.Enms;
using Robot.Common.Models;
using System;


namespace Robot.Infrastructure.StateMachine.LocationHelpers
{
    public class LocationHelperProcessor : ILocationHelperProcessor
    {

        public Direction ProcessNewDirection(RotationType rotationType, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.N:
                    return rotationType == RotationType.Left ? Direction.W : Direction.E;
                case Direction.E:
                    return rotationType == RotationType.Left ? Direction.N : Direction.S;
                case Direction.S:
                    return rotationType == RotationType.Left ? Direction.E : Direction.W;
                case Direction.W:
                    return rotationType == RotationType.Left ? Direction.S : Direction.N;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Position ProcessNewPosition(Direction currentDirection, int x, int y, int distance)
        {
            switch (currentDirection)
            {
                case Direction.N:
                    return new Position(x, y + distance);
                case Direction.E:
                    return new Position(x + distance, y);
                case Direction.S:
                    return new Position(x, y - distance);
                case Direction.W:
                    return new Position(x - distance, y);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
