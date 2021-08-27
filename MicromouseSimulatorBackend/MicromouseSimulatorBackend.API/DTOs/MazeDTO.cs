using MicromouseSimulatorBackend.BLL.Models;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class MazeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsFullSize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string GoalArea { get; set; }
        public string Walls { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public MazeDTO()
        {

        }
        public MazeDTO(Maze maze)
        {
            this.Id = maze.Id;
            this.Name = maze.Name;
            this.IsFullSize = maze.IsFullSize;
            this.Width = maze.Width;
            this.Height = maze.Height;
            this.GoalArea = maze.GoalArea;
            this.Walls = maze.Walls;
        }

        public Maze ToEntity()
        {
            return new Maze()
            {
                Id = this.Id,
                Name = this.Name,
                IsFullSize = this.IsFullSize,
                Width = this.Width,
                Height = this.Height,
                GoalArea = this.GoalArea,
                Walls = this.Walls
            };
        }
    }
}
