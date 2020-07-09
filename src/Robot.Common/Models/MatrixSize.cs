
namespace Robot.Common.Models
{
    public class MatrixSize
    {
        public int Min_X_Value { get; set; }// = int.MinValue;
        public int Max_X_Value { get; set; }// = int.MaxValue;
                                            //
        public int Min_Y_Value { get; set; }// = int.MinValue;
        public int Max_Y_Value { get; set; }// = int.MaxValue;
                                            //
        public int Min_Z_Value { get; set; }// = int.MinValue;
        public int Max_Z_Valye { get; set; }// = int.MaxValue;

        public MatrixSize()
        {

        }

        public MatrixSize(int Xmax, int Ymax)
        {
            Min_X_Value = 0;
            Max_X_Value = Xmax;

            Min_Y_Value = 0;
            Max_Y_Value = Ymax;                             
        }
    }
}
