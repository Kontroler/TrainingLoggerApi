using System;
using System.Collections.Generic;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class Training : BaseModel
    {
        public Training()
        {
            Exercises = new List<Exercise>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Exercise> Exercises { get; }
        public User User { get; set; }
    }
}