using System.Collections.Generic;
using System.Linq;
using MicromouseSimulatorBackend.API.DTOs;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using MicromouseSimulatorBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicromouseSimulatorBackend.API.Controllers
{
    [Route("simulations")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationService _service;
        public SimulationController(ISimulationService service)
        {
            this._service = service;
        }

        // Fetch all simulations
        [HttpGet]
        public ActionResult<IEnumerable<SimulationExpandedDTO>> GetSimulations()
        {
            return Ok(_service.FindAll().Select(s => new SimulationExpandedDTO(s)));
        }

        // Find one simulation by id
        [HttpGet("{id}")]
        public ActionResult<SimulationDTO> GetSimulation(string id)
        {
            var result = _service.FindById(id);

            if (result == null)
                return NotFound();

            return Ok(new SimulationDTO(result));
        }

        // Create a new simulation
        [HttpPost]
        public ActionResult<SimulationDTO> CreateNewSimulation(SimulationDTO simulationDTO)
        {
            // Handle error if no data is sent.
            if (simulationDTO == null)
                return BadRequest("Simulation data must be set!");
            // Handle error if id is sent.
            if (simulationDTO.Id != null)
                return BadRequest("No ID should be provided!");

            // Map the DTO to entity and save the entity
            Simulation createdEntity = _service.Create(simulationDTO.ToEntity());

            // According to the conventions, we have to return a HTTP 201 created repsonse, with
            // field "Location" in the header pointing to the created object
            return CreatedAtAction(
                nameof(GetSimulation),
                new { id = createdEntity.Id },
                new SimulationDTO(createdEntity));
        }

        // Update an existing simulation
        [HttpPut("{id}")]
        public ActionResult UpdateSimulation(string id, SimulationDTO simulationDTO)
        {
            // Handle error if no data is sent.
            if (simulationDTO == null)
                return BadRequest("Simulation data must be set!");

            try
            {
                // Map the DTO to entity and save it
                _service.Update(id, simulationDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content.
                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                // Handle error if the simulation to update doesn't exists.
                return BadRequest("No Simulation exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSimulation(string id)
        {
            _service.Delete(id);
            // According to the conventions, we have to return HTTP 204 No Content.
            return NoContent();
        }
    }
}
