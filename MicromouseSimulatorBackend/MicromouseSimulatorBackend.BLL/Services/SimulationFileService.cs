using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using System;
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

        public string Save(SimulationExpanded simulation)
        {
            // create an empty sim folder
            var folderPath = getFolderPath(simulation.Id);
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
            var mazeJson = new MazeJson(maze);
            var mazeFilePath = Path.Combine(folderPath, "maze.json");
            var jsonString = JsonSerializer.Serialize(mazeJson);
            File.WriteAllText(mazeFilePath, jsonString);

            return folderPath;
        }

        public SimulationResult ReadResult(SimulationExpanded simulation)
        {
            // read the result.json
            var folderPath = getFolderPath(simulation.Id);
            var resultFileName = Path.Combine(folderPath, "result.json");
            var result = new SimulationResult();
            if (File.Exists(resultFileName))
            {
                string jsonString = File.ReadAllText(resultFileName);
                result = JsonSerializer.Deserialize<SimulationResult>(jsonString);
            }
            else
            {
                result.Error = "The simulation stopped with no result! Most probably it timed out!";
            }

            // read the history and insert inside the list
            var historyFileName = Path.Combine(folderPath, "history.txt");
            result.History = new List<string>();
            try
            {
                using (var sr = new StreamReader(historyFileName))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        result.History.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // save the current simulation in the result
            result.Simulation = simulation;

            return result;
        }

        public void DeleteById(string id)
        {
            var folderPath = getFolderPath(id);
            if (Directory.Exists(folderPath))
                Directory.Delete(folderPath, true);
        }

        private string getFolderPath(string id)
        {
            var currentPath = Directory.GetCurrentDirectory();
            return Path.Combine(currentPath, "Resources", "Simulations", id);
        }
    }
}
