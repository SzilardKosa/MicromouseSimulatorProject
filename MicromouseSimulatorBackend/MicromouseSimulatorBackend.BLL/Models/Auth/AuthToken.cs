using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class AuthToken
    {
        [Required]
        public string Token { get; set; }
    }
}
