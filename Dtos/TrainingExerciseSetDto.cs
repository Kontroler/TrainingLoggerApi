using System.Collections.Generic;

namespace TrainingLogger.Dtos
{
    public class TrainingExerciseSetDto
    {
        public ICollection<TrainingExerciseSetRepDto> Reps { get; set; }
    }
}   