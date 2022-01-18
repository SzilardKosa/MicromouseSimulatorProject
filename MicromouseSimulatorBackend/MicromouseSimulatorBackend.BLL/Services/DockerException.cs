using System;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class DockerException : Exception
    {
        public DockerException() : base()
        {
        }

        public DockerException(string message) : base(message)
        {
        }
    }
}
