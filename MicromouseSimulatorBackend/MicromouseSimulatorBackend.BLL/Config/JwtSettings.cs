
namespace MicromouseSimulatorBackend.BLL.Config
{
    public class JwtSettings : IJwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
    }

    public interface IJwtSettings
    {
        string Key { get; set; }
        string Issuer { get; set; }
    }
}
