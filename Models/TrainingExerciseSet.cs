using System.Collections.Generic;

namespace TrainingLogger.Models
{
    public class TrainingExerciseSet: BaseModel
    {
        public TrainingExerciseSet()
        {
            Reps = new List<TrainingExerciseSetRep>();
        }
        public int Id { get; set; }
        public ICollection<TrainingExerciseSetRep> Reps { get; set; }
        public TrainingExercise Exercise { get; set; }
        public int TrainingExerciseId { get; set; } 
    }
}