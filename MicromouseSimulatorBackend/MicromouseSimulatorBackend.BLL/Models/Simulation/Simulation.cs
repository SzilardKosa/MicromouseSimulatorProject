using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
    }
}
