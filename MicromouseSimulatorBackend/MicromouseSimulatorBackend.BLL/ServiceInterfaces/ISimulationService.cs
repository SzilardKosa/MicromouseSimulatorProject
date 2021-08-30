using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationService
    {
        IEnumerable<SimulationExpanded> FindAll();
        SimulationExpanded FindById(string id);
        Simulation Create(Simulation document);
        void Update(string id, Simulation document);
        void Delete(string id);
        Task RunSimulationAsync(string id);
    }
}
