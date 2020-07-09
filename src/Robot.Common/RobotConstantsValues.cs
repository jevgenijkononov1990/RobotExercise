using Robot.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Robot.Common
{
    public static class RobotConstantsValues
    {
        public static readonly string ConstructorInitFailure = "Constructor Initialization failure due to:";
        public static readonly string CriticalErrorOccuredMissionWillContinue = "Robot has encountered critical error. Mission will be continued, but with a risk of losing data";
        public static readonly string CriticalErrorOccuredMissionWillNotContinue = "Robot has encountered critical error and requires program termination or restart";

        public static RobotConfig RobotDefaultCongifs = new RobotConfig
        {
            Name = "Rover",
            StartPosition = new RobotStartPosition { X = 10, Y = 10 },
            StartDirection = Enms.Direction.North
        };

    }
}
