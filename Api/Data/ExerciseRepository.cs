using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;
using TrainingLogger.Exceptions;
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
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ArgumentNullOrWhiteSpaceException("Exercise name cannot be null or white space");
            }
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

        public async Task<IEnumerable<Exercise>> GetAllByUserId(int userId)
        {
            return await _context.Exercises.Where(e => e.User.Id == userId).ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<string>> GetAllNames(int userId)
        {
            var names = await _context.Exercises.Select(e => e.Name).ToListAsync();
            return names;
        }
    }
}