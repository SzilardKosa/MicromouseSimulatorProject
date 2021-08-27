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

        public MongoDBRef AlgorithmRef { get; set; }
        public MongoDBRef MazeRef { get; set; }
        public MongoDBRef MouseRef { get; set; }

        [BsonIgnore]
        public Algorithm Algorithm { get; set; }
        [BsonIgnore]
        public Maze Maze { get; set; }
        [BsonIgnore]
        public Mouse Mouse { get; set; }
    }
}
