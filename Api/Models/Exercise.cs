using System;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public class Exercise: BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User User { get; set; }

        public static Exercise Create(string name, User user) => new Exercise
        {
            Name = name,
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