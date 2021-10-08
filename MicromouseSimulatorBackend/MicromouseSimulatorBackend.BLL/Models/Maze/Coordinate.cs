using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class Coordinate
    {
        [JsonPropertyName("x")]
        [Required]
        public int X { get; set; }
        [JsonPropertyName("y")]
        [Required]
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
