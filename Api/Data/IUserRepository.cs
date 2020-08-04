using System.Threading.Tasks;
using TrainingLogger.API.Models;

namespace TrainingLogger.API.Data
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUser(int id);
    }
}