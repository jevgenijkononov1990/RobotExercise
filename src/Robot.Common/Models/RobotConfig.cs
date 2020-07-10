
namespace Robot.Common.Models
{
    public class RobotConfig 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RobotPosition Position { get; set; }
        public MatrixSize MatrixSize { get; set; }
    }
}
