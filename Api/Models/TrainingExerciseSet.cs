using System;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class TrainingExerciseSet : BaseModel
    {
        public int Id { get; set; }
        public decimal Reps { get; set; }
        public decimal Weight { get; set; }
        public Unit Unit { get; set; }
        public TrainingExercise Exercise { get; set; }
        public int TrainingExerciseId { get; set; }

        public static TrainingExerciseSet Create(decimal reps, decimal weight, Unit unit, User user) => new TrainingExerciseSet
        {
            Reps = reps,
            Weight = weight,
            Unit = unit,
            Created = DateTime.Now,
            CreatedBy = user,
            CreatedById = user.Id,
            LastUpdated = DateTime.Now,
            LastUpdatedBy = user,
            LastUpdatedById = user.Id
        };
    }
}