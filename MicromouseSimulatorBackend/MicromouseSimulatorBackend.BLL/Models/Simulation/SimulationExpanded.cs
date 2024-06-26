﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicromouseSimulatorBackend.BLL.Models
{
    public class SimulationExpanded : BaseDocument
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AlgorithmId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MazeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MouseId { get; set; }

        public Algorithm Algorithm { get; set; }
        public Maze Maze { get; set; }
        public Mouse Mouse { get; set; }
    }
}
