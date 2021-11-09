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
    [Route("mice")]
    [ApiController]
    public class MouseController : ControllerBase
    {
        private readonly IMouseService _service;
        public MouseController(IMouseService service)
        {
            this._service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MouseDTO>> GetMice()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(_service.FindAll(userId).Select(m => new MouseDTO(m)));
        }

        [HttpGet("{id}")]
        public ActionResult<MouseDTO> GetMouse(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _service.FindById(id, userId);

            if (result == null)
                return NotFound();

            return Ok(new MouseDTO(result));
        }

        [HttpPost]
        public ActionResult<MouseDTO> CreateNewMouse(NewMouseDTO mouseDTO)
        {
            if (mouseDTO.Id != null)
                return BadRequest("No ID should be provided!");

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Mouse createdEntity = _service.Create(mouseDTO.ToEntity(), userId);

            return CreatedAtAction(
                nameof(GetMouse),
                new { id = createdEntity.Id },
                new MouseDTO(createdEntity));
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMouse(string id, NewMouseDTO mouseDTO)
        {
            try
            {
                string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _service.Update(id, mouseDTO.ToEntity(), userId);

                return NoContent();
            }
            catch (DocumentDoesntExistsException)
            {
                return BadRequest("No Mouse exists with the given ID!");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMouse(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _service.Delete(id, userId);
            return NoContent();
        }
    }
}
