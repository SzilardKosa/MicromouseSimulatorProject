using MicromouseSimulatorBackend.BLL.Models;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class SimulationExpandedDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string AlgorithmId { get; set; }
        public string MazeId { get; set; }
        public string MouseId { get; set; }
        public AlgorithmDTO Algorithm { get; set; }
        public MazeDTO Maze { get; set; }
        public MouseDTO Mouse { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public SimulationExpandedDTO()
        {

        }
        public SimulationExpandedDTO(SimulationExpanded simulation)
        {
            this.Id = simulation.Id;
            this.Name = simulation.Name;
            this.AlgorithmId = simulation.AlgorithmId;
            this.MazeId = simulation.MazeId;
            this.MouseId = simulation.MouseId;
            if (simulation.Algorithm != null)
                this.Algorithm = new AlgorithmDTO(simulation.Algorithm);
            if (simulation.Maze != null)
                this.Maze = new MazeDTO(simulation.Maze);
            if (simulation.Mouse != null)
                this.Mouse = new MouseDTO(simulation.Mouse);
        }

        public SimulationExpanded ToEntity()
        {
            return new SimulationExpanded()
            {
                Id = this.Id,
                Name = this.Name,
                AlgorithmId = this.Algorithm.Id,
                Algorithm = this.Algorithm.ToEntity(),
                MouseId = this.Mouse.Id,
                Mouse = this.Mouse.ToEntity(),
                MazeId = this.Maze.Id,
                Maze = this.Maze.ToEntity(),
            };
        }
    }
}
