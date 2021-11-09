using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface IBaseService<TDocument> where TDocument : BaseDocument
    {
        IEnumerable<TDocument> FindAll(string userId);
        TDocument FindById(string id, string userId);
        TDocument Create(TDocument document, string userId);
        void Update(string id, TDocument document, string userId);
        void Delete(string id, string userId);
    }
}
