using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class SimulationResult
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonIgnore]
        public SimulationExpanded Simulation { get; set; }
        [JsonIgnore]
        public List<string> History { get; set; }
    }
}
