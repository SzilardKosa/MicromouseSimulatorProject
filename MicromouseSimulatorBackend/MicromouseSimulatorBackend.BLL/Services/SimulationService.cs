using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System;
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

        public IEnumerable<SimulationExpanded> FindAll(string userId)
        {
            var result = _simulationRepository.FindAllAndPopulate(userId).ToList();
            return result;
        }

        public SimulationExpanded FindById(string id, string userId)
        {
            return _simulationRepository.FindByIdAndPopulate(id, userId);
        }

        public Simulation Create(Simulation simulation, string userId)
        {
            if (simulation.AlgorithmId != null && _algorithmRepository.FindById(simulation.AlgorithmId) == null)
                throw new Exception("The given AlgorithmId does not exist!");
            if (simulation.MazeId != null && _mazeRepository.FindById(simulation.MazeId) == null)
                throw new Exception("The given MazeId does not exist!");
            if (simulation.MouseId != null && _mouseRepository.FindById(simulation.MouseId) == null)
                throw new Exception("The given MouseId does not exist!");

            simulation.UserId = userId;
            _simulationRepository.InsertOne(simulation);
            return simulation;
        }

        public void Update(string id, Simulation simulation, string userId)
        {
            if (!simulationExists(id, userId))
                throw new DocumentDoesntExistsException("No Simulation exists with the given ID!");
            if (simulation.AlgorithmId != null && _algorithmRepository.FindById(simulation.AlgorithmId) == null)
                throw new Exception("The given AlgorithmId does not exist!");
            if (simulation.MazeId != null && _mazeRepository.FindById(simulation.MazeId) == null)
                throw new Exception("The given MazeId does not exist!");
            if (simulation.MouseId != null && _mouseRepository.FindById(simulation.MouseId) == null)
                throw new Exception("The given MouseId does not exist!");

            simulation.Id = id;
            simulation.UserId = userId;
            _simulationRepository.ReplaceOne(id, simulation);
        }

        public void Delete(string id, string userId)
        {
            if (simulationExists(id, userId))
            {
                _simulationRepository.DeleteById(id);
                _simulationFileService.DeleteById(id);
            }
        }

        public async Task<SimulationResult> RunSimulationAsync(string id, string userId)
        {
            var simulation = _simulationRepository.FindByIdAndPopulate(id, userId);
            if (simulation == null)
                throw new DocumentDoesntExistsException("No Simulation exists with the given ID!");
            if (simulation.Algorithm == null)
                throw new Exception("The Simulation does not contain any Algorithm!");
            if (simulation.Maze == null)
                throw new Exception("The Simulation does not contain any Maze!");

            var folderPath = _simulationFileService.Save(simulation);
            try
            {
                await _simulationDockerService.RunContainerAsync(simulation, folderPath);
            }
            catch (Exception e)
            {
                throw new DockerException(e.Message);
            }
            return _simulationFileService.ReadResult(simulation);
        }

        private bool simulationExists(string id, string userId)
        {
            if (!_simulationRepository.IsValidId(id))
                return false;
            var result = _simulationRepository.FilterBy(doc => doc.UserId == userId && doc.Id == id).FirstOrDefault();
            if (result == null)
                return false;
            return true;
        }
    }
}
