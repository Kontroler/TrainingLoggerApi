using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;
using TrainingLogger.Models;

namespace TrainingLogger.Data
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;

        public TrainingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(Training entity)
        {
            _context.Trainings.Add(entity);
        }

        public void Delete(Training entity)
        {
            _context.Trainings.Remove(entity);
        }

        public async Task<IEnumerable<Training>> GetAllByUserId(int userId)
        {
            return await _context.Trainings
                .Where(t => t.User.Id == userId)                
                .Include(training => training.User)

                .Include(training => training.Exercises)
                .ThenInclude(exercise => exercise.Trainig)

                .Include(training => training.Exercises)
                .ThenInclude(exercise => exercise.Exercise)

                .Include(training => training.Exercises)
                .ThenInclude(exercise => exercise.Sets)
                .ThenInclude(set => set.Exercise)

                .Include(training => training.Exercises)
                .ThenInclude(exercise => exercise.Sets)
                .ThenInclude(set => set.Reps)
                .ThenInclude(rep => rep.Unit)

                .Include(training => training.Exercises)
                .ThenInclude(exercise => exercise.Sets)
                .ThenInclude(set => set.Reps)
                .ThenInclude(rep => rep.Set)                
                
                .ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}