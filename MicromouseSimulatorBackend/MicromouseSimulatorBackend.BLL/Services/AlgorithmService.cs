using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class AlgorithmService : BaseService<Algorithm>, IAlgorithmService
    {
        public AlgorithmService(IBaseRepository<Algorithm> algorithmRepository) : base(algorithmRepository)
        {
        }

    }
}
