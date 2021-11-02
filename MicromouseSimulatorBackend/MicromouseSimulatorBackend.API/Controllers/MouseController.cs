using System.Collections.Generic;
using System.Linq;
using MicromouseSimulatorBackend.API.DTOs;
using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using MicromouseSimulatorBackend.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicromouseSimulatorBackend.API.Controllers
{
    [Route("mice")]
    [ApiController]
    public class MouseController : ControllerBase
    {
        private readonly IMouseService _service;
        public MouseController(IMouseService service)
        {
            this._service = service;
        }

        // Fetch all mice
        [HttpGet]
        public ActionResult<IEnumerable<MouseDTO>> GetMice()
        {
            return Ok(_service.FindAll().Select(m => new MouseDTO(m)));
        }

        // Find one mouse by id
        [HttpGet("{id}")]
        public ActionResult<MouseDTO> GetMouse(string id)
        {
            var result = _service.FindById(id);

            if (result == null)
                return NotFound();

            return Ok(new MouseDTO(result));
        }

        // Create a new mouse
        [HttpPost]
        public ActionResult<MouseDTO> CreateNewMouse(NewMouseDTO mouseDTO)
        {
            // Handle error if no data is sent.
            if (mouseDTO == null)
                return BadRequest("Mouse data must be set!");
            // Handle error if id is sent.
            if (mouseDTO.Id != null)
                return BadRequest("No ID should be provided!");

            // Map the DTO to entity and save the entity
            Mouse createdEntity = _service.Create(mouseDTO.ToEntity());

            // According to the conventions, we have to return a HTTP 201 created repsonse, with
            // field "Location" in the header pointing to the created object
            return CreatedAtAction(
                nameof(GetMouse),
                new { id = createdEntity.Id },
                new MouseDTO(createdEntity));
        }

        // Update an existing mouse
        [HttpPut("{id}")]
        public ActionResult UpdateMouse(string id, NewMouseDTO mouseDTO)
        {
            // Handle error if no data is sent.
            if (mouseDTO == null)
                return BadRequest("Mouse data must be set!");

            try
            {
                // Map the DTO to entity and save it
                _service.Update(id, mouseDTO.ToEntity());

                // According to the conventions, we have to return HTTP 204 No Content.
                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                // Handle error if the mouse to update doesn't exists.
                return BadRequest("No Mouse exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMouse(string id)
        {
            _service.Delete(id);
            // According to the conventions, we have to return HTTP 204 No Content.
            return NoContent();
        }
    }
}
