
using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Mazes")]
    public class Maze : BaseDocument
    {
        [JsonIgnore]
        public string Name { get; set; }
        [JsonPropertyName("is_full_size")]
        public bool IsFullSize { get; set; }
        [JsonPropertyName("width")]
        public int Width { get; set; }
        [JsonPropertyName("height")]
        public int Height { get; set; }
        [JsonPropertyName("goal_area")]
        public GoalArea GoalArea { get; set; }
        [JsonPropertyName("maze_map")]
        public string Walls { get; set; }
    }
}
