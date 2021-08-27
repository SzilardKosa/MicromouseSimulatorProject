using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class MouseService : BaseService<Mouse>, IMouseService
    {
        public MouseService(IMongoRepository<Mouse> mouseRepository) : base(mouseRepository)
        {
        }

    }
}
