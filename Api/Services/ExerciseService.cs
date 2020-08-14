using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainingLogger.Data;
using TrainingLogger.Dtos;

namespace TrainingLogger.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _repo;
        public ExerciseService(IExerciseRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ExerciseNameResponseDto>> GetAllNames(int userId)
        {
            var exerciseNames = await _repo.GetAllNames(userId);
            var exerciseNameResponseDtos = exerciseNames.Select(name => new ExerciseNameResponseDto { Name = name });
            return exerciseNameResponseDtos;
        }
    }
}
