using System;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Mazes")]
    public class Maze : BaseDocument
    {
        public string Name { get; set; }
        public bool IsFullSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Tuple<Coordinate, Coordinate> GoalArea { get; set; }
        public List<List<CellWalls>> Walls { get; set; }
    }
}
