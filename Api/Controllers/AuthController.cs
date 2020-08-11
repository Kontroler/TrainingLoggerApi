using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainingLogger.API.Dtos;
using TrainingLogger.Services;
using TrainingLogger.Exceptions;
using Microsoft.Extensions.Logging;

namespace TrainingLogger.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger _logger;
        public AuthController(IAuthService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<AuthController>();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            try
            {
                await _service.Register(userForRegisterDto.Username, userForRegisterDto.Password);
                return StatusCode(201);
            }
            catch (UsernameAlreadyExistsException e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Username already exists.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Register error.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            try
            {
                var token = await _service.Login(userForLoginDto.Username, userForLoginDto.Password);
                if (token == null) return Unauthorized();
                return Ok(new { token });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Login error.");
            }
        }

    }
}