using MicromouseSimulatorBackend.BLL.Models;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationDockerService
    {
        Task RunContainerAsync(SimulationExpanded simulation, string folderPath);
    }
}
