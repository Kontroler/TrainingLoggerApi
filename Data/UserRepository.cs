using System.Threading.Tasks;
using TrainingLogger.API.Models;
using Microsoft.EntityFrameworkCore;

namespace TrainingLogger.API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(User entity)
        {
            _context.Add(entity);
        }

        public void Delete(User entity)
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}