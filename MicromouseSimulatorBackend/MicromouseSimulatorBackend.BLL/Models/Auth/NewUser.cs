using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class NewUser
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
