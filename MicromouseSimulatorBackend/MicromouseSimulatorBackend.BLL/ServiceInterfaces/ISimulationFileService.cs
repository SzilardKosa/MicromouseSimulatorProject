using MicromouseSimulatorBackend.BLL.Models;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationFileService
    {
        string Save(SimulationExpanded simulation);
        SimulationResult ReadResult(SimulationExpanded simulation);
        void DeleteById(string id);
    }
}
