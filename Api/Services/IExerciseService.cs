using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.Dtos;

namespace TrainingLogger.Services
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseNameResponseDto>> GetAllNames(int userId);
    }
}
