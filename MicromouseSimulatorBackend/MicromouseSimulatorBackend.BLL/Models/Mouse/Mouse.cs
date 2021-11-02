
namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Mice")]
    public class Mouse : BaseDocument
    {
        public string Name { get; set; }
        public double Acceleration { get; set; } // m/s/s
        public double Deceleration { get; set; } // m/s/s
        public double MaxSpeed { get; set; } // m/s
        public double TurnTime { get; set; } // s

    }
}
