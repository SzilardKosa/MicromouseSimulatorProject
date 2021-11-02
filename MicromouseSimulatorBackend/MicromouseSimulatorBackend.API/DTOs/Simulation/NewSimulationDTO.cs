using MicromouseSimulatorBackend.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class NewSimulationDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AlgorithmId { get; set; }
        public string MazeId { get; set; }
        public string MouseId { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public NewSimulationDTO()
        {

        }
        public NewSimulationDTO(Simulation simulation)
        {
            this.Id = simulation.Id;
            this.Name = simulation.Name;
            this.AlgorithmId = simulation.AlgorithmId;
            this.MazeId = simulation.MazeId;
            this.MouseId = simulation.MouseId;
        }

        public Simulation ToEntity()
        {
            return new Simulation()
            {
                Id = this.Id,
                Name = this.Name,
                AlgorithmId = this.AlgorithmId,
                MouseId = this.MouseId,
                MazeId = this.MazeId,
            };
        }
    }
}
