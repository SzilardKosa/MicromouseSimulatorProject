using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class ChangePassword
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
