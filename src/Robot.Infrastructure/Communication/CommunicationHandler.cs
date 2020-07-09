using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Robot.Common.Logging;

namespace Robot.Infrastructure.Communication
{
    public class CommunicationHandler : ICommunicationHandler
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public CommunicationHandler()
        {

        }

        (bool success, List<string> cmds) ReadCommand(string txtToCmd)
        {
            return (false, null);
        }
    }
}
