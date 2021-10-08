using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class CellWalls
    {
        [Required]
        public bool bottom { get; set; }
        [Required]
        public bool left { get; set; }
    }
}
