﻿using Robot.Common.Enms;

namespace Robot.Common.Models
{
    public class RobotCommandView
    {
        public string  OriginalText{ get; set; }
        public RobotCommand Cmd { get; set; }
        public Direction CurentDirection { get; set; }
        public int Id { get; set; }
    }
}