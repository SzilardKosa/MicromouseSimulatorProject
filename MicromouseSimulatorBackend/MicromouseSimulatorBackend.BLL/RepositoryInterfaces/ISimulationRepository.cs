using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.BLL.RepositoryInterfaces
{
    public interface ISimulationRepository : IBaseRepository<Simulation>
    {
        IEnumerable<SimulationExpanded> FindAllAndPopulate();
        SimulationExpanded FindByIdAndPopulate(string id);
    }
}
