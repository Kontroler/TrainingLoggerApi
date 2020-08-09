using System.Collections.Generic;

namespace TrainingLogger.Dtos
{
    public class TrainingExerciseDto
    {
        public ExerciseDto Exercise { get; set; }
        public ICollection<TrainingExerciseSetDto> Set { get; set; }
    }
}