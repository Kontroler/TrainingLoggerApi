using System.ComponentModel.DataAnnotations;

namespace TrainingLogger.Dtos
{
    public class TrainingExerciseSetDto
    {
        [Required]
        public decimal Reps { get; set; }

        [Required]
        public decimal Weight { get; set; }

        [Required]
        public string Unit { get; set; }
    }
}   