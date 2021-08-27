
namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Mice")]
    public class Mouse : BaseDocument
    {
        public string Name { get; set; }
        public bool IsFullSize { get; set; }
        
    }
}
