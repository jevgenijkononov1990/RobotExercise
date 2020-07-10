using System.Threading;
using System.Threading.Tasks;

namespace Robot.Infrastructure.RobotService
{
    public interface IMainRobotService
    {
        bool OsInitialization();
        Task StartCommunicationThreadAsync(CancellationTokenSource cancellationTokenSource);
    }
}
