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

        public IEnumerable<SimulationExpanded> FindAllAndPopulate(string userId)
        {
            var filterByUser = Builders<Simulation>.Filter.Eq(doc => doc.UserId, userId);
            return _collection
                .Aggregate()
                .Match(filterByUser)
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

        public SimulationExpanded FindByIdAndPopulate(string id, string userId)
        {
            if (!IsValidId(id))
                return null;
            var filterById = Builders<Simulation>.Filter.Eq(doc => doc.Id, id);
            var filterByUser = Builders<Simulation>.Filter.Eq(doc => doc.UserId, userId);
            return _collection.Aggregate()
                .Match(filterById)
                .Match(filterByUser)
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
