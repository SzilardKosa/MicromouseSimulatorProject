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

        public IEnumerable<TDocument> FindAll(string userId)
        {
            return _repository.FilterBy(doc => doc.UserId == userId).ToList();
        }

        public TDocument FindById(string id, string userId)
        {
            if (!_repository.IsValidId(id))
                return null;
            return _repository.FilterBy(doc => doc.UserId == userId && doc.Id == id).FirstOrDefault();
        }

        public virtual TDocument Create(TDocument document, string userId)
        {
            document.UserId = userId;
            _repository.InsertOne(document);
            return document;
        }

        public virtual void Update(string id, TDocument document, string userId)
        {
            if(FindById(id, userId) == null)
            {
                throw new DocumentDoesntExistsException();
            }
            document.Id = id;
            document.UserId = userId;
            _repository.ReplaceOne(id, document);
        }

        public virtual void Delete(string id, string userId)
        {
            if (FindById(id, userId) != null)
            {
                _repository.DeleteById(id);
            }
        }

    }
}
