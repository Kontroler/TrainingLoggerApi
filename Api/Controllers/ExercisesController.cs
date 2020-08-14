using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingLogger.Services;

namespace TrainingLogger.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : BaseController
    {
        private readonly IExerciseService _service;
        private readonly ILogger _logger;

        public ExercisesController(IExerciseService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<ExercisesController>();
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetAllNames()
        {
            try
            {
                var userId = GetUserId();
                var names = await _service.GetAllNames(userId);
                return Ok(names);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Get all exercise names error.");
            }
        }

    }
}
