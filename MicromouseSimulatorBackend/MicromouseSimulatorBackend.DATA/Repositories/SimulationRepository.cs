using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.DATA.Config;
using MongoDB.Driver;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.DATA.Repositories
{
    public class SimulationRepository : BaseRepository<Simulation>, ISimulationRepository
    {
        public SimulationRepository(IMicromouseDatabaseSettings settings) : base(settings)
        {
        }

        public IEnumerable<SimulationExpanded> FindAllAndPopulate()
        {
            return _collection
                .Aggregate()
                .Lookup(foreignCollectionName: _settings.AlgorithmsCollectionName,
                        localField: nameof(SimulationExpanded.AlgorithmId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Algorithm))
                .Lookup(foreignCollectionName: _settings.MazesCollectionName,
                        localField: nameof(SimulationExpanded.MazeId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Maze))
                .Lookup(foreignCollectionName: _settings.MiceCollectionName,
                        localField: nameof(SimulationExpanded.MouseId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Mouse))
                .Unwind(field: nameof(SimulationExpanded.Algorithm), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .Unwind(field: nameof(SimulationExpanded.Maze), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .Unwind(field: nameof(SimulationExpanded.Mouse), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .ToEnumerable();
        }

        public SimulationExpanded FindByIdAndPopulate(string id)
        {
            if (!isValidId(id))
                return null;
            var filter = Builders<Simulation>.Filter.Eq(doc => doc.Id, id);
            return _collection.Aggregate()
                .Match(filter)
                .Lookup(foreignCollectionName: _settings.AlgorithmsCollectionName,
                        localField: nameof(SimulationExpanded.AlgorithmId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Algorithm))
                .Lookup(foreignCollectionName: _settings.MazesCollectionName,
                        localField: nameof(SimulationExpanded.MazeId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Maze))
                .Lookup(foreignCollectionName: _settings.MiceCollectionName,
                        localField: nameof(SimulationExpanded.MouseId),
                        foreignField: "_id",
                        @as: nameof(SimulationExpanded.Mouse))
                .Unwind(field: nameof(SimulationExpanded.Algorithm), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .Unwind(field: nameof(SimulationExpanded.Maze), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .Unwind(field: nameof(SimulationExpanded.Mouse), new AggregateUnwindOptions<SimulationExpanded>() { PreserveNullAndEmptyArrays = true })
                .SingleOrDefault();
        }
    }
}
