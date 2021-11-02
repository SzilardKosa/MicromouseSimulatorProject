using MicromouseSimulatorBackend.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class MouseDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Acceleration { get; set; } // m/s/s
        [Required]
        public double Deceleration { get; set; } // m/s/s
        [Required]
        public double MaxSpeed { get; set; } // m/s
        [Required]
        public double TurnTime { get; set; } // s


        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public MouseDTO()
        {

        }
        public MouseDTO(Mouse mouse)
        {
            this.Id = mouse.Id;
            this.Name = mouse.Name;
            this.Acceleration = mouse.Acceleration;
            this.Deceleration = mouse.Deceleration;
            this.MaxSpeed = mouse.MaxSpeed;
            this.TurnTime = mouse.TurnTime;
        }

        public Mouse ToEntity()
        {
            return new Mouse()
            {
                Id = this.Id,
                Name = this.Name,
                Acceleration = this.Acceleration,
                Deceleration = this.Deceleration,
                MaxSpeed = this.MaxSpeed,
                TurnTime = this.TurnTime
            };
        }
    }
}
