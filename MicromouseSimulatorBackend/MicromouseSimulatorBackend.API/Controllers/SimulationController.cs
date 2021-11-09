using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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

        [HttpGet]
        public ActionResult<IEnumerable<SimulationExpandedDTO>> GetSimulations()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_service.FindAll(userId).Select(s => new SimulationExpandedDTO(s)));
        }

        [HttpGet("{id}")]
        public ActionResult<SimulationExpandedDTO> GetSimulation(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _service.FindById(id, userId);

            if (result == null)
                return NotFound();

            return Ok(new SimulationExpandedDTO(result));
        }

        [HttpPost]
        public ActionResult<SimulationDTO> CreateNewSimulation(NewSimulationDTO simulationDTO)
        {
            if (simulationDTO.Id != null)
                return BadRequest("No ID should be provided!");
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                Simulation createdEntity = _service.Create(simulationDTO.ToEntity(), userId);

                return CreatedAtAction(
                    nameof(GetSimulation),
                    new { id = createdEntity.Id },
                    new SimulationDTO(createdEntity));
            }
            catch (DocumentDoesntExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateSimulation(string id, NewSimulationDTO simulationDTO)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _service.Update(id, simulationDTO.ToEntity(), userId);

                return NoContent();
            }
            catch (DocumentDoesntExistsException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSimulation(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _service.Delete(id, userId);
            return NoContent();
        }

        [HttpGet("{id}/run")]
        public async Task<ActionResult<SimulationResultDTO>> RunSimulationAsync(string id)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var result = await _service.RunSimulationAsync(id, userId);
                return Ok(new SimulationResultDTO(result));
            }
            catch (DocumentDoesntExistsException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
