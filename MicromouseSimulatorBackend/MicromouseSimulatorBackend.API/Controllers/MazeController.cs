using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [HttpGet]
        public ActionResult<IEnumerable<MazeDTO>> GetMazes()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_service.FindAll(userId).Select(m => new MazeDTO(m)));
        }

        [HttpGet("{id}")]
        public ActionResult<MazeDTO> GetMaze(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _service.FindById(id, userId);

            if (result == null)
                return NotFound();

            return Ok(new MazeDTO(result));
        }

        [HttpPost]
        public ActionResult<MazeDTO> CreateNewMaze(NewMazeDTO mazeDTO)
        {
            if (mazeDTO.Id != null)
                return BadRequest("No ID should be provided!");

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Maze createdEntity = _service.Create(mazeDTO.ToEntity(), userId);

            return CreatedAtAction(
                nameof(GetMaze),
                new { id = createdEntity.Id },
                new MazeDTO(createdEntity));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMaze(string id, NewMazeDTO mazeDTO)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _service.Update(id, mazeDTO.ToEntity(), userId);

                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                return NotFound("No Maze exists with the given ID!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMaze(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _service.Delete(id, userId);
            return NoContent();
        }
    }
}
