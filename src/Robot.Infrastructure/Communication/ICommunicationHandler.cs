
using Robot.Common.Models;
using System.Collections.Generic;

namespace Robot.Infrastructure.Communication
{
    public interface ICommunicationHandler
    {
        (bool success, List<RobotCommand> robotCommands) ConvertInputToCommandList(string txtToConvert, RobotPosition currentPosition);
    }
}
