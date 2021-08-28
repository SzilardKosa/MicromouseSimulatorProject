using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.RepositoryInterfaces;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class BaseService<TDocument> : IBaseService<TDocument> where TDocument : BaseDocument
    {
        private readonly IBaseRepository<TDocument> _repository;

        public BaseService(IBaseRepository<TDocument> repository)
        {
            this._repository = repository;
        }
        public IEnumerable<TDocument> FindAll()
        {
            return _repository.FilterBy(TDocument => true).ToList();
        }

        public TDocument FindById(string id)
        {
            return _repository.FindById(id);
        }

        public virtual TDocument Create(TDocument document)
        {
            _repository.InsertOne(document);
            return document;
        }
        public virtual void Update(string id, TDocument document)
        {
            if(_repository.FindById(id) == null)
            {
                throw new DocumentDoesntExistsException();
            }
            document.Id = id;
            _repository.ReplaceOne(id, document);
        }

        public virtual void Delete(string id)
        {
            _repository.DeleteById(id);
        }

    }
}
