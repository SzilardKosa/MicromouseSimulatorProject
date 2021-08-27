
namespace MicromouseSimulatorBackend.BLL.Models
{
    [BsonCollection("Algorithms")]
    public class Algorithm : BaseDocument
    {
        public string Name { get; set; }
        public string Language { get; set; }
        public string CodeText { get; set; }
    }
}
