using MicromouseSimulatorBackend.BLL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class MazeDTO
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsFullSize { get; set; }
        [Required]
        [Range(1, 32)]
        public int Width { get; set; }
        [Required]
        [Range(1, 32)]
        public int Height { get; set; }
        [Required]
        public GoalAreaDTO GoalArea { get; set; }
        [Required]
        public List<List<CellWalls>> Walls { get; set; }

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
            this.GoalArea = new GoalAreaDTO(maze.GoalArea);
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
                GoalArea = this.GoalArea.ToEntity(),
                Walls = this.Walls
            };
        }
    }
}
