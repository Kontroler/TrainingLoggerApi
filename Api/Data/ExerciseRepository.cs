using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DataContext _context;

        public ExerciseRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Exercise entity)
        {
            _context.Exercises.Add(entity);
        }

        public void Delete(Exercise entity)
        {
            _context.Exercises.Remove(entity);
        }

        public async Task<Exercise> GetByName(string name, int userId)
        {
            return await _context.Exercises.FirstOrDefaultAsync(e => e.Name == name && e.User.Id == userId);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}