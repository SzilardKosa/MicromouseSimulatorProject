using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace MicromouseSimulatorBackend.BLL.Services
{
    public class SimulationFileService : ISimulationFileService
    {
        private Dictionary<string, string> languageToFileName = new Dictionary<string, string>()
        {
            {"C", "main.c" },
            {"C++", "main.cpp" },
            {"Python", "main.py" }
        };

        public void DeleteById(string id)
        {
            var currentPath = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(currentPath, "Resources", "Simulations", id);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);
        }

        public string Save(SimulationExpanded simulation)
        {
            // create an empty sim folder
            var currentPath = Directory.GetCurrentDirectory();
            var folderPath = Path.Combine(currentPath, "Resources", "Simulations", simulation.Id);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);
            Directory.CreateDirectory(folderPath);

            // save the algo to a file
            var algorithm = simulation.Algorithm;
            var algorithmFileName = languageToFileName[algorithm.Language];
            var algorithmFilePath = Path.Combine(folderPath, algorithmFileName);
            File.WriteAllText(algorithmFilePath, algorithm.CodeText);

            // save the maze to a file
            var maze = simulation.Maze;
            var mazeFilePath = Path.Combine(folderPath, "maze.json");
            var jsonString = JsonSerializer.Serialize(maze);
            File.WriteAllText(mazeFilePath, jsonString);

            return folderPath;
        }
    }
}
