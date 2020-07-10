using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Common.Models
{
    public class StateResponse
    {
        public string ResponseTime { get; private set; } = DateTime.UtcNow.ToString();
        public RobotPosition CurrentPosition { get; set; }
    }
}
