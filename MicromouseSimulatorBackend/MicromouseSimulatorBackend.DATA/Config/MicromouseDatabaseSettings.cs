
namespace MicromouseSimulatorBackend.DATA.Config
{
    public class MicromouseDatabaseSettings : IMicromouseDatabaseSettings
    {
        public string AlgorithmsCollectionName  { get; set; }
        public string MazesCollectionName { get; set; } 
        public string MiceCollectionName { get; set; }
        public string SimulationsCollectionName { get; set; }
        public string ConnectionString  { get; set; }
        public string DatabaseName  { get; set; }
    }
    public interface IMicromouseDatabaseSettings
    {
        string AlgorithmsCollectionName { get; set; }
        string MazesCollectionName { get; set; }
        string MiceCollectionName { get; set; }
        string SimulationsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
