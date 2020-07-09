using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Robot.Common.Enms;
using Robot.Common.Logging;
using Robot.Infrastructure.StateMachine.States;

namespace Robot.Infrastructure.StateMachine
{
    public class RobotStateMachineFactory : IRobotStateMachineFactory
    {
        private static readonly ILogger _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public RobotStateMachineFactory()
        {

        }
        public IStateStep Build(RobotCommand stepCommand)
        {

            throw new NotImplementedException();
        }
    }
}
