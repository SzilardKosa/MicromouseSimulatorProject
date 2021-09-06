using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class Coordinate
    {
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
