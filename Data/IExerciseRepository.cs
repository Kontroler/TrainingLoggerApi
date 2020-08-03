using System.Threading.Tasks;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public interface IExerciseRepository: IBaseRepository<Exercise>
    {
         Task<Exercise> GetByName(string name, int userId);
    }
}