using System;
using System.ComponentModel.DataAnnotations;
using TrainingLogger.API.Models;

namespace TrainingLogger.Models
{
    public abstract class BaseModel
    {
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public User LastUpdatedBy { get; set; }
        public int LastUpdatedById { get; set; }
    }
}