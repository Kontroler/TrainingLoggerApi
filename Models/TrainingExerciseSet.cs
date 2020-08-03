using System;
using System.Collections.Generic;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class TrainingExerciseSet : BaseModel
    {
        public TrainingExerciseSet()
        {
            Reps = new List<TrainingExerciseSetRep>();
        }
        public int Id { get; set; }
        public ICollection<TrainingExerciseSetRep> Reps { get; set; }
        public TrainingExercise Exercise { get; set; }
        public int TrainingExerciseId { get; set; }

        public static TrainingExerciseSet Create(User user) => new TrainingExerciseSet
        {
            Created = DateTime.Now,
            CreatedBy = user,
            CreatedById = user.Id,
            LastUpdated = DateTime.Now,
            LastUpdatedBy = user,
            LastUpdatedById = user.Id
        };
    }
}