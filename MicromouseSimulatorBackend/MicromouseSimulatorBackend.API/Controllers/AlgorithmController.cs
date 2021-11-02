using System.Collections.Generic;
using System.Linq;
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

        // Fetch all algorithms
        [HttpGet]
        public ActionResult<IEnumerable<AlgorithmDTO>> GetAlgorithms()
        {
            return Ok(_service.FindAll().Select(a => new AlgorithmDTO(a)));
        }

        // Find one algorithm by id
        [HttpGet("{id}")]
        public ActionResult<AlgorithmDTO> GetAlgorithm(string id)
        {
            var result = _service.FindById(id);

            if (result == null)
                return NotFound();

            return Ok(new AlgorithmDTO(result));
        }

        // Create a new algorithm
        [HttpPost]
        public ActionResult<AlgorithmDTO> CreateNewAlgorithm(NewAlgorithmDTO algorithmDTO)
        {
            // Handle error if no data is sent.
            if (algorithmDTO == null)
                return BadRequest("Algorithm data must be set!");
            // Handle error if id is sent.
            if (algorithmDTO.Id != null)
                return BadRequest("No ID should be provided!");

            // Map the DTO to entity and save the entity
            Algorithm createdEntity = _service.Create(algorithmDTO.ToEntity());

            // According to the conventions, we have to return a HTTP 201 created repsonse, with
            // field "Location" in the header pointing to the created object
            return CreatedAtAction(
                nameof(GetAlgorithm),
                new { id = createdEntity.Id },
                new AlgorithmDTO(createdEntity));
        }

        // Update an existing algorithm
        [HttpPut("{id}")]
        public ActionResult UpdateAlgorithm(string id, NewAlgorithmDTO algorithmDTO)
        {
            // Handle error if no data is sent.
            if (algorithmDTO == null)
                return BadRequest("Algorithm data must be set!");

            try
            {
                // Map the DTO to entity and save it
                _service.Update(id, algorithmDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content.
                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                // Handle error if the algorithm to update doesn't exists.
                return BadRequest("No Algorithm exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAlgorithm(string id)
        {
            _service.Delete(id);
            // According to the conventions, we have to return HTTP 204 No Content.
            return NoContent();
        }
    }
}
