using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingLogger.API.Dtos;
using TrainingLogger.Services;
using TrainingLogger.Exceptions;

namespace TrainingLogger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                await _service.Register(userForRegisterDto.Username, userForRegisterDto.Password);
                return StatusCode(201);
            } 
            catch (UsernameAlreadyExistsException)
            {
                return BadRequest("Username already exists.");
            } 
            catch (Exception)
            {
                return BadRequest("Register error.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var token = await _service.Login(userForLoginDto.Username, userForLoginDto.Password);
            if (token == null) return Unauthorized();
            return Ok(new { token });
        }

    }
}