using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Robot.Common.Logging;
using Robot.Common.Models;

namespace Robot.Infrastructure.Communication
{
    public class CommunicationHandler : ICommunicationHandler
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommunicationHandler()
        {

        }

        public (bool success, List<RobotCommand> robotCommands) ConvertInputToCommandList(string txtToCmd)
        {
            throw new NotImplementedException();
        }
    }
}
