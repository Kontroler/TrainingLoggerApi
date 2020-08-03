using System;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class TrainingExerciseSetRep : BaseModel
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public decimal Weight { get; set; }
        public Unit Unit { get; set; }
        public TrainingExerciseSet Set { get; set; }
        public int TrainingExerciseSetId { get; set; }

        public static TrainingExerciseSetRep Create(decimal value, decimal weight, Unit unit, User user) => new TrainingExerciseSetRep
        {
            Value = value,
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