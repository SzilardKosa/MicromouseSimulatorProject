using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class MazeService : BaseService<Maze>, IMazeService
    {
        public MazeService(IMongoRepository<Maze> mazeRepository) : base(mazeRepository)
        {
        }

    }
}
