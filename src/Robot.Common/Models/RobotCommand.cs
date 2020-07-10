using Robot.Common.Enms;

namespace Robot.Common.Models
{
    public class RobotCommand
    {
        public string OriginalCmd{ get; set; }
        public RobotCommandType Type { get; set; }
        public Direction CurentDirection { get; set; }
        public int QueueId { get; set; }
        public Position MoveTo { get; set; }
    }
}
