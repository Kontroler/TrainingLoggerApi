using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrainingLogger.API.Data;
using TrainingLogger.Exceptions;
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
            if (string.IsNullOrWhiteSpace(entity.Name))
            {
                throw new ArgumentNullOrWhiteSpaceException("Training name cannot be null or white space");
            }
            foreach (var exercise in entity.Exercises)
            {
                if (string.IsNullOrWhiteSpace(exercise.Exercise.Name))
                {
                    throw new ArgumentNullOrWhiteSpaceException("Exercise name cannot be null or white space");
                }
            }
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
                .ThenInclude(set => set.Unit)

                .ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAllTrainingNames(int userId)
        {
            return await _context.Trainings
                .Where(t => t.User.Id == userId)
                .Select(t => t.Name)
                .Distinct()
                .ToListAsync();
        }

        public async Task<Training> GetById(int trainingId, int userId)
        {
            return await _context.Trainings
                .Where(t => t.User.Id == userId && t.Id == trainingId)
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
                .ThenInclude(set => set.Unit)
                
                .FirstOrDefaultAsync();                
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}