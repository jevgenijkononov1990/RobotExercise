using Robot.Common.Enms;
using Robot.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Robot.Infrastructure.Communication.Decoders
{
    public class RotationCommandDecoder : IDecoder
    {
        public RotationCommandDecoder()
        {

        }

        public List<DecodedInput> DecodeInput(string textToDecode)
        {
            List<string> splittedInputListByInt = Regex.Matches(textToDecode, @"\d+|\D+")
                           .Cast<Match>()
                           .Select(m => m.Value)
                           .ToList();

            if (splittedInputListByInt == null)
            {
                return (null);
            }

            bool nextShouldBeNumber = false;
            string currentRotationPointer = "";
            var decodedCommands = new List<DecodedInput>();
            RobotCommandType robotCommandType = RobotCommandType.Unknown;

            foreach (var item in splittedInputListByInt)
            {
                if (item == "L")
                {
                    nextShouldBeNumber = true;
                    currentRotationPointer = "L";
                    robotCommandType = RobotCommandType.Move;
                }
                else if (item == "R")
                {
                    nextShouldBeNumber = true;
                    currentRotationPointer = "R";
                    robotCommandType = RobotCommandType.Move;
                }
                else
                {
                    if (nextShouldBeNumber == true)
                    {
                        int number;

                        bool success = int.TryParse(item, out number);

                        if (success)
                        {
                            Console.WriteLine($"CMD:{currentRotationPointer}{number}");
                            decodedCommands.Add(new DecodedInput
                            {
                                OriginalCmd = $"CMD:{currentRotationPointer}{number}",
                                MovementDistance = number,
                                RotationType = currentRotationPointer == "L" ? RotationType.Left : RotationType.Right,
                                CommandType = robotCommandType
                            });
                        }

                        nextShouldBeNumber = false;
                    }
                    currentRotationPointer = "";
                }
            }

            return decodedCommands;
        }
    }

}
