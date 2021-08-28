using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface ISimulationService
    {
        IEnumerable<SimulationExpanded> FindAll();
        Simulation FindById(string id);
        Simulation Create(Simulation document);
        void Update(string id, Simulation document);
        void Delete(string id);
    }
}
