using Robot.Common.Enms;

namespace Robot.Common.Models
{
    public class RobotCommandView
    {
        public string  OriginalText{ get; set; }
        public RobotCommand Command { get; set; }
        public Direction CurentDirection { get; set; }
        public int QueueId { get; set; }
        public MatrixLocation MoveTo { get; set; }
    }
}
