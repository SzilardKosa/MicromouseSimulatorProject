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
    [Route("algorithms")]
    [ApiController]
    public class AlgorithmController : ControllerBase
    {
        private readonly IAlgorithmService _service;
        public AlgorithmController(IAlgorithmService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlgorithmDTO>> GetAlgorithms()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_service.FindAll(userId).Select(a => new AlgorithmDTO(a)));
        }

        [HttpGet("{id}")]
        public ActionResult<AlgorithmDTO> GetAlgorithm(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _service.FindById(id, userId);

            if (result == null)
                return NotFound();

            return Ok(new AlgorithmDTO(result));
        }

        [HttpPost]
        public ActionResult<AlgorithmDTO> CreateNewAlgorithm(NewAlgorithmDTO algorithmDTO)
        {
            if (algorithmDTO.Id != null)
                return BadRequest("No ID should be provided!");

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Algorithm createdEntity = _service.Create(algorithmDTO.ToEntity(), userId);

            return CreatedAtAction(
                nameof(GetAlgorithm),
                new { id = createdEntity.Id },
                new AlgorithmDTO(createdEntity));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAlgorithm(string id, NewAlgorithmDTO algorithmDTO)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _service.Update(id, algorithmDTO.ToEntity(), userId);

                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                return NotFound("No Algorithm exists with the given ID!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAlgorithm(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _service.Delete(id, userId);
            return NoContent();
        }
    }
}
