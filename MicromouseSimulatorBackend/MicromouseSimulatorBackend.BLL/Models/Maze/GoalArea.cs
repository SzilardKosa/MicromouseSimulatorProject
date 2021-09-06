using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class GoalArea
    {
        [JsonPropertyName("top_left")]
        public Coordinate TopLeft { get; set; }
        [JsonPropertyName("bottom_right")]
        public Coordinate BottomRight { get; set; }
    }
}
