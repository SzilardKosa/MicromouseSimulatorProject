using MicromouseSimulatorBackend.BLL.Models;
using MicromouseSimulatorBackend.BLL.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MicromouseSimulatorBackend.API.Controllers
{
    [Route("auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            this._service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(NewUser newUser)
        {
            try
            {
                await _service.Register(newUser);
                return StatusCode(201); // resource successfully created
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Login data)
        {
            try
            {
                var token = await _service.Login(data);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
