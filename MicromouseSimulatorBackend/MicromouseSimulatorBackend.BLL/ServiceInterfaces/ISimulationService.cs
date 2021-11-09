using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationService
    {
        IEnumerable<SimulationExpanded> FindAll(string userId);
        SimulationExpanded FindById(string id, string userId);
        Simulation Create(Simulation document, string userId);
        void Update(string id, Simulation document, string userId);
        void Delete(string id, string userId);
        Task<SimulationResult> RunSimulationAsync(string id, string userId);
    }
}
