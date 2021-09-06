using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly ISimulationRepository _simulationRepository;
        private readonly IBaseRepository<Algorithm> _algorithmRepository;
        private readonly IBaseRepository<Maze> _mazeRepository;
        private readonly IBaseRepository<Mouse> _mouseRepository;
        private readonly ISimulationFileService _simulationFileService;
        private readonly ISimulationDockerService _simulationDockerService;

        public SimulationService(
            ISimulationRepository simulationRepository,
            IBaseRepository<Algorithm> algorithmRepository,
            IBaseRepository<Maze> mazeRepository,
            IBaseRepository<Mouse> mouseRepository,
            ISimulationFileService simulationFileService,
            ISimulationDockerService simulationDockerService
            )
        {
            _simulationRepository = simulationRepository;
            _algorithmRepository = algorithmRepository;
            _mazeRepository = mazeRepository;
            _mouseRepository = mouseRepository;
            _simulationFileService = simulationFileService;
            _simulationDockerService = simulationDockerService;
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

        public Simulation Create(Simulation simulation)
        {
            if (simulation.AlgorithmId != null && _algorithmRepository.FindById(simulation.AlgorithmId) == null)
                throw new DocumentDoesntExistsException("The given AlgorithmId does not exist!");
            if (simulation.MazeId != null && _mazeRepository.FindById(simulation.MazeId) == null)
                throw new DocumentDoesntExistsException("The given MazeId does not exist!");
            if (simulation.MouseId != null && _mouseRepository.FindById(simulation.MouseId) == null)
                throw new DocumentDoesntExistsException("The given MouseId does not exist!");

            _simulationRepository.InsertOne(simulation);
            return simulation;
        }
        public void Update(string id, Simulation simulation)
        {
            if (_simulationRepository.FindById(id) == null)
                throw new DocumentDoesntExistsException("No Simulation exists with the given ID!");
            if (simulation.AlgorithmId != null && _algorithmRepository.FindById(simulation.AlgorithmId) == null)
                throw new DocumentDoesntExistsException("The given AlgorithmId does not exist!");
            if (simulation.MazeId != null && _mazeRepository.FindById(simulation.MazeId) == null)
                throw new DocumentDoesntExistsException("The given MazeId does not exist!");
            if (simulation.MouseId != null && _mouseRepository.FindById(simulation.MouseId) == null)
                throw new DocumentDoesntExistsException("The given MouseId does not exist!");

            simulation.Id = id;
            _simulationRepository.ReplaceOne(id, simulation);
        }

        public void Delete(string id)
        {
            _simulationRepository.DeleteById(id);

            _simulationFileService.DeleteById(id);
        }

        public async Task<SimulationResult> RunSimulationAsync(string id)
        {
            var simulation = _simulationRepository.FindByIdAndPopulate(id);
            if (simulation == null)
                throw new DocumentDoesntExistsException("No Simulation exists with the given ID!");
            if (simulation.Algorithm == null)
                throw new DocumentDoesntExistsException("The Simulation does not contain any Algorithm!");
            if (simulation.Maze == null)
                throw new DocumentDoesntExistsException("The Simulation does not contain any Maze!");

            var folderPath = _simulationFileService.Save(simulation);
            await _simulationDockerService.RunContainerAsync(simulation, folderPath);
            return _simulationFileService.ReadResult(simulation);
        }
    }
}
