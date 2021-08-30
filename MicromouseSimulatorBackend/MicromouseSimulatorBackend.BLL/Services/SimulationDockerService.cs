﻿using Docker.DotNet;
using Docker.DotNet.Models;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System;
using System.Collections.Generic;
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
            // Create container
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
                    StopTimeout = new TimeSpan(0, 0, 30),
                });

            // Run container
            await client.Containers.StartContainerAsync(response.ID, null);
        }
    }
}