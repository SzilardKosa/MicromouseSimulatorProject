using MicromouseSimulatorBackend.BLL.Models;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface IAuthService
    {
        Task Register(NewUser newUser);
        Task<AuthToken> Login(Login login);
    }
}
