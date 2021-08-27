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
        public bool IsFullSize { get; set; }
     

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public MouseDTO()
        {

        }
        public MouseDTO(Mouse mouse)
        {
            this.Id = mouse.Id;
            this.Name = mouse.Name;
            this.IsFullSize = mouse.IsFullSize;
        }

        public Mouse ToEntity()
        {
            return new Mouse()
            {
                Id = this.Id,
                Name = this.Name,
                IsFullSize = this.IsFullSize,
            };
        }
    }
}
