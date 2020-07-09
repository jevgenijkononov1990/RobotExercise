using System.Threading;
using System.Threading.Tasks;

namespace Robot.Infrastructure.RobotService
{
    public interface IMainRobotService
    {
        bool InitializeFramework();
        Task StartCommunicationThreadAsync(CancellationTokenSource cancellationTokenSource);
    }
}
