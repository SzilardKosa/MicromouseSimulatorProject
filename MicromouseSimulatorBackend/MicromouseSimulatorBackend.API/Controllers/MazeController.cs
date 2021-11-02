using System.Collections.Generic;
using System.Linq;
using MicromouseSimulatorBackend.API.DTOs;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using MicromouseSimulatorBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicromouseSimulatorBackend.API.Controllers
{
    [Route("mazes")]
    [ApiController]
    public class MazeController : ControllerBase
    {
        private readonly IMazeService _service;
        public MazeController(IMazeService service)
        {
            this._service = service;
        }

        // Fetch all mazes
        [HttpGet]
        public ActionResult<IEnumerable<MazeDTO>> GetMazes()
        {
            return Ok(_service.FindAll().Select(m => new MazeDTO(m)));
        }

        // Find one maze by id
        [HttpGet("{id}")]
        public ActionResult<MazeDTO> GetMaze(string id)
        {
            var result = _service.FindById(id);

            if (result == null)
                return NotFound();

            return Ok(new MazeDTO(result));
        }

        // Create a new maze
        [HttpPost]
        public ActionResult<MazeDTO> CreateNewMaze(NewMazeDTO mazeDTO)
        {
            // Handle error if no data is sent.
            if (mazeDTO == null)
                return BadRequest("Maze data must be set!");
            // Handle error if id is sent.
            if (mazeDTO.Id != null)
                return BadRequest("No ID should be provided!");

            // Map the DTO to entity and save the entity
            Maze createdEntity = _service.Create(mazeDTO.ToEntity());

            // According to the conventions, we have to return a HTTP 201 created repsonse, with
            // field "Location" in the header pointing to the created object
            return CreatedAtAction(
                nameof(GetMaze),
                new { id = createdEntity.Id },
                new MazeDTO(createdEntity));
        }

        // Update an existing maze
        [HttpPut("{id}")]
        public ActionResult UpdateMaze(string id, NewMazeDTO mazeDTO)
        {
            // Handle error if no data is sent.
            if (mazeDTO == null)
                return BadRequest("Maze data must be set!");

            try
            {
                // Map the DTO to entity and save it
                _service.Update(id, mazeDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content.
                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                // Handle error if the maze to update doesn't exists.
                return BadRequest("No Maze exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMaze(string id)
        {
            _service.Delete(id);
            // According to the conventions, we have to return HTTP 204 No Content.
            return NoContent();
        }
    }
}
