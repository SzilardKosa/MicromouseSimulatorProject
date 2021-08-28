using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ISimulationRepository _simulationRepository;
        public SimulationService(ISimulationRepository simulationRepository)
        {
            _simulationRepository = simulationRepository;
        }

        public IEnumerable<SimulationExpanded> FindAll()
        {
            var result = _simulationRepository.FindAllAndPopulate().ToList();
            return result;
        }

        public SimulationExpanded FindById(string id)
        {
            return _simulationRepository.FindByIdAndPopulate(id);
        }

        public Simulation Create(Simulation document)
        {
            _simulationRepository.InsertOne(document);
            return document;
        }
        public void Update(string id, Simulation document)
        {
            if (_simulationRepository.FindById(id) == null)
            {
                throw new DocumentDoesntExistsException();
            }
            document.Id = id;
            _simulationRepository.ReplaceOne(id, document);
        }

        public void Delete(string id)
        {
            _simulationRepository.DeleteById(id);
        }
    }
}
