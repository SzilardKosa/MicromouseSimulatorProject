using MicromouseSimulatorBackend.BLL.Models;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface IUserService
    {
        Task ChangePassword(ChangePassword change, string userId);
    }
}
