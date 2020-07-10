using Robot.Common.Models;


namespace Robot.Common
{
    public static class RobotConstantsValues
    {
        #region StringConstants
        public static readonly string ConstructorInitFailure = "Constructor Initialization failure due to:";
        public static readonly string CriticalErrorOccuredMissionWillContinue = "Robot has encountered critical error. Mission will be continued, but with a risk of losing data";
        public static readonly string CriticalErrorOccuredMissionWillNotContinue = "Robot has encountered critical error and requires program termination or restart";
        public static readonly string CommunicationError = "Robot has encountered communication error";
        public static readonly string CommandFailure = "Robot has failed to complete command";
        public static readonly string RobotProgramLastMessage = "Robot program termination....";
        public static readonly string InvalidMatrDefault_Matrix_XixSetupValue = "Invalid matrix setup";
        public static readonly string MatrixSetupFailureBackToDefault = "Matrix was init with error, robot wil not be able to move correctly. Matrix settings is back to default";
        #endregion

        #region Default_Settings
        public static readonly int Default_Matrix_Xmax = 40;
        public static readonly int Default_Matrix_Ymax = 30;
        public static readonly Enms.Direction Default_Direction = Enms.Direction.N;
        public static readonly string Default_RobotName = "Rover";
        public static readonly int Default_Robot_Id = 1;

        public static readonly RobotConfig RobotDefaultCongifs = new RobotConfig
        {
            Id = Default_Robot_Id,
            Name = Default_RobotName,
            Position = new RobotPosition { X = 10, Y = 10, Direction = Default_Direction },
            MatrixSize = new MatrixSize(Default_Matrix_Xmax, Default_Matrix_Ymax),
        };
        #endregion
    }
}
