using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Simulations")]
    public class Simulation : BaseDocument
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string AlgorithmId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MazeId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string MouseId { get; set; }

        [BsonIgnore]
        public Algorithm Algorithm { get; set; }
        [BsonIgnore]
        public Maze Maze { get; set; }
        [BsonIgnore]
        public Mouse Mouse { get; set; }
    }
}
