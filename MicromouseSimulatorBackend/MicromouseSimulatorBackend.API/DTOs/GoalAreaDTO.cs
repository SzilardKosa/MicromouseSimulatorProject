using MicromouseSimulatorBackend.BLL.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MicromouseSimulatorBackend.API.DTOs
{
    public class GoalAreaDTO
    {
        [Required]
        public Coordinate Cell1 { get; set; }
        [Required]
        public Coordinate Cell2 { get; set; }

        // Because of the way JSON Deserialization works (first an object is created, then
        // its properties set), we need a default constructor.
        public GoalAreaDTO()
        {

        }
        public GoalAreaDTO(Tuple<Coordinate, Coordinate> goalArea)
        {
            this.Cell1 = goalArea.Item1;
            this.Cell2 = goalArea.Item2;
        }

        public Tuple<Coordinate, Coordinate> ToEntity()
        {
            return new Tuple<Coordinate, Coordinate>
            (
                this.Cell1,
                this.Cell2
            );
        }
    }
}