using System;
using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class GoalArea
    {
        [JsonPropertyName("top_left")]
        public Coordinate TopLeft { get; set; }
        [JsonPropertyName("bottom_right")]
        public Coordinate BottomRight { get; set; }

        public GoalArea(Tuple<Coordinate, Coordinate> goalArea)
        {
            var topLeftX = Math.Min(goalArea.Item1.X, goalArea.Item2.X);
            var topLeftY = Math.Max(goalArea.Item1.Y, goalArea.Item2.Y);
            var bottomRightX = Math.Max(goalArea.Item1.X, goalArea.Item2.X);
            var bottomRightY = Math.Min(goalArea.Item1.Y, goalArea.Item2.Y);
            this.TopLeft = new Coordinate(topLeftX, topLeftY);
            this.BottomRight = new Coordinate(bottomRightX, bottomRightY);
        }
    }
}
