using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class SimulationResultDTO
    {
        [Required]
        public string Error { get; set; }
        [Required]
        public SimulationExpandedDTO Simulation { get; set; }
        [Required]
        public List<string> History { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public SimulationResultDTO()
        {

        }
        public SimulationResultDTO(SimulationResult result)
        {
            this.Error = result.Error;
            this.Simulation = new SimulationExpandedDTO(result.Simulation);
            this.History = result.History;
        }

        public SimulationResult ToEntity()
        {
            return new SimulationResult()
            {
                Error = this.Error,
                Simulation = this.Simulation.ToEntity(),
                History = this.History
            };
        }
    }
}
