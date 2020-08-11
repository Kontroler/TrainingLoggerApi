using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingLogger.Dtos;
using TrainingLogger.Services;

namespace TrainingLogger.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingsController : ControllerBase
    {
        private readonly ITrainingService _service;
        private readonly ILogger _logger;

        public TrainingsController(ITrainingService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<TrainingsController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var trainigs = await _service.GetAllByUserId(userId);
                return Ok(trainigs);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Get all trainings error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(TrainingForAddDto trainingForAddDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try
            {
                var result = await _service.Add(trainingForAddDto, userId);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Add training error.");
            }
        }
    }
}