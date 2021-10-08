using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class MazeJson
    {
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

        public MazeJson(Maze maze)
        {
            this.IsFullSize = maze.IsFullSize;
            this.Width = maze.Width;
            this.Height = maze.Height;
            this.GoalArea = new GoalArea(maze.GoalArea);
            this.Walls = wallsToString(maze.Walls);
        }

        private string wallsToString(List<List<CellWalls>> walls)
        {
            var numberOfRows = walls.Count;
            var numberOfCols = walls[0].Count;
            var result = "";

            // Top walls
            result += String.Concat(Enumerable.Repeat("o---", numberOfCols)) + "o\n";

            for (int row = numberOfRows - 1; row >= 0; row--)
            {
                // Vertical walls
                result += "|   ";
                for (int col = 1; col < numberOfCols; col++)
                {
                    var leftWall = walls[row][col].left;
                    result += leftWall ? "|   " : "    ";
                }
                result += "|\n";

                // Horizontal walls
                if (row == 0) break;
                for (int col = 0; col < numberOfCols; col++)
                {
                    var bottomWall = walls[row][col].bottom;
                    result += bottomWall ? "o---" : "o   ";
                }
                result += "o\n";
            }

            // Bottom walls
            result += String.Concat(Enumerable.Repeat("o---", numberOfCols)) + "o\n";

            return result;
        }
    }
}
