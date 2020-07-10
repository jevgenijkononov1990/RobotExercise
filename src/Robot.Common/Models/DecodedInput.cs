using Robot.Common.Enms;

namespace Robot.Common.Models
{
    public class DecodedInput
    {
        public RobotCommandType CommandType { get; set; }
        public RotationType RotationType { get; set; }
        public string OriginalCmd { get; set; }
        public int MovementDistance { get; set; }
    }
}
