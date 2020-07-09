using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Common.Models
{
    public class RobotStartPosition : MatrixCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public interface MatrixCoordinate
    {
        int X { get; set; }
        int Y { get; set; }
    }
}
