using MicromouseSimulatorBackend.BLL.Models;
using System.Collections.Generic;

namespace MicromouseSimulatorBackend.BLL.ServiceInterfaces
{
    public interface IBaseService<TDocument> where TDocument : BaseDocument
    {
        IEnumerable<TDocument> FindAll();
        TDocument FindById(string id);
        TDocument Create(TDocument document);
        void Update(string id, TDocument document);
        void Delete(string id);
    }
}
