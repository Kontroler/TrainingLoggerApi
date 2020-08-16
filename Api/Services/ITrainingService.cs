using System.Collections.Generic;
using System.Threading.Tasks;
using TrainingLogger.Dtos;

namespace TrainingLogger.Services
{
    public interface ITrainingService
    {
        Task<IEnumerable<TrainingDto>> GetAllByUserId(int userId);
        Task<bool> Add(TrainingForAddDto trainingForAddDto, int userId);
        Task<IEnumerable<TrainingNameResponseDto>> GetAllTrainingNames(int userId);
        Task<bool> Delete(int trainingId, int userId);
    }
}