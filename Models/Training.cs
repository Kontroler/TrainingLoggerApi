using System;
using System.Collections.Generic;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class Training : BaseModel
    {
        public Training()
        {
            Exercises = new List<TrainingExercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<TrainingExercise> Exercises { get; }
        public User User { get; set; }

        public static Training Create(string name, DateTime date, User user) => new Training
        {
            Name = name,
            Date = date,
            User = user,
            Created = DateTime.Now,
            CreatedBy = user,
            CreatedById = user.Id,
            LastUpdated = DateTime.Now,
            LastUpdatedBy = user,
            LastUpdatedById = user.Id
        };
    }
}