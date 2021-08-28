using System;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class DocumentDoesntExistsException : Exception
    {
        public DocumentDoesntExistsException() : base()
        {
        } 
        
        public DocumentDoesntExistsException(string message) : base(message)
        {
        }
    }
}
