using MicromouseSimulatorBackend.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class GoalAreaDTO
    {
        [Required]
        public Coordinate TopLeft { get; set; }
        [Required]
        public Coordinate BottomRight { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public GoalAreaDTO()
        {

        }
        public GoalAreaDTO(GoalArea goalArea)
        {
            this.TopLeft = goalArea.TopLeft;
            this.BottomRight = goalArea.BottomRight;
        }

        public GoalArea ToEntity()
        {
            return new GoalArea()
            {
                TopLeft = this.TopLeft,
                BottomRight = this.BottomRight,
            };
        }
    }
}
