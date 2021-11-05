using Docker.DotNet;
using Docker.DotNet.Models;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class SimulationDockerService : ISimulationDockerService
    {
        private Dictionary<string, string> languageToImageName = new Dictionary<string, string>()
        {
            {"C", "my-c-simulator" },
            {"C++", "my-cpp-simulator" },
            {"Python", "my-python-simulator" }
        };

        public async Task RunContainerAsync(SimulationExpanded simulation, string folderPath)
        {
            DockerClient client = new DockerClientConfiguration().CreateClient();
            var imageName = languageToImageName[simulation.Algorithm.Language];
            var response = await client.Containers.CreateContainerAsync(
                new CreateContainerParameters
                {
                    Image = imageName,
                    HostConfig = new HostConfig
                    {
                        Mounts = new[] {
                            new Mount
                            {
                                Target = "/usr/src/app/shared",
                                Source = folderPath,
                                Type = "bind",
                                ReadOnly = false,
                            }
                        },
                        AutoRemove = true,
                    },
                });

            await client.Containers.StartContainerAsync(response.ID, null);
            var timeout = 30;
            var hasTimedOut = false;
            while (true)
            {
                Thread.Sleep(1000);
                try
                {
                    var info = await client.Containers.InspectContainerAsync(response.ID, CancellationToken.None);
                    if (!info.State.Running) break;
                } catch (DockerContainerNotFoundException) { break; }

                timeout -= 1;
                if (timeout == 0)
                {
                    hasTimedOut = true;
                    break;
                }
            }
            if (hasTimedOut)
            {
                await client.Containers.StopContainerAsync(response.ID, new ContainerStopParameters {}, CancellationToken.None);
            }
        }
    }
}
