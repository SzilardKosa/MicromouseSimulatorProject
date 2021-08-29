using MicromouseSimulatorBackend.BLL.Models;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface IFileService
    {
        void SaveSimulation(SimulationExpanded simulation);
    }
}
