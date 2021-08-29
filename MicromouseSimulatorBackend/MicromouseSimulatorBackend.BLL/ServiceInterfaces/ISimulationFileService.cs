using MicromouseSimulatorBackend.BLL.Models;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationFileService
    {
        void SaveSimulation(SimulationExpanded simulation);
        void DeleteSimulation(string id);
    }
}
