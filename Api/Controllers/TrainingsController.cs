using System;
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
    public class TrainingsController : BaseController
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
                var userId = GetUserId();
                var trainigs = await _service.GetAllByUserId(userId);
                return Ok(trainigs);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Get all trainings error.");
            }
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetAllNames()
        {
            try
            {
                var userId = GetUserId();
                var trainingNames = await _service.GetAllTrainingNames(userId);
                return Ok(trainingNames);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Get all training names error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(TrainingForAddDto trainingForAddDto)
        {
            try
            {
                var userId = GetUserId();
                var result = await _service.Add(trainingForAddDto, userId);
                return StatusCode(201);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Add training error.");
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetUserId();
                var result = await _service.Delete(id, userId);
                return StatusCode(204);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Delete training error.");
            }
        }
    }
}