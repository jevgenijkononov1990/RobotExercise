using Robot.Common.Logging;
using System.Reflection;

namespace Robot.Common.Models
{
    public class MatrixSize
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public int Min_X_Value { get; private set; }// = int.MinValue;
        public int Max_X_Value { get; private set; }// = int.MaxValue;
                                          
        public int Min_Y_Value { get; private set; }// = int.MinValue;
        public int Max_Y_Value { get; private set; }// = int.MaxValue;
                                            
        public bool X_Has_Limit { get; private set; } 
        public bool Y_Has_Limit { get; private set; }


        public MatrixSize(int Xmax, int Ymax)
        {
            ValidateSetupValue(Xmax);
            ValidateSetupValue(Ymax);

            Min_X_Value = 0;
            Max_X_Value = Xmax;

            Min_Y_Value = 0;
            Max_Y_Value = Ymax;

            ValidateMatrixSetup();
        }

        public MatrixSize(int Xmin = 0, int Xmax = int.MaxValue, int Ymin = 0, int Ymax = int.MaxValue)
        {
            ValidateSetupValue(Xmin);
            ValidateSetupValue(Xmax);
            ValidateSetupValue(Ymin);
            ValidateSetupValue(Ymax);

            Min_X_Value = Xmin;
            Max_X_Value = Xmax;

            Min_Y_Value = Ymin;
            Max_Y_Value = Ymax;

            ValidateMatrixSetup();
        }

        private void ValidateSetupValue(int value)
        {
            if(GeneralValidationHelper.IsIntegerValueNegative(value))
            {
                _log.Message(LogLevel.Warn, RobotConstantsValues.MatrixSetupFailureBackToDefault);
                // no need to throw exception. Mission will fail
                PreDefaultMatrix();
            }
        }

        private void ValidateMatrixSetup()
        {
            if(Min_X_Value == Max_X_Value && Min_Y_Value == Max_Y_Value)
            {
                _log.Message(LogLevel.Warn, RobotConstantsValues.MatrixSetupFailureBackToDefault);
                // no need to throw exception. Mission will fail
                PreDefaultMatrix();
            }

            if (Min_X_Value > Max_X_Value || Min_Y_Value > Max_X_Value)
            {
                _log.Message(LogLevel.Warn, RobotConstantsValues.MatrixSetupFailureBackToDefault);
                // no need to throw exception. Mission will fail
                PreDefaultMatrix();
            }

            if(Max_X_Value != int.MaxValue)
            {
                X_Has_Limit = true;
            }

            if (Max_Y_Value != int.MaxValue)
            {
                Y_Has_Limit = true;
            }
        }
        private void PreDefaultMatrix()
        {
            
            Min_X_Value = 0;
            Max_X_Value = RobotConstantsValues.Default_Matrix_Xmax;

            Min_Y_Value = 0;
            Max_Y_Value = RobotConstantsValues.Default_Matrix_Ymax;
        }
    }
}


