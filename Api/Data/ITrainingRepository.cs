using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public interface ITrainingRepository : IBaseRepository<Training>
    {
        Task<IEnumerable<Training>> GetAllByUserId(int userId);
        Task<IEnumerable<string>> GetAllTrainingNames(int userId);
    }
}