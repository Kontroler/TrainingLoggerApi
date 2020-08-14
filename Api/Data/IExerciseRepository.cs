using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public interface IExerciseRepository : IBaseRepository<Exercise>
    {
        Task<Exercise> GetByName(string name, int userId);
        Task<IEnumerable<Exercise>> GetAllByUserId(int userId);
        Task<IEnumerable<string>> GetAllNames(int userId);
    }
}