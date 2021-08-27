
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class GoalArea
    {
        [Required]
        public Coordinate TopLeft { get; set; }
        [Required]
        public Coordinate BottomRight { get; set; }
    }
}
