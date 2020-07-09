using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Robot.Infrastructure.RobotService
{
    public interface IMianRobotService
    {
        bool InitializeRobotFramework();
        Task StartRobotFrameworkThreadAsync(CancellationTokenSource cancellationTokenSource);
    }
}
