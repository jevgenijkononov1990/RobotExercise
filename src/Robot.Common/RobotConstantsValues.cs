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
        public static readonly string CommunicationError = "Robot has encountered communication error";
        public static readonly string CommandFailure = "Robot has failed to complete command";

        public static RobotConfig RobotDefaultCongifs = new RobotConfig
        {
            Name = "Rover",
            Position = new RobotPosition { X = 10, Y = 10, Direction = Enms.Direction.N },
            MatrixSize = new MatrixSize(40, 30),
        };
    }
}
