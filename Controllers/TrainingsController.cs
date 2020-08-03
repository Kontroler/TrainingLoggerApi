using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public TrainingsController(ITrainingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var trainigs = await _service.GetAllByUserId(userId);
            return Ok(trainigs);
        }

        [HttpPost]
        public async Task<IActionResult> Add(TrainingForAddDto trainingForAddDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            try {                
                await _service.Add(trainingForAddDto, userId);
                return StatusCode(201);
            } 
            catch(Exception)
            {
                return BadRequest("Add training error");
            }
        }
    }
}