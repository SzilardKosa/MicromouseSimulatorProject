using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class SimulationService : BaseService<Simulation>, ISimulationService
    {
        public SimulationService(IMongoRepository<Simulation> simulationRepository) : base(simulationRepository)
        {
        }

    }
}
