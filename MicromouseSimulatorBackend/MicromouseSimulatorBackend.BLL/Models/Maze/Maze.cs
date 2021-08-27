
namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Mazes")]
    public class Maze : BaseDocument
    {
        public string Name { get; set; }
        public bool IsFullSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public GoalArea GoalArea { get; set; }
        public string Walls { get; set; }
    }
}
