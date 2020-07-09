using Robot.Common.Enms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Common.Models
{
    public class RobotConfig 
    {
        public string Name { get; set; }
        public RobotStartPosition StartPosition { get; set; }
        public Direction StartDirection { get; set; }  
    }
}
