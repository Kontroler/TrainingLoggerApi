using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.Dtos
{
    public class TrainingExerciseDto
    {
        [Required]
        public ExerciseDto Exercise { get; set; }

        [Required]
        public ICollection<TrainingExerciseSetDto> Sets { get; set; }
    }
}