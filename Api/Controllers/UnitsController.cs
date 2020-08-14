using System;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrainingLogger.Controllers;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : BaseController
    {
        private readonly IUnitService _service;
        private readonly ILogger _logger;

        public UnitsController(IUnitService service, ILoggerFactory loggerFactory)
        {
            _service = service;
            _logger = loggerFactory.CreateLogger<UnitsController>();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetAll());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("Get all units error.");
            }
        }
    }
}