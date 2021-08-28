using MicromouseSimulatorBackend.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.RepositoryInterfaces
{
    public interface ISimulationRepository : IBaseRepository<Simulation>
    {
        IEnumerable<SimulationExpanded> FindAllAndPopulate();
        SimulationExpanded FindByIdAndPopulate(string id);
    }
}
