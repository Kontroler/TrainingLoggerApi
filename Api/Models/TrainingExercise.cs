using System;
using System.Collections.Generic;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class TrainingExercise : BaseModel
    {
        public int Id { get; set; }
        public Exercise Exercise { get; set; }
        public TrainingExerciseSet Set { get; set; }
        public Training Trainig { get; set; }
        public int TrainingId { get; set; }

        public static TrainingExercise Create(Exercise exercise, User user) => new TrainingExercise
        {
            Exercise = exercise,
            Created = DateTime.Now,
            CreatedBy = user,
            CreatedById = user.Id,
            LastUpdated = DateTime.Now,
            LastUpdatedBy = user,
            LastUpdatedById = user.Id
        };
    }
}