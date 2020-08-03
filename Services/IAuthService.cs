using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TrainingLogger.API.Models;

namespace TrainingLogger.Services
{
    public interface IAuthService
    {
        Task<User> Register(string username, string password);
        Task<string> Login(string username, string password);
    }
}