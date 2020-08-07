using System.Collections.Generic;

namespace TrainingLogger.Dtos
{
    public class TrainingExerciseDto
    {
        public ExerciseDto Exercise { get; set; }
        public TrainingExerciseSetDto Set { get; set; }
    }
}